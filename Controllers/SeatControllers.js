// loading necessary modules
const {seatModel} = require("../Models/SeatModel");

/*
method: GET
route : api/train/
description: returns all the trains
*/

//adding trains 
const addSeat = async(req,res)=>{

  const{
    seatNumber,
    seatOccupiedt,
  endpoint,
  price,
  seats,
  totalSeats} = req.body;

  const newTrain = new trainModel({
    
    name:name,
    startpoint:startpoint,
    endpoint:endpoint,
    price:price,
    seats:seats,
    totalSeats:totalSeats

  });

  await newTrain.save().then(result=>{
    return res.status(200).json({msg:"Train added"});
  }).catch(err=>{
    return res.status(500).json(err);
  })


}






module.exports = { getTrains, addTrain, getTrain, deleteTrain };
