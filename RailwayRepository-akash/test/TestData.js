//data for user context//////////////////////
const addUserData ={
    userName: "darky",
    email: "akahsh@gmail.com",
    phone: "077177772",
    password: "12345",
    firstName: "akash",
    lastName: "induruwa",
  
 
}

const wronguserNameData ={
    userName: "black",
    password: "12345",
}

const wrongpasswordData ={
    userName: "black",
    password: "12345",
}

const getUserData ={
    userName : "darky"
}

const loginData = {
    userName:"darky",
    password:"12345"
}


//////////////////// train data ///////////////////////////

const createTrain = {
    trainNumber :"200",
    name : "Udarata Manike",
    boxcount : 3
}

const getTrain = {
    trainNumber : 200
}

const getTrainBoxes = [
    "64d9c5d0f30fde16e47d04be",
          "64d9c5d0f30fde16e47d04bf",
      "64d9c5d0f30fde16e47d04c0",
]



module.exports ={

    getUserData,
    addUserData,
    wrongpasswordData,
    wronguserNameData,
    loginData,
    createTrain,
    getTrain,getTrainBoxes
}