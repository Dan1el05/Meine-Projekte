// Importieren der benötigten Module
const LocalStrategy = require('passport-local').Strategy; // Passport-Strategie für die lokale Authentifizierung
const bcrypt = require('bcrypt'); // Modul für das Hashen und Vergleichen von Passwörtern
const User = require('./models/userModel'); // Importieren des Benutzermodells

// Eine asynchrone Funktion, die einen Benutzer anhand seiner E-Mail-Adresse sucht und zurückgibt
async function getUserByEmail(email) {
  try {
    const user = await User.findOne({ email: email }); // Benutzer durch seine E-Mail-Adresse finden
    return user; // Wenn kein Benutzer mit der E-Mail gefunden wird, gibt findOne null zurück
  } catch (error) {
    console.error('Error finding user by email:', error); // Fehlerbehandlung, falls ein Fehler auftritt
    throw error; // Fehler weiterwerfen, um ihn später zu behandeln
  }
}

// Eine asynchrone Funktion, die einen Benutzer anhand seiner ID sucht und zurückgibt
async function getUserById(id) {
  try {
    const user = await User.findById(id); // Benutzer durch seine ID finden
    return user; // Wenn kein Benutzer mit der ID gefunden wird, gibt findById null zurück
  } catch (error) {
    console.error('Error finding user by ID:', error); // Fehlerbehandlung, falls ein Fehler auftritt
    throw error; // Fehler weiterwerfen, um ihn später zu behandeln
  }
}

// Eine Funktion zum Initialisieren der Passport-Authentifizierung
function initialize(passport) {
  // Eine Funktion zur Authentifizierung eines Benutzers
  const authenticateUser = async (email, password, done) => {
    try {
      const user = await getUserByEmail(email); // Benutzer anhand der E-Mail-Adresse suchen
      if (!user) { // Wenn kein Benutzer gefunden wird
        return done(null, false, { message: 'No user with that email' }); // Benutzer nicht gefunden
      }
      // Wenn ein Benutzer gefunden wird, Passwort überprüfen
      if (await bcrypt.compare(password, user.password)) { // Passwortüberprüfung mit bcrypt
        return done(null, user); // Benutzer authentifiziert
      } else {
        return done(null, false, { message: 'Password incorrect' }); // Falsches Passwort
      }
    } catch (error) {
      return done(error); // Fehler behandeln
    }
  };

  // Passport verwenden, um eine lokale Authentifizierungsstrategie zu definieren
  passport.use(new LocalStrategy({ usernameField: 'email' }, authenticateUser));

  // Passport-Seriennummerisierung: Benutzerobjekt in eine ID umwandeln
  passport.serializeUser((user, done) => done(null, user.id));

  // Passport-Deserialisierung: ID in ein Benutzerobjekt umwandeln
  passport.deserializeUser(async (id, done) => {
    try {
      const user = await getUserById(id); // Benutzer anhand der ID suchen
      done(null, user); // Benutzerobjekt zurückgeben
    } catch (error) {
      done(error); // Fehler behandeln
    }
  });
}

// Die initialize-Funktion exportieren, um sie in anderen Dateien verwenden zu können
module.exports = initialize;
