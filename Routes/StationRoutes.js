const { createStation } = require("../Controllers/StationController");
const router = require("express").Router();

router.post('/createstation',createStation);

module.exports = router;