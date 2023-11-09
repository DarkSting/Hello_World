const mongoose = require("mongoose");
const { RoundSchema } = require("./RoundModel");

//train schema
const TournementSchema = new mongoose.Schema({
  name: {
    type: String,
    default: "unknown",
  },
  currentRound: {
    type: Number,
    default: -1,
  },

  roundHistory: [RoundSchema],
});

const TournementModel = mongoose.model("tournement", TournementSchema);

module.exports = {
  TournementModel,
};
