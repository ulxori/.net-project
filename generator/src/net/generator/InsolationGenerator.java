package net.generator;

class InsolationGenerator extends Generator<Integer> implements Runnable {
    private int minInsolation;

    private int maxInsolation;

    public InsolationGenerator(String label, int samplePerMinute, int minInsolation, int maxInsolation,
                               boolean phaseFlag) {
        this.minInsolation = minInsolation;
        this.maxInsolation = maxInsolation;
        this.samplePerMinute = samplePerMinute;
        this.label = label;
        this.phaseFlag = phaseFlag;

        factory.setHost(ConnectionConfig.ADDRESS);
        factory.setPort(Integer.parseInt(ConnectionConfig.PORT));
    }

    @Override
    protected Integer generateValue() {
        return random.nextInt(maxInsolation - minInsolation) + minInsolation;
    }

    @Override
    public void run() {
        generate();
    }
}
