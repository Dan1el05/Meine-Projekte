const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true
  },
  email: {
    type: String,
    required: true
  },
  password: {
    type: String,
    required: true
  },
  budget: {
    type: Number, // Budget als einzelner Wert speichern
    default: 0 // Standardwert für das Budget
  },
  expenses: [{
    type: Number // Ausgaben separat speichern
  }]
});

module.exports = mongoose.model('User', userSchema);
