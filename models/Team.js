const mongoose = require('mongoose');


const teamSchema = new mongoose.Schema({

    teamName:{
        type:String,
        required:[true,'team name is not provided'],
        unique:[true,'name should be unique']
    }
    ,
    teamId:{
        type:String,
        required:[true,'id not provided'],
        unique:[true,'id should be unique']
    }
    ,
    teamLeader:{
        type:mongoose.Schema.Types.ObjectId,
        ref:'Players',
        unique:[true,'same person cannot belong to more than one team']
    },
    teamMembers:{
        type:[mongoose.Schema.Types.ObjectId],
        validate: {
            validator: async function (ids) {
              // Validate that the array contains unique IDs
              return new Set(ids).size === ids.length;
            },
            message: 'Array should contain unique IDs only',
          },
          default:[]
        },
        
    
        
    

})

teamSchema.pre('save', async function (next) {

    this.teamMembers = [...new Set(this.teamMembers)];
  
    next();
  });

const TeamModel = mongoose.model('Team',teamSchema);

module.exports = {
TeamModel
}
