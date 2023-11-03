const { RouteModel } = require("../Models/RouteModel");
const { StationModel } = require("../Models/StationModel");
const { trainModel } = require("../Models/TrainModel");
const {tripModel}= require("../Models/TripModel");


const createTrip= async(req,res)=>{

    const{trainID,routeID,departure,arrival,expiry} = req.body;

    try{
        let depart = new Date(departure);
        let exp = new Date(expiry);

        if(depart<exp){
            return res.status(500).json({code:500,data:"expiry cannot be larger than departure"})
        }

    }catch(e){
        return res.status(500).json({e})
    }
    

    if(!trainID && !routeID || !departure || !arrival || !expiry ){
        return res.status(409).json({code:404,data:"provide all necessary details"})
    }
 
    console.log(trainID);

    const foundTrain = await trainModel.findOne({trainNumber:trainID});
    const foundroute = await RouteModel.findOne({_id:routeID});
    const foundTrip = await tripModel.findOne({train:foundTrain.trainNumber});

    console.log(foundTrip);
    console.log(foundTrain);


    if(foundTrip){

        let current_date = new Date();
        let departure = new Date(foundTrip.departure)

        if(departure > current_date){
            return res.status(409).json({code:409,data:"trip already exists"});
        }
       
    }

   


    

    if(foundTrain && foundroute){

        const newTrip =  new tripModel({

            train:foundTrain.trainNumber,
            departure:departure.trim(),
            arrival:arrival.trim(),
            route:foundroute.routeNumber,
            bookingOpeningExpired:expiry.trim(),
    

        })
         await newTrip.save().then(r=>{
            return res.status(201).json({code:201,data:r});
        }).catch(er=>{

            console.log(er);
            return res.status(500).json({code:500,data:er.message});
        })
    }
    else{
        return res.status(404).json({code:404,data:"cannot find either the train or the route"});
    }

}

const getTrips= async(req,res)=>{


    try{

        const foundTrip = await tripModel.find({bookingOpeningExpired:{$gt:Date()}});

        if(foundTrip){
            return res.status(200).json({code:200,trips:foundTrip});
        }
        else{
            return res.status(500).json({code:500,data:"cannot find the trips"});
        }

    }catch(e){
        return res.status(500).json({code:500,data:e.message});
    }
    

   
}

const getCurrentTrips = async(req,res)=>{

    const foundTrips = await tripModel.find({arrival:{$gt:Date()}});

    let resultsArray = [];

    for(let currentTrip of foundTrips){
       let foundRoute = await RouteModel.findOne({routeNumber:currentTrip.route});
       let foundTrain = await trainModel.findOne({trainNumber:currentTrip.train});

       console.log(currentTrip.route);
       if(!foundRoute){
        return res.status(404).json({msg:'unable to find route'});
       }
       let stationsArray = await StationModel.find({_id:{$in:foundRoute.Stops}});

        let dataObject = {
            trip:currentTrip,
            stations:stationsArray,
            train:foundTrain
        }


        resultsArray.push(dataObject);
    }
    
    
    console.log(resultsArray[0].stations);


    return res.status(200).json(resultsArray);

}

module.exports = {
    createTrip,
    getTrips,
    getCurrentTrips

};