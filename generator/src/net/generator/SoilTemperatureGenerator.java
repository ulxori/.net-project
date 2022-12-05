package net.generator;

class SoilTemperatureGenerator extends Generator<Double> implements Runnable {
    private double minSoilTemperature;

    private double maxSoilTemperature;

    public SoilTemperatureGenerator(String label, int samplePerMinute, double minSoilTemperature,
                                    double maxSoilTemperature, boolean phaseFlag) {
        this.minSoilTemperature = minSoilTemperature;
        this.maxSoilTemperature = maxSoilTemperature;
        this.label = label;
        this.samplePerMinute = samplePerMinute;
        this.phaseFlag = phaseFlag;

        factory.setHost(ConnectionConfig.ADDRESS);
        factory.setPort(Integer.parseInt(ConnectionConfig.PORT));
    }

    @Override
    protected Double generateValue() {
        return random.nextDouble() * (maxSoilTemperature - minSoilTemperature) + minSoilTemperature;
    }

    @Override
    public void run() {
        generate();
    }
}
