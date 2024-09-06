package ksb;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class LoginController {

    @FXML
    private TextField emailField;

    @FXML
    private PasswordField passwordField;

    @FXML
    private Label feedbackLabel;

    private static final String CSV_FILE = "src/main/resources/users.csv"; // Pfad zur CSV-Datei mit Benutzerdaten

    //Login
    @FXML
    private void handleLoginButtonAction(ActionEvent event) {
        String email = emailField.getText();
        String password = passwordField.getText();

        if (validateLogin(email, password)) {
            feedbackLabel.setText("Login erfolgreich!");
            navigateToPrimary(); // Bei erfolgreichem Login zur primary.fxml navigieren
        } else {
            feedbackLabel.setText("Ungültige Email oder Passwort!");
        }
    }

    //Login validieren
    private boolean validateLogin(String email, String password) {
        try (BufferedReader br = new BufferedReader(new FileReader(CSV_FILE))) {
            String line;
            while ((line = br.readLine()) != null) {
                String[] values = line.split(",");
                if (values.length == 4 && values[2].equals(email) && values[3].equals(password)) {
                    return true; // Wenn Email und Passwort übereinstimmen
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return false; // Keine Übereinstimmung gefunden
    }

    //Navigiere zum primary.fxml
    private void navigateToPrimary() {
        try {
            Stage stage = (Stage) emailField.getScene().getWindow(); // Aktuelle Stage holen
            Parent root = FXMLLoader.load(getClass().getResource("/ksb/primary.fxml")); // FXML für primary laden
            stage.setScene(new Scene(root, 627, 609)); // Neue Szene setzen
            stage.show(); // Stage anzeigen
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @FXML
    private void handleRegisterAction(ActionEvent event) {
        // Code für die Registrierungsansicht
        try {
            Stage stage = (Stage) emailField.getScene().getWindow();
            Parent root = FXMLLoader.load(getClass().getResource("/ksb/register.fxml"));
            stage.setScene(new Scene(root, 607, 372));
            stage.show();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @FXML
    //Passwort vergessen 
    private void handleForgotPasswordAction(ActionEvent event) {
        System.out.println("Bitte Password überLink in Mail zurücksetzen");
    }
}
