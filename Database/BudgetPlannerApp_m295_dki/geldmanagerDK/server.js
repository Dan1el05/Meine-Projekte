// Dieser Abschnitt prüft, ob die Umgebungsvariable NODE_ENV nicht auf 'production' gesetzt ist und lädt die .env-Datei, falls erforderlich
if (process.env.NODE_ENV !== 'production') {
  require('dotenv').config();
}

// Import von benötigten Modulen und Initialisierung von Express
const express = require('express');
const app = express();
const bcrypt = require('bcrypt');
const passport = require('passport');
const flash = require('express-flash');
const session = require('express-session');
const methodOverride = require('method-override');
const mongoose = require('mongoose');
const User = require('./models/userModel'); // Annahme: Benutzermodell ist definiert
const initializePassport = require('./passport-config'); // Annahme: Passport-Konfiguration ist definiert

// Exportiere die Express-App für die Verwendung in anderen Dateien
module.exports = app;

// Initialisierung der Passport-Konfiguration
initializePassport(
  passport,
  async (email) => await User.findOne({ email: email }), // Funktion zum Suchen eines Benutzers anhand der E-Mail
  async (id) => await User.findById(id) // Funktion zum Suchen eines Benutzers anhand der ID
);

// Setzen des View-Engines für das Rendern von EJS-Dateien
app.set('view-engine', 'ejs');
// Middleware zum Verarbeiten von URL-codierten Daten im Request-Body
app.use(express.urlencoded({ extended: false }));
// Flash-Messages Middleware für das Anzeigen von Meldungen an den Benutzer
app.use(flash());
// Middleware für das Verwalten von Sitzungen
app.use(
  session({
    secret: process.env.SESSION_SECRET, // Geheimnis für die Sitzungsverschlüsselung
    resave: false,
    saveUninitialized: false,
  })
);
// Passport-Initialisierung und Session-Unterstützung
app.use(passport.initialize());
app.use(passport.session());
// Middleware für das Überladen von HTTP-Methoden
app.use(methodOverride('_method'));

// MongoDB-Verbindung
mongoose
  .connect(
    // MongoDB-Verbindungszeichenfolge
    'mongodb+srv://danielkiener:test1234@clusterdk.ncpqt0p.mongodb.net/mongodbtest1?retryWrites=true&w=majority&appName=ClusterDK'
  )
  .then(() => console.log('DB Connection successful!')) // Bei erfolgreicher Verbindung
  .catch(err => console.error('DB Connection error:', err)); // Bei Fehlern

// Benutzerdefinierte Middleware, um zu überprüfen, ob der Benutzer authentifiziert ist
function checkAuthenticated(req, res, next) {
  if (req.isAuthenticated()) {
    return next();
  }
  res.redirect('/login');
}

// Benutzerdefinierte Middleware, um zu überprüfen, ob der Benutzer nicht authentifiziert ist
function checkNotAuthenticated(req, res, next) {
  if (req.isAuthenticated()) {
    return res.redirect('/');
  }
  next();
}

// Routen

// Startseite, erfordert Authentifizierung
app.get('/', checkAuthenticated, (req, res) => {
  res.render('index.ejs', { name: req.user.name });
});

// Budgetseite, erfordert Authentifizierung
app.get('/budget', checkAuthenticated, async (req, res) => {
  try {
    // Benutzer aus der Datenbank abrufen
    const user = await User.findById(req.user._id);
    if (!user) {
      console.error('User not found:', req.user._id);
      return res.status(404).send('User not found');
    }

    // Budget und Ausgaben des Benutzers abrufen
    const budget = user.budget;
    const spent = user.expenses.reduce((total, expense) => total + expense, 0);
    const availableBudget = budget - spent;

    // Rendern der Budgetseite mit den Daten
    res.render('index.ejs', { name: req.user.name, budget: budget, spent: spent, availableBudget: availableBudget });
  } catch (error) {
    console.error('Error fetching user:', error);
    res.status(500).send('Error fetching user');
  }
});

