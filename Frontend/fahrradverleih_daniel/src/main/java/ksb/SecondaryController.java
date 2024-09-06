package ksb;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.DatePicker;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.stage.Stage;

import java.io.IOException;

public class SecondaryController {

    @FXML
    private ChoiceBox<String> bikeSelection;

    @FXML
    private DatePicker datePicker;

    @FXML
    private ImageView ivLogout;

    @FXML
    private ImageView ivMoon;

    @FXML
    private Label lbBiken;

    @FXML
    private Label lbFahrradverleih;

    @FXML
    private Label lbHallo;

    @FXML
    void changeBlack(ActionEvent event) {
        navigateToPrimary(); // Methode aufrufen, um zur Primary View zu navigieren
    }

    @FXML
    void handleBuchenButtonAction(ActionEvent event) {
        String selectedBike = bikeSelection.getValue();
        String selectedDate = datePicker.getValue() != null ? datePicker.getValue().toString() : "No date selected";
        
        System.out.println("Bike: " + selectedBike);
        System.out.println("Date: " + selectedDate);
        
        // Buchungsbestätigung als Pop-up-Fenster anzeigen
        Alert alert = new Alert(AlertType.INFORMATION);
        alert.setTitle("Buchungsbestätigung");
        alert.setHeaderText("Ihre Buchung war erfolgreich");
        alert.setContentText("Fahrrad: " + selectedBike + "\nDatum: " + selectedDate);
        alert.showAndWait();
    }

    @FXML
    void handleLogout(ActionEvent event) {
        try {
            Stage stage = (Stage) bikeSelection.getScene().getWindow();
            FXMLLoader loader = new FXMLLoader(getClass().getResource("/ksb/login.fxml"));
            Parent root = loader.load();

            Scene scene = new Scene(root, 600, 400);
            stage.setScene(scene);
            stage.show();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @FXML
    private void initialize() {
        initializeChoiceBoxAndDatePicker();
    }

    private void initializeChoiceBoxAndDatePicker() {
        bikeSelection.getItems().addAll("Hardtail", "E-Bike", "Vollgefedert", "Rennrad");
        // Überprüfung, ob datePicker null ist, bevor darauf zugegriffen wird
        if (datePicker != null) {
            datePicker.setEditable(false);
        } else {
            System.err.println("DatePicker is null. Check FXML configuration.");
        }
    }

    private void navigateToPrimary() {
        try {
            Stage stage = (Stage) bikeSelection.getScene().getWindow();
            FXMLLoader loader = new FXMLLoader(getClass().getResource("/ksb/primary.fxml"));
            Parent root = loader.load();

            Scene scene = new Scene(root, 627, 609); // Anpassen der Größe nach Bedarf
            stage.setScene(scene);
            stage.show();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
