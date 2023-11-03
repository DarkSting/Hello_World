const mongoose =require('mongoose');

const status = [null,'PLAYED', 'NO_SHOW' , 'WALK_OVER' , 'NO_PARTY']

const ParticipantSchema = new mongoose.Schema({

      id:{
        type:String
     },
      resultText:{
        type:String,
        default:null
      },
     
      isWinner:{
        type:Boolean,
        default:false
      },
      status:{
        type:String,
        enum:status,
        default:status[0]
      }
      ,
      name:{
        type:String
      }
    

});



const ParticipantModel = mongoose.model('paticipants',ParticipantSchema);

module.exports ={ParticipantModel,ParticipantSchema};
