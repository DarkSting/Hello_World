const {createUser} = require('../Controllers/UserControllers');
const supertest = require('supertest');
const mongoose = require('mongoose');
const {app} = require('./TestServer');
require('dotenv').config();
const {
    addUserData,
   getUserData,
   wronguserNameData,
   loginData
   ,createTrain
} = require('./TestData');

describe('testing the functions of userController',()=>{
    beforeAll(async()=>{
        await mongoose.connect(process.env.MONGO_TEST_URI).then(()=>{
            console.log("DB connected");
        })
    })

    test('should create user when all creaditials are provided',async()=>{
        
        await supertest(app).post('/user/adduser')
        .send(addUserData)
        .expect(201);

    });

    test('should avoid adding the same user when creating a user', async()=>{
        await supertest(app).post('/user/adduser')
        .send(addUserData)
        .expect(500);

    })

    test('should find a particular user when search by the username',async()=>{
        const{body}=await supertest(app).post('/user/getuser')
        .send(getUserData)

        expect(body.userName).toMatch(/darky/);

    

    })

    test('should login the user when password and username are correct', async()=>{

        await supertest(app).post('/user/loginuser').send(loginData).expect(200);
    })

    test('user shouldn\'t be able to log when username is incorrect', async()=>{

        await supertest(app).post('/user/loginuser').send(wronguserNameData).expect(500)

    })

    test('user shouldn\'t be able to log when password is incorrect', async()=>{

        await supertest(app).post('/user/loginuser').send(wronguserNameData).expect(500)

    })


   
    
    
    
    afterAll(async()=>{
        await mongoose.disconnect();
        await mongoose.connection.close();

        
    })
})


//testing the train controller
describe('Testing the train controller',()=>{

    beforeAll(async()=>{
        await mongoose.connect(process.env.MONGO_TEST_URI).then(()=>{
            console.log("DB connected");
        })
    })

    test('should create a new train when the provided data are valid',async()=>{
        
        await supertest(app).post('/train/addtrain')
        .send(createTrain)
        .expect(200);

    });


    afterAll(async()=>{
        await mongoose.disconnect();
        await mongoose.connection.close();
    })

})