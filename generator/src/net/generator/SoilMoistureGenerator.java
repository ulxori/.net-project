package net.generator;

class SoilMoistureGenerator extends Generator<Integer> implements Runnable {
    private int minSoilMoisture;

    private int maxSoilMoisture;

    SoilMoistureGenerator(String label, int samplePerMinute, int minSoilMoisture, int maxSoilMoisture,
                          boolean phaseFlag) {
        this.label = label;
        this.minSoilMoisture = minSoilMoisture;
        this.maxSoilMoisture = maxSoilMoisture;
        this.samplePerMinute = samplePerMinute;
        this.phaseFlag = phaseFlag;

        factory.setHost(ConnectionConfig.ADDRESS);
        factory.setPort(Integer.parseInt(ConnectionConfig.PORT));
    }

    @Override
    protected Integer generateValue() {
        return random.nextInt(maxSoilMoisture - minSoilMoisture) + minSoilMoisture;
    }

    @Override
    public void run() {
        generate();
    }
}
