const router = require('express').Router();

const{ createRoute, getRoute } = require('../Controllers/TrainRouteController');



router.post('/createroute',createRoute);
router.get('/getroute',getRoute);


module.exports = router;