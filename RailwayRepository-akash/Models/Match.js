const mongoose =require('mongoose');
const { ParticipantSchema } = require('./ParticipantModel');

const status = [null,'PLAYED' , 'NO_SHOW' , 'WALK_OVER' , 'NO_PARTY']

const MatchSchema = new mongoose.Schema({

      nextMatchId:{
        type:mongoose.Schema.Types.ObjectId,
        ref:'match'
      },
      tournamentRoundText:{
        type:String,
        default:"0"
      },
     
      startTime:{
        type:Date,
        default:Date.now
      },
      state:{
        type:String,
        enum:status,
        default:status[0]
      }
      ,
      participants:[ParticipantSchema]
    
});

const MatchModel = mongoose.model('matches',MatchSchema);

module.exports ={MatchModel,MatchSchema};
