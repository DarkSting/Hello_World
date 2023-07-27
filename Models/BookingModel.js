const mongoose = require("mongoose");
const {seatSchema} = require('./SeatModel');

const bookingSchema = new mongoose.Schema({
  refNo: {
    type: mongoose.Schema.Types.ObjectId,
    ref: "train",
  },
  isPaid: {
    type: Boolean,
    default:false
  },
  totalPrice:{
    type:Number,

  }
  ,
  bookedDate:{
    type:Date.now,
  },
  bookedSeats:{
    type:[seatSchema],
    required:[true ,'seats not provided']
  },
  bookedPerson:{
    type: mongoose.Schema.Types.ObjectId,
    ref:'users',
    required:[true ,'booked person not provided']
  }

});

module.exports = mongoose.model("bookings", bookingSchema);
