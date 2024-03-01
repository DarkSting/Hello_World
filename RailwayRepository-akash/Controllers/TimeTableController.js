const {TimeTable} = require('../Models/TimeTableModel')


const creatTable = async(req,res)=>{

try{
    const enum_days = ["MONDAY","TUESDAY","WEDNESDAY","THURSDAY","FRIDAY"];
    const date_structures = [];

    enum_days.forEach((value,index)=>{

        const obj = {}
        obj.dayName = value
        obj.date =""
        obj.sessions=[]
        date_structures.push(obj)
    })

    const newTimeTable = new TimeTable({
        days:date_structures,
        name:req.body.name
    })

    await newTimeTable.save();

    return res.status(201).json({msg:"table created successfully"});

}
catch(e){

    return res.status(400).json({msg:"unable to create timetable"});
}


}


const insertTimeSlot = async(req,res)=>{
    
try{
    const {dayName,startTime,endTime,tableName} = req.body

    const foundDate = await TimeTable.findOne({name:tableName});

    if(foundDate){

        let target_date = null
        
        // prepare the timeslot
        let obj = {}
        obj.startTime = startTime;
        obj.endTime = endTime;
        obj.lecturer = null;
        obj.subject = null;

        // iterating days
        foundDate.days.forEach(element => {
            if(element.dayName === dayName){
                target_date = element
            }
        });

        

        target_date.sessions.push(obj)

        console.log(foundDate.days)

        await foundDate.save();

        return res.status(200).json({msg:"table updated"});

    }

    return res.status(400).json({msg:"bad data"});


}catch(e){
    return res.status(400).json({msg:"unable to create timetable"});
}

}

const deleteTimeSlot = async(req,res) =>{
    
    const{dayName,tableName,endTime,startTime} = req.body

    try{

        const foundDate = await TimeTable.findOne({name:tableName});

    if(foundDate){

        const temp = []
        // iterating days
        foundDate.days.forEach(element => {

            if(element.dayName === dayName){
                
                //removes the element at the specified location
                element.sessions.forEach(session=>{
                    if(startTime === session.startTime && endTime === session.endTime){
                       
                    }
                    else{
                        temp.push(session)
                    }

                })

                element.sessions = temp

            }

        })



        await foundDate.save();

        return res.status(200).json({msg:"table slot deleted"});

    }
}
    catch(e){

        return res.status(400).json({msg:e.message})

    }

}


const updateTimeSlot = async(req,res)=>{

    const{dayName,startTime,endTime,lecturer,subject,tableName} = req.body;

    try{

        const foundDate = await TimeTable.findOne({name:tableName});
        
        if(foundDate){

            //iterate all the sloted in the requested day
            foundDate.days.forEach(element => {
                
                if(element.dayName === dayName){

                    element.sessions.forEach(session=>{

                        if(startTime === session.startTime && endTime === session.endTime){
                            session.lecturer = lecturer;
                            session.subject = subject;
                        }
                    })
                }

            });



            console.log(foundDate)
    
            await foundDate.save();
    
            return res.status(200).json({msg:"table updated"});
    
        }
    
        return res.status(400).json({msg:"bad data"});

    }catch(e){
        return res.status(400).json({msg:e.message});
    }

}




module.exports = {creatTable,insertTimeSlot,updateTimeSlot,deleteTimeSlot}