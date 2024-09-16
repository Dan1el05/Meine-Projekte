package ksb;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class FahrradverleihToggenburg extends Application {

    @Override
    public void start(Stage primaryStage) throws Exception {
        // Laden der primary.fxml Datei
        FXMLLoader loader = new FXMLLoader(getClass().getResource("/ksb/login.fxml"));
        Parent root = loader.load();

        // Laden des CSS-Stils
        Scene scene = new Scene(root, 600, 372);
        scene.getStylesheets().add(getClass().getResource("/ksb/mystyleSheet.css").toExternalForm());

        // Setzen des Titels und Anzeigen der Szene
        primaryStage.setTitle("Fahrradverleih Toggenburg");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
