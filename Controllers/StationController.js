/**
 * 
 * METHOD : POST
 * DESC : create station
 * ROUTE : 
 * 
 */

const { StationModel } = require("../Models/StationModel");

const createStation = async(req,res)=>{

    const{stationNumber,stationName,stationClass} = req.body;


    if(!stationNumber || !stationName || !stationClass){
        return res.status(404).json({code:404,data:"please provide necessary details"});
    }

    const newStation = new StationModel({
        stationNumber:stationNumber,
        stationName:stationName,
        stationClass:stationClass
    })

    newStation.save().then(r=>{
        return res.status(201).json({code:201,data:r});

    }).catch(
        er=>{
            return res.status(500).json({code:500,data:er.message});
        }
    )

                
}

module.exports = {createStation};