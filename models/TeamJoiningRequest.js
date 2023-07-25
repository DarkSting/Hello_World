const mongoose = require('mongoose');

const enumVals = ["VALORANT","DOTA2","CSGO","LOL"];

const teamJoinRequestSchema = new mongoose.Schema({

    senderId:{
        type:mongoose.Schema.Types.ObjectId,
        required:[true,'id not provide']
    },
    senderDescription:{
        type:String,
        default:""
    }
    ,
    //provide a validation like on the tournement model
    tournement:{
        type:String,
        default:""
    }
    ,
    receiverId:{
        type:mongoose.Schema.Types.ObjectId,
        required:[true,'receiver id not provided'],
        ref:'Players'
    }

})

const joinRequestModel = mongoose.model('JoinRequests',teamJoinRequestSchema);

module.exports ={joinRequestModel};