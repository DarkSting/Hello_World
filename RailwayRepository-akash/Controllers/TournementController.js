const { MatchModel } = require("../Models/Match");
const { ParticipantModel } = require("../Models/ParticipantModel");
const { RoundModel } = require("../Models/RoundModel");
const { TournementModel } = require("../Models/TournementModel");

const CreateTournementMatches = async (req, res) => {
  
  //teams : array of teams
  const { teams,tournementName } = req.body;

  let count = teams.length;
  let rounds = 0;

  if (count < 4) {
    return res
      .status(500)
      .json({ msg: "teams not sufficient to create a tournement" });
  }

  //counting the match rounds
  for (let i = 2; i < 5; i++) {
    if (count === (Math.pow(2,i))) {
      
        rounds = i;
        console.log(count)
    }
  }

  if(rounds===0){
    return res.status(500).json({msg:"teams count should be equal to 2^n"})
  }


  let currentMatchCount = count/2;


  //store rounds
  let rd = [];

  //creating matches according to team count
  for(let i=0;i<rounds;i++){

    //store matches in each round
    let mt = [];

    for(let j=0;j<currentMatchCount;j++){
            
        const currentMatch = new MatchModel({ })
        let savedMatch =await currentMatch.save();
        mt.push(savedMatch);

    }

    const savedRound = new RoundModel({
      round:i,
      matches:mt
    }).save()

    rd.push(savedRound);

    currentMatchCount = currentMatchCount/2;

  }


  //creating the match network
  for(let i=0;i<rd.length;i++){

    //get the next pointer converted to array index
    let pointerToNextMatch = (rd[i].length/2)-1;

    //iterating the 2d dynamic array
    for(let j=0;j<rd[i].length;j++){



      if((j+1)%2===0 && (j+1)!==rd[i].length){



      }

    }
  }


  let currentMatchIndex = 0;


  //assigning participants for a match
  for(let i =1;i<=count;i++){

    let newplayer = new ParticipantModel({
      id:i,
      name:`${i}`
    })

    let savedplayer = await newplayer.save()

    rd[0][currentMatchIndex].participants.push(savedplayer);

    if(i%2===0){
      currentMatchIndex++;
    }

  }
  

  return res.status(500).json({msg:"completed"})
//creating the tournement 
//   const newTournement = new TournementModel({
//     name: tournementName,

//   })



};

module.exports = {
  CreateTournementMatches,
};
