const mongoose = require('mongoose');
const supertest = require('supertest');
const {app} = require('./TestServer');
const {createTrain, getTrain, getTrainBoxes} = require('./TestData');


describe('Testing the train controller API',()=>{

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

    test('cannot add a train using the same train number',async()=>{
        
        await supertest(app).post('/train/addtrain')
        .send(createTrain)
        .expect(409);

    });

    test('should receive all the trains available in the database',async()=>{
        
        await supertest(app).post('/train/gettrains')
        .send({})
        .expect(200);

    });

    test('should receive an exisiting train when a valid train number is provided and boxes should exact same',async()=>{
        
        const{body} = await supertest(app).post('/train/gettrain')
        .send(getTrain)
     
        expect(body.trainBoxesWithIDs).toEqual(getTrainBoxes)
    });
    

    afterAll(async()=>{
        await mongoose.disconnect();
        await mongoose.connection.close();
    })

})