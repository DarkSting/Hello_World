const mongoose = require('mongoose');

const bookingSessionSchema = new mongoose.Schema({

    stationID:{
        type:mongoose.Schema.Types.ObjectId,
        ref:'stations'
    },
    bookings:[{
        type:mongoose.Schema.Types.ObjectId,
        ref:'bookings'
    }],
    capacity:{
        type:Number,
        required:[true,"capacity required"]
    },
    createdTime:{
        type:Date,
    },
    expiredDate:{
        type:Date,
    }

})

const bookingSessionModel = new mongoose.model('bookingsessions',bookingSessionSchema);

module.exports = bookingSessionModel;


