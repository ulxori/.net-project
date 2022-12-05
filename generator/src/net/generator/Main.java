package net.generator;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import org.apache.log4j.BasicConfigurator;

public class Main extends Application {
    @Override
    public void start(Stage primaryStage) throws Exception {
        // for RabbitMQ's log4j error
        BasicConfigurator.configure();

        FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("GeneratorUI.fxml"));
        Parent root = fxmlLoader.load();
        primaryStage.setTitle("Generator danych - projekt .NET");
        primaryStage.setScene(new Scene(root, 1100, 600));
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}