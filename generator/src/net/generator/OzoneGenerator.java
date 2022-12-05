package net.generator;

public class OzoneGenerator extends Generator<Integer> implements Runnable {
    private int minOzone;

    private int maxOzone;

    public OzoneGenerator(String label, int samplePerMinute, int minOzone, int maxOzone, boolean phaseFlag) {
        this.minOzone = minOzone;
        this.maxOzone = maxOzone;
        this.label = label;
        this.samplePerMinute = samplePerMinute;
        this.phaseFlag = phaseFlag;

        factory.setHost(ConnectionConfig.ADDRESS);
        factory.setPort(Integer.parseInt(ConnectionConfig.PORT));
    }

    @Override
    protected Integer generateValue() {
        return random.nextInt(maxOzone - minOzone) + minOzone;
    }

    @Override
    public void run() {
        generate();
    }
}
