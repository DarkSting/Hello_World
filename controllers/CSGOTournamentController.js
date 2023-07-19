const CSGOTournament = require("../models/CSGOTournament");

// Controller to handle Counter-Strike: Global Offensive tournament creation
const createCSGOTournament = async (req, res) => {
  try {
    const {
      tournamentName,
      tournamentDescription,
      startDate,
      endDate,
      maxTeams,
      isOnline,
      tournamentLocation,
      tournamentFormat,
      tournamentRules,
      createdBy
      
    } = req.body;

    // Create a new Counter-Strike: Global Offensive tournament
    const csgoTournament = new CSGOTournament({
      tournamentName,
      tournamentDescription,
      startDate,
      endDate,
      maxTeams,
      isOnline,
      tournamentLocation,
      tournamentFormat,
      tournamentRules,
    });

    // Save the tournament to the database
    await csgoTournament.save();

    return res.status(201).json({
      message:
        "Counter-Strike: Global Offensive tournament created successfully",
      tournamentId: csgoTournament._id, // Get the unique ID of the created tournament
    });
  } catch (error) {
    return res.status(500).json({ error: "Internal server error" });
  }
};

module.exports = {
  createCSGOTournament,
};
