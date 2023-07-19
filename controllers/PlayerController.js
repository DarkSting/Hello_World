const bcrypt = require("bcrypt");
const Player = require("../models/Player");

// Controller to handle player registration
const registerPlayer = async (req, res) => {
  try {
    const {
      fullName,
      gamingAlias,
      username,
      email,
      steamProfileLink,
      password,
    } = req.body;

    // Check if the player already exists
    const existingPlayer = await Player.findOne({
      $or: [{ username }, { email }],
    });
    if (existingPlayer) {
      if (existingPlayer.username === username) {
        return res.status(409).json({ error: "Username already exists" });
      } else {
        return res.status(409).json({ error: "Email already exists" });
      }
    }

    // Create a new player
    const player = new Player({
      fullName,
      gamingAlias,
      username,
      email,
      steamProfileLink,
      password,
    });

    // Save the player to the database
    await player.save();

    return res.status(201).json({ message: "Player registered successfully" });
  } catch (error) {
    return res.status(500).json({ error: "Internal server error" });
  }
};

// Controller to handle player login
const loginPlayer = async (req, res) => {
  try {
    const { username, password } = req.body;

    // Find the player with the provided username
    const player = await Player.findOne({ username });

    if (!player) {
      return res.status(404).json({ error: "Invalid username" });
    }

    // Compare the provided password with the hashed password
    const passwordMatch = await bcrypt.compare(password, player.password);

    if (passwordMatch) {
      return res.json({ message: "Login successful" });
    } else {
      return res.status(401).json({ error: "Invalid password" });
    }
  } catch (error) {
    return res.status(500).json({ error: "Internal server error" });
  }
};

module.exports = {
  registerPlayer,
  loginPlayer,
};