// Handler zum Aktualisieren des Budgets
app.post('/updateBudget', checkAuthenticated, async (req, res) => {
  const newBudget = req.body.budget;

  try {
    // Budget des Benutzers in der Datenbank aktualisieren
    const result = await User.findByIdAndUpdate(req.user._id, { budget: newBudget });

    if (!result) {
      console.error('User not found:', req.user._id);
      return res.status(404).send('User not found');
    }

    console.log('Budget updated successfully:', result.budget);
    res.redirect('/budget');
  } catch (error) {
    console.error('Error updating budget:', error);
    res.status(500).send('Error updating budget');
  }
});

// Handler zum Hinzufügen einer Ausgabe
app.post('/addExpense', checkAuthenticated, async (req, res) => {
  const newExpense = req.body.expense;

  try {
    // Benutzer aus der Datenbank abrufen
    const user = await User.findById(req.user._id);
    if (!user) {
      console.error('User not found:', req.user._id);
      return res.status(404).send('User not found');
    }

    // Ausgabe zum Budget des Benutzers hinzufügen
    user.expenses.push(newExpense);
    await user.save();

    console.log('Expense added successfully:', newExpense);
    res.redirect('/budget');
  } catch (error) {
    console.error('Error adding expense:', error);
    res.status(500).send('Error adding expense');
  }
});

// Handler zum Zurücksetzen des Budgets und der Ausgaben des Benutzers
app.post('/reset', checkAuthenticated, async (req, res) => {
  try {
    // Benutzer aus der Datenbank abrufen
    const user = await User.findById(req.user._id);
    if (!user) {
      console.error('User not found:', req.user._id);
      return res.status(404).send('User not found');
    }

    // Budget und Ausgaben zurücksetzen
    user.budget = 0;
    user.expenses = [];
    await user.save();

    console.log('Budget and expenses reset successfully');
    res.redirect('/budget');
  } catch (error) {
    console.error('Error resetting budget and expenses:', error);
    res.status(500).send('Error resetting budget and expenses');
  }
});

// Handler zum Verarbeiten von Budgetdaten
app.post('/budget', checkAuthenticated, (req, res) => {
  // Budgetdaten vom Client verarbeiten
  console.log('Received budget data:', req.body);
  // Hier können Sie die Budgetdaten verarbeiten und eine Antwort senden, wenn erforderlich
  res.send('Budget data received successfully!');
});


// Route zum Rendern der Login-Seite, erfordert nicht authentifizierten Zustand
app.get('/login', checkNotAuthenticated, (req, res) => {
  res.render('login.ejs');
});

// Route zum Verarbeiten des Login-Formulars, erfordert nicht authentifizierten Zustand
app.post('/login', checkNotAuthenticated, passport.authenticate('local', {
  successRedirect: '/budget', // Bei erfolgreicher Authentifizierung Weiterleitung auf Budgetseite
  failureRedirect: '/login',   // Bei Misserfolg Weiterleitung auf Login-Seite
  failureFlash: true           // Aktivierung von Flash-Mitteilungen im Falle eines Authentifizierungsfehlers
}));

// Route zum Rendern der Registrierungsseite, erfordert nicht authentifizierten Zustand
app.get('/register', checkNotAuthenticated, (req, res) => {
  res.render('register.ejs');
});

// Route zum Verarbeiten des Registrierungsformulars, erfordert nicht authentifizierten Zustand
app.post('/register', checkNotAuthenticated, async (req, res) => {
  try {
    // Hashen des Passworts mit bcrypt
    const hashedPassword = await bcrypt.hash(req.body.password, 10);
    // Erstellen eines neuen Benutzers mit den übermittelten Registrierungsdaten
    const user = new User({
      name: req.body.name,
      email: req.body.email,
      password: hashedPassword,
    });
    // Benutzer in der Datenbank speichern
    await user.save();
    // Nach erfolgreicher Registrierung Weiterleitung zur Login-Seite
    res.redirect('/login');
  } catch (error) {
    console.error(error);
    res.redirect('/register'); // Bei Fehlern Weiterleitung zur Registrierungsseite
  }
});

// Passport-Initialisierung und Session-Unterstützung
app.use(passport.initialize());
app.use(passport.session());

// Route zum Abmelden des Benutzers
app.delete('/logout', (req, res) => {
  req.logout(() => {
    res.redirect('/login'); // Nach Abmeldung Weiterleitung zur Login-Seite
  });
});

// Starten des Servers und Zuhören auf Port 3000
app.listen(3000, () => {
  console.log('Server is running on http://localhost:3000');
});