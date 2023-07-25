const bcrypt = require("bcrypt");
const Player = require("../models/Player");
const { joinRequestModel } = require("../models/TeamJoiningRequest");
const { TeamModel } = require("../models/Team");

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

    console.log(`${fullName} , ${gamingAlias}, ${username},${email},${steamProfileLink},${password}`)
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


//sending a request to join a team
const sendRequest = async(req,res)=>{

  //gets the player mongoose id and descriptoin and tournement 
const{playerId,description,tournement,receiver} = req.body;

//check the nulls
if(!playerId && !receiver) return res.status(404).json("please provide playerid");

//if player already joined to a team cannot joined to another team 
const requestReceiver = await Player.findOne({_id:receiver});

//checks whether the request is being sent to the same person
const pendingRequest = await joinRequestModel.findOne({$and:[{senderId:playerId},{receiverId:receiver}]});

if(pendingRequest){

  return res.status(500).json("creator having pending request");
    
}

let desvalue = description?description:"";
let tournementval= tournement?tournement:"";

//creating the request and saving the model
const newRequest = new joinRequestModel({
  senderId:playerId,
  senderDescription:desvalue,
  tournement:tournementval,
  receiverId:requestReceiver._id

})

newRequest.save().then(async(r)=>{
 try{

  await Player.updateOne({_id:playerId},{joinedTeam:""});
  return res.status(201).json("request sent");

 }catch(e){

  return res.status(500).json(e.message);
 } 

  
}).catch(er=>{
  return res.status(500).json("model saving error");
})

}


//get created team 
const getCreatedTeam = async(req,res)=>{

const{playerId} = req.body;

const response = await Player.findOne({_id:playerId});

console.log(response.joinedTeam);

if(response?._id){

TeamModel.findOne({_id:response.joinedTeam}).then(r=>{
 console.log(r);
 return res.status(200).json("team found");
}).catch(er=>{
  return res.status(500).json("cannot find any team");
})
}

}

//get available teams
const getTeams = async(req,res)=>{

  const teams = await TeamModel.find({});

  if(teams.length ===0){
    return res.status(404).json("cannot find any teams");
  }

  return res.status(200).json(teams)

}


//get pending requests by providing the mongoose id
const getPendingRequests = async(req,res)=>{

  const{playerId} = req.body;

  if(!playerId) return res.status(404).json("player not provided");

  const response = await joinRequestModel.find({receiverId:playerId});

  console.log(response);

  if(response.length===0){
    return res.status(404).json("no pending requests");
  }

  return res.status(200).json(response);

}


//create a controller to update the teams 


module.exports = {
  registerPlayer,
  loginPlayer,
  sendRequest,
  getCreatedTeam,
  getTeams,
  getPendingRequests
};
