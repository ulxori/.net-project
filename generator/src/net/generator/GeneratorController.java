package net.generator;

import javafx.fxml.FXML;
import javafx.scene.control.*;
import org.json.simple.JSONObject;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class GeneratorController {
    @FXML
    private TextField ipTextField;

    @FXML
    private TextField portTextField;

    @FXML
    private TextField queueTextField;

    @FXML
    private Slider minSoilMoistureSlider;

    @FXML
    private Label minSoilMoistureLabel;

    @FXML
    private Slider maxSoilMoistureSlider;

    @FXML
    private Label maxSoilMoistureLabel;

    @FXML
    private Slider minSoilTemperatureSlider;

    @FXML
    private Label minSoilTemperatureLabel;

    @FXML
    private Slider maxSoilTemperatureSlider;

    @FXML
    private Label maxSoilTemperatureLabel;

    @FXML
    private Slider minInsolationSlider;

    @FXML
    private Label minInsolationLabel;

    @FXML
    private Slider maxInsolationSlider;

    @FXML
    private Label maxInsolationLabel;

    @FXML
    private Slider minOzoneSlider;

    @FXML
    private Label minOzoneLabel;

    @FXML
    private Slider maxOzoneSlider;

    @FXML
    private Label maxOzoneLabel;

    @FXML
    private Slider moistureFrequencySlider;

    @FXML
    private Label moistureFrequencyLabel;

    @FXML
    private Slider temperatureFrequencySlider;

    @FXML
    private Label temperatureFrequencyLabel;

    @FXML
    private Slider insolationFrequencySlider;

    @FXML
    private Label insolationFrequencyLabel;

    @FXML
    private Slider ozoneFrequencySlider;

    @FXML
    private Label ozoneFrequencyLabel;

    @FXML
    private CheckBox phaseCheckBox;

    @FXML
    private Button generateBtn;

    @FXML
    private void initialize() {
        setDefaultLabels();
        setDefaultGeneratorsParams();
    }

    private void setDefaultLabels() {
        ipTextField.setText("localhost");
        portTextField.setText("15672");
        queueTextField.setText("test-queue");
    }

    private void setDefaultGeneratorsParams() {
        minSoilMoistureSlider.setValue(35);
        minSoilMoistureLabel.setText("35");
        maxSoilMoistureSlider.setValue(45);
        maxSoilMoistureLabel.setText("45");

        minSoilTemperatureSlider.setValue(22.3);
        minSoilTemperatureLabel.setText("22.3");
        maxSoilTemperatureSlider.setValue(27.1);
        maxSoilTemperatureLabel.setText("27.1");

        minInsolationSlider.setValue(500);
        minInsolationLabel.setText("500");
        maxInsolationSlider.setValue(1500);
        maxInsolationLabel.setText("1500");

        minOzoneSlider.setValue(3);
        minOzoneLabel.setText("3");
        maxOzoneSlider.setValue(350);
        maxOzoneLabel.setText("350");

        moistureFrequencySlider.setValue(5);
        moistureFrequencyLabel.setText("5");
        temperatureFrequencySlider.setValue(6);
        temperatureFrequencyLabel.setText("6");
        insolationFrequencySlider.setValue(3);
        insolationFrequencyLabel.setText("3");
        ozoneFrequencySlider.setValue(10);
        ozoneFrequencyLabel.setText("10");
    }

    @FXML
    private void generateData() {
        boolean flag = phaseCheckBox.isSelected();

        ConnectionConfig.ADDRESS = ipTextField.getText();
        ConnectionConfig.PORT = portTextField.getText();
        ConnectionConfig.QUEUE_NAME = queueTextField.getText();

        if (validateInput()) {
            ExecutorService executor = Executors.newFixedThreadPool(4);
            SoilMoistureGenerator soilMoistureGenerator = new SoilMoistureGenerator(
                    "MO-",
                    (int) moistureFrequencySlider.getValue(),
                    (int) minSoilMoistureSlider.getValue(),
                    (int) maxSoilMoistureSlider.getValue(),
                    flag
            );
            executor.execute(soilMoistureGenerator);

            SoilTemperatureGenerator soilTemperatureGenerator = new SoilTemperatureGenerator(
                    "TM-",
                    (int) temperatureFrequencySlider.getValue(),
                    (int) minSoilTemperatureSlider.getValue(),
                    (int) maxSoilTemperatureSlider.getValue(),
                    flag
            );
            executor.execute(soilTemperatureGenerator);

            InsolationGenerator insolationGenerator = new InsolationGenerator(
                    "IN-",
                    (int) insolationFrequencySlider.getValue(),
                    (int) minInsolationSlider.getValue(),
                    (int) maxInsolationSlider.getValue(),
                    flag
            );
            executor.execute(insolationGenerator);

            OzoneGenerator ozoneGenerator = new OzoneGenerator(
                    "OZ-",
                    (int) ozoneFrequencySlider.getValue(),
                    (int) minOzoneSlider.getValue(),
                    (int) maxOzoneSlider.getValue(),
                    flag
            );

            executor.execute(ozoneGenerator);
            executor.shutdown();

            while (!executor.isTerminated()) {
            }
            System.out.println("Zakończono!");
        }
        else {
            System.out.println("Popraw ustawienia!");
        }
    }

    @FXML
    private void changeButtonColor() {
        generateBtn.setStyle("-fx-background-color: #0c4cab");
    }

    @FXML
    private void resetButtonColor() {
        generateBtn.setStyle("-fx-background-color: #b00c3d");
    }

    /*  */

    @FXML
    private void changeMinSoilMoistureValue() {
        double value = minSoilMoistureSlider.getValue();
        minSoilMoistureLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void changeMaxSoilMoistureValue() {
        double value = maxSoilMoistureSlider.getValue();
        maxSoilMoistureLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void changeMinSoilTemperatureValue() {
        double value = minSoilTemperatureSlider.getValue();
        String tempLabel = String.format("%.2f", value);
        minSoilTemperatureLabel.setText(tempLabel);
    }

    @FXML
    private void changeMaxSoilTemperatureValue() {
        double value = maxSoilTemperatureSlider.getValue();
        String tempLabel = String.format("%.2f", value);
        maxSoilTemperatureLabel.setText(tempLabel);
    }

    @FXML
    private void changeMinInsolationValue() {
        double value = minInsolationSlider.getValue();
        minInsolationLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void changeMaxInsolationValue() {
        double value = maxInsolationSlider.getValue();
        maxInsolationLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void changeMinOzoneValue() {
        double value = minOzoneSlider.getValue();
        minOzoneLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void changeMaxOzoneValue() {
        double value = maxOzoneSlider.getValue();
        maxOzoneLabel.setText(Integer.toString((int) value));
    }

    /*  */

    @FXML
    private void setMoistureFrequency() {
        double value = moistureFrequencySlider.getValue();
        moistureFrequencyLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void setTemperatureFrequency() {
        double value = temperatureFrequencySlider.getValue();
        temperatureFrequencyLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void setInsolationFrequency() {
        double value = insolationFrequencySlider.getValue();
        insolationFrequencyLabel.setText(Integer.toString((int) value));
    }

    @FXML
    private void setOzoneFrequency() {
        double value = ozoneFrequencySlider.getValue();
        ozoneFrequencyLabel.setText(Integer.toString((int) value));
    }

    private boolean validateInput() {
        return (int) minSoilMoistureSlider.getValue() < (int) maxSoilMoistureSlider.getValue() &&
                (int) minSoilTemperatureSlider.getValue() < (int) maxSoilTemperatureSlider.getValue() &&
                (int) minInsolationSlider.getValue() < (int) maxInsolationSlider.getValue() &&
                (int) minOzoneSlider.getValue() < (int) maxOzoneSlider.getValue();
    }
}