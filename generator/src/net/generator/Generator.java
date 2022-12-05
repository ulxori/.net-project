package net.generator;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import org.json.simple.JSONObject;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.time.LocalDate;
import java.time.Period;
import java.util.Random;
import java.util.concurrent.TimeoutException;

import static net.generator.ConnectionConfig.QUEUE_NAME;

abstract class Generator<T> {
    protected String label;

    protected int samplePerMinute;

    protected Random random = new Random();

    protected ConnectionFactory factory = new ConnectionFactory();

    protected boolean phaseFlag;

    public void generate() {
        try (Connection connection = factory.newConnection()) {
            Channel channel = connection.createChannel();
            channel.queueDeclare(QUEUE_NAME, false, false, false, null);

            long difference = 60000 / samplePerMinute;

            long start = System.currentTimeMillis();
            long last = start;
            long aux = start;

            while (last - start <= 61000) {
                last = System.currentTimeMillis();
                if (last - aux >= difference) {

                    for (int i = 1; i <= 8; ++i) {
                        T value = generateValue();
                        String logMessage = label + " odczyt: " + value;
                        //System.out.println(logMessage);

                        JSONObject obj = createJSON(value, i);
                        channel.basicPublish("", QUEUE_NAME, null, obj.toJSONString().getBytes(StandardCharsets.UTF_8));
                        aux = last;
                    }
                }
            }
        } catch (IOException | TimeoutException e) {
            e.printStackTrace();
        }
    }

    abstract protected T generateValue();

    private JSONObject createJSON(T value, int i) {
        JSONObject obj = new JSONObject();
        obj.put("sensorType", getTypeByLabel());
        obj.put("sensorNumber", getNumberByLabel(i));
        obj.put("value", value);
        obj.put("date", generateDate().toString());
        return obj;
    }

    private String getTypeByLabel() {
        String shortcut = label.substring(0, 2);

        return switch (shortcut) {
            case "MO" -> "moisture";
            case "TM" -> "temperature";
            case "IN" -> "insolation";
            case "OZ" -> "ozone";
            default -> "";
        };
    }

    private int getNumberByLabel(int i) {
        String type = getTypeByLabel();
        int shift = 0;

        if (type == "temperature")
            shift = 8;
        else if (type == "insolation")
            shift = 16;
        else if (type == "moisture")
            shift = 24;

        return i + shift;
    }

    private LocalDate generateDate() {
        if (phaseFlag)
            return LocalDate.now();

        return LocalDate.now().minus(Period.ofDays((new Random().nextInt(365 * 12))));
    }
}
