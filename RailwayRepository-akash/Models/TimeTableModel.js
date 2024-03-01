const mongoose = require("mongoose");


const timeSlot = {

    startTime:{
        type:String,
        required:true
    }
,
    endTime:{
        type:String,
        required:true
    }
,
    subject:{
        type:String,
    }
,
    lecturer:{
        type:String,
        ref:'Lecturer',
        
    }
}

const day = {

    dayName:{
        type:String,
        required:true
    },
    date:{
        type:String,
        default:null
    },
    sessions: {
        type: [timeSlot],
      }

}

const timeTableSchema = new mongoose.Schema({
  days: {
    type: [day]
  },

  name:{
    type:String
  }
  
});


const TimeTable = mongoose.model("timetable", timeTableSchema);

module.exports = {TimeTable};