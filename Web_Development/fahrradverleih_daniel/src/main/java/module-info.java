module ksb {
    requires javafx.controls;
    requires javafx.fxml;

    opens ksb to javafx.fxml;
    exports ksb;
}
