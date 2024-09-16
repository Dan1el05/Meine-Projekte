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

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class RegisterController {

    @FXML
    private TextField emailField;

    @FXML
    private PasswordField passwordField;

    @FXML
    private PasswordField confirmPasswordField;

    @FXML
    private TextField vornameField;

    @FXML
    private TextField nachnameField;

    @FXML
    private Label feedbackLabel;

    private static final String CSV_FILE = "src/main/resources/users.csv";

    @FXML
    private void handleRegisterButtonAction(ActionEvent event) {
        String vorname = vornameField.getText();
        String nachname = nachnameField.getText();
        String email = emailField.getText();
        String password = passwordField.getText();
        String confirmPassword = confirmPasswordField.getText();

        if (!isValidEmail(email)) {
            feedbackLabel.setText("Ungültige E-Mail-Adresse.");
            return;
        }

        if (!password.equals(confirmPassword)) {
            feedbackLabel.setText("Passwörter stimmen nicht überein.");
            return;
        }

        if (registerUser(vorname, nachname, email, password)) {
            feedbackLabel.setText("Registrierung erfolgreich!");
            navigateToLogin();
        } else {
            feedbackLabel.setText("Registrierung fehlgeschlagen.");
        }
    }

    private boolean isValidEmail(String email) {
        String emailPattern = "^[A-Za-z0-9+_.-]+@(.+)$";
        Pattern pattern = Pattern.compile(emailPattern);
        Matcher matcher = pattern.matcher(email);
        return matcher.matches();
    }

    private boolean registerUser(String vorname, String nachname, String email, String password) {
        try (BufferedWriter bw = new BufferedWriter(new FileWriter(CSV_FILE, true))) {
            bw.write(String.join(",", vorname, nachname, email, password));
            bw.newLine();
            return true;
        } catch (IOException e) {
            e.printStackTrace();
            return false;
        }
    }

    @FXML
    private void handleLoginAction(ActionEvent event) throws IOException {
        navigateToLogin();
    }

    private void navigateToLogin() {
        try {
            Stage stage = (Stage) emailField.getScene().getWindow();
            Parent root = FXMLLoader.load(getClass().getResource("/ksb/login.fxml"));
            stage.setScene(new Scene(root, 607, 372));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
