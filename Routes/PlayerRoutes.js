const {Router} = require('express');
const { registerPlayer, loginPlayer } = require('../controllers/PlayerController');
const {  createTournement } = require('../controllers/OrganizerController');
const authenticateUser = require('../Middleware/authentication');

const router = Router();

router.post("/api/players/register", registerPlayer);
router.post("/api/players/login", loginPlayer);
router.post("/api/players/createtournement",authenticateUser,createTournement);

module.exports = router;