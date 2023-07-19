const mongoose = require("mongoose");

const enumVals = ["VALORANT","DOTA2","CSGO","LOL"];
// Creating the Counter-Strike: Global Offensive tournament schema
const csgoTournamentSchema = new mongoose.Schema({
  
  // Name of the tournament
  tournamentName: {
    type: String,
    validate: {
      validator: function (value) {
        // Custom validation logic to check if the value is in the enumValues array
        return enumVals.includes(value);
      },
      message: props => `${props.value} is not a valid enum value!`,
    },
    required: [true,"not provide tournement name"],
  },
  // Description of the tournament
  tournamentDescription: {
    type: String,
    required: true,
  },
  // Start date of the tournament
  startDate: {
    type: String,
    required: true,
  },
  // End date of the tournament
  endDate: {
    type: String,
    required: true,
  },
  // Maximum number of teams allowed in the tournament
  maxTeams: {
    type: Number,
    required: true,
  },
  // Indicates whether the tournament is held online or offline
  isOnline: {
    type: Boolean,
  },
  // Location of the tournament (required if the tournament is offline)
  tournamentLocation: {
    type: String,
    required: function () {
      return !this.isOnline;
    },
  },
  // Format of the tournament
  tournamentFormat: {
    type: String,
    required: true,
  },
  // Rules of the tournament
  tournamentRules: {
    type: String,
  },
  createdBy:{
    type:mongoose.Schema.Types.ObjectId,
    required:[true, 'provide a creator'],
    ref:'Organizers'
  }
});

// Creating the CSGOTournament model using the csgoTournamentSchema
const CSGOTournament = mongoose.model("CSGOTournament", csgoTournamentSchema);

module.exports = CSGOTournament;
