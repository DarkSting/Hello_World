const mongoose = require('mongoose');

const lecturerSchema = new mongoose.Schema({

    name:{
        type:String,
        required:[true,'provide a name for the lecturer']
    },

})


const lecturerModel = mongoose.model("lecturer", lecturerSchema);

module.exports = {lecturerModel};