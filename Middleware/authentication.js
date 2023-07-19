const jwt = require('jsonwebtoken');


//to make this work properly request should contain mongoose model id
const authenticateUser = (req,res)=>{

    const{
        token
    } = req.cookies.jwt;

    if(!token){

        jwt.verify(token,process.env.SECRET,(err,decorded)=>{
            if(err){
                if(!decorded){
                    console.log(decorded._id);
                    req._id = decorded._id;
                    next();
                }
                else{
                    res.status(500).json({msg:"unable to decode"});
                }
            }
            else{
                res.status(500).json(err.message);
            }
        })

    }
    else{
        res.status(500).json({msg:"couldnt find the token"});
    }
}

module.exports = authenticateUser;