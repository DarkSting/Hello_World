const Player = require("../models/Player");
const { TeamModel } = require("../models/Team");


//create teams 
const createTeam = async(req,res)=>{

    //get mongoose id and team name
    const{creatorId,teamName} = req.body;

    //if creator id is not found then request will be sent back
    if(!creatorId) return res.status(404).json("creator id not defined");


    //checks whether the player has a team
    const hasATeam = await Player.findOne({_id:creatorId})

    console.log(hasATeam);

    if(hasATeam?.joinedTeam){
        if(hasATeam.joinedTeam!==""){
            return res.status(500).json("creator has a team");
        }
    }

    //defining a team id
    const lastModel = TeamModel.find({}).sort({teamId:-1});

    let teamId = 0;

    if(lastModel[0]){
        teamId = lastModel[0].teamId
    }
    else{
        teamId = 10+1;
    }


    //creating and saving the team model
    const response = new TeamModel({
        teamName:teamName,
        teamId: teamId,
        teamLeader: creatorId,

    })

   
    response.save().then(r=>{
        Player.findOneAndUpdate({_id:creatorId},{joinedTeam:r._id}).then(r=>{
            return res.status(201).json("team created");
        }).catch(er=>{
            return res.status(201).json("player updation is failed");
        })
       
    }).catch(er=>{
        return res.status(500).json(er.message);
    })
}



module.exports = {
    createTeam
}




