const {Router} = require('express');
const { registerPlayer, loginPlayer, getCreatedTeam, sendRequest, getTeams, getPendingRequests } = require('../controllers/PlayerController');
const {  createTournement } = require('../controllers/OrganizerController');
const authenticateUser = require('../Middleware/authentication');
const { createTeam } = require('../controllers/TeamController');

const router = Router();

router.post("/api/players/register", registerPlayer);
router.post("/api/players/login", loginPlayer);
router.post("/api/players/createtournement",authenticateUser,createTournement);
router.post("/api/players/createteam",createTeam);
router.get("/api/players/getteam",getCreatedTeam);
router.post("/api/players/sendrequest",sendRequest);
router.get("/api/players/getteams",getTeams);
router.get("/api/players/getpendingrequest",getPendingRequests);


module.exports = router;