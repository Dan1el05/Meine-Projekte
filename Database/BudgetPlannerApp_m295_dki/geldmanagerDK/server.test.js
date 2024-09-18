const request = require('supertest');
const app = require('./server');
const mongoose = require('mongoose');

// Öffne die Datenbankverbindung vor dem Start der Tests
beforeAll(async () => {
  await mongoose.connect(
    'mongodb+srv://danielkiener:test1234@clusterdk.ncpqt0p.mongodb.net/mongodbtest1?retryWrites=true&w=majority&appName=ClusterDK'
  );
  console.log('DB Connection successful!');
});

// Schließe die Datenbankverbindung nach Abschluss der Tests
afterAll(async () => {
  await mongoose.connection.close();
  console.log('DB Connection closed!');
});

describe('Server Endpoints', () => {
  it('should calculate available budget correctly', async () => {
    // Annahme: Authentifizierung ist erforderlich
    // Erstelle einen Testbenutzer oder verwende vorhandene Testdaten
    const testUser = { email: 'test@example.com', password: 'password' };
    
    // Melde dich mit dem Testbenutzer an
    const loginResponse = await request(app)
      .post('/login')
      .send(testUser);
      
    // Überprüfe, ob die Anmeldung erfolgreich war (Statuscode 302 bedeutet erfolgreiche Anmeldung und Umleitung)
    expect(loginResponse.statusCode).toEqual(302);
    
    // Sende eine Anfrage an die geschützte Route erst nach erfolgreicher Anmeldung
    const budgetResponse = await request(app)
      .get('/budget')
      .set('Cookie', loginResponse.headers['set-cookie']); // Übergebe die Session-Cookies, um authentifiziert zu bleiben
    
    // Überprüfe, ob die Antwort eine Weiterleitung ist (Statuscode 302)
    expect(budgetResponse.statusCode).toEqual(302);
    
  });
});
