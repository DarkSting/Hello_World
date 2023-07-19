const bcrypt = require("bcrypt");
const Organizer = require("../models/Organizer");
const jwt = require('jsonwebtoken');
const CSGOTournament = require("../models/CSGOTournament");

const maxAge = 2*60*60;
const createToken = (id)=>{

  return jwt.sign(id,process.env.SECRET,{expiresIn:maxAge});

}

// Controller to handle organizer registration
const registerOrganizer = async (req, res) => {
  try {
    const { fullName, organizationName, username, email, password } = req.body;

    // Check if the organizer already exists
    const existingOrganizer = await Organizer.findOne({
      $or: [{ username }, { email }],
    });
    if (existingOrganizer) {
      if (existingOrganizer.username === username) {
        return res.status(409).json({ error: "Username already exists" });
      } else {
        return res.status(409).json({ error: "Email already exists" });
      }
    }

    // Create a new organizer
    const organizer = new Organizer({
      fullName,
      organizationName,
      username,
      email,
      password,
    });

    // Save the organizer to the database
    await organizer.save();

    return res
      .status(201)
      .json({ message: "Organizer registered successfully" });
  } catch (error) {
    return res.status(500).json({ error: "Internal server error" });
  }
};

const createTournement = async(req,res)=>{

  const{
    _id,
    tournamentName,
    tournamentDescription,
    startDate,
    endDate,
    maxTeams,
    isOnline,
    tournamentLocation,
    tournamentFormat,
    tournamentRules,} = req.body;

  const newTournement = new CSGOTournament({
      tournamentName:tournamentName,
      tournamentDescription:tournamentDescription,
      startDate:startDate,
      endDate:endDate,
      maxTeams:maxTeams,
      isOnline:isOnline,
      tournamentLocation:tournamentLocation,
      tournamentFormat:tournamentFormat,
      tournamentRules:tournamentRules,
      createdBy:_id
  })

  newTournement.save().then(r=>{
    res.status(200).json({msg:"tournement created"});
  }).catch(err=>{
    res.status(500).json({msg:err.message});
  })

}

// Controller to handle organizer login
const loginOrganizer = async (req, res) => {
  try {
    const { username, password } = req.body;

    // Find the organizer with the provided username
    const organizer = await Organizer.findOne({ username });

    if (!organizer) {
      return res.status(404).json({ error: "Invalid username" });
    }

    // Compare the provided password with the hashed password
    const passwordMatch = await bcrypt.compare(password, organizer.password);


    if (passwordMatch) {
      const token = createToken(organizer._id);
      res.cookies(jwt,token,{httpOnly:true});
      return res.json({ message: "Login successful" });
    } else {
      return res.status(401).json({ error: "Invalid password" });
    }
  } catch (error) {
    return res.status(500).json({ error: "Internal server error" });
  }
};

module.exports = {
  registerOrganizer,
  loginOrganizer,
  createTournement
};
