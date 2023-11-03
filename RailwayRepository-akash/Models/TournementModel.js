const mongoose = require("mongoose");

//train schema 
const TournementSchema = new mongoose.Schema({
  
  name: {
    type: String,
  },
  currentRound:{
    type:mongoose.Schema.Types.ObjectId
  },
  
  roundHistory:[
    {
      type:mongoose.Schema.Types.ObjectId,
      ref:'round'
    }
  ]

 
});

const TournementModel = mongoose.model('tournement',TournementSchema)

 module.exports = {
  TournementModel
 }
