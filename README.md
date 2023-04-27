
The TTRPG scheduler is a web API that allows users to propose session dates, allows players to post their availability for those sessions, and to see the names of the players. 


######################### Changes Made #########################
I originally planned to use Stored Procedures in the database to handle more complex requests: for example showing player names rather than playerIds where relevant. This was cut for time.

The player response table now is delete on cascade if either sessions or players are deleted-as those responses are not needed if the player is no longer part of the group or if a session is cancelled.

######################### Working Endpoints #########################

/api/PlayerAttendance:
-GET requests produce a list of player responses
-POST requests with {int playerId, int sessionId, bool availability} allow users to post their             availability to a particular session

/api/Player:
-GET requests produce a list of playerIds and associated names

/api/ProposedSession:
-GET requests produce a list of VIABLE sessions (sessions have a viability boolean that should be flipped when a player indicates they are not available)

-GET requests with a date only parameter produce session info for the given date. Example format: api/ProposedSession/15 Mar 2022. Dates should be given (dd mon YYYY)


######################### Non-Working Endpoints #########################
/api/Player:
-DELETE requests work in theory but are currently blocked

/api/PlayerAttendance:
-PUT requests should allow players to update their availability with {int attendanceId, bool availability} but are blocked along with DELETE requests


######################### Known Bugs #########################

-PUT and DELETE requests are being blocked. Googling indicated this is a web.config issue but I've been unable to find it.

-Basic Response has a model but isn't fully implemented. Struggling to create a list that will accept multiple types to hold the response data.


Response Body (if it worked):
{int statusCode, string statusDescription, list<object> response data}


######################### Future Plans #########################
-Fix bugs (obvi)
-continue to develop endpoints and methods for full functionality
-Add frontend