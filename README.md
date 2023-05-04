
The TTRPG scheduler is a web API that allows users to propose session dates, allows players to post their availability for those sessions, and to see the names of the players. 


######################### Changes Made #########################

I originally planned to use Stored Procedures in the database to handle more complex requests: for example showing player names rather than playerIds where relevant. This was cut for time.

The player response table now is delete on cascade if either sessions or players are deleted-as those responses are not needed if the player is no longer part of the group or if a session is cancelled.

Currently only specific endpoints are working. The full application will take many more endpoints.

######################### Example Response Body #########################

{
    "statusCode": 200,
    "statusDescription": "Post Attendance  Request succeeded",
    "sdata": null,
    "pdata": null,
    "adata": {
        "attendanceId": 6,
        "sessionId": 5,
        "playerId": 3,
        "availability": true
    }
}

######################### Working Endpoints #########################

/api/PlayerAttendance:
-GET requests produce a list of player responses

-POST requests with {int playerId, int sessionId, bool availability} allow users to post their availability to a particular session

/api/Player:
-GET requests produce a list of playerIds and associated names

-DELETE requests delete a player and associate attendance responses. Request must send playerId in the body of the player to be deleted.

/api/ProposedSession:
-GET requests produce a list of VIABLE sessions (sessions have a viability boolean that should be flipped when a player indicates they are not available)

-GET requests with a date only parameter produce session info for the given date. Example format: api/ProposedSession/15 Mar 2022. Dates should be given (dd mon YYYY)


######################### Non-Working Endpoints #########################

/api/PlayerAttendance:
-PUT requests allows players to update their availability with {int attendanceId, bool availability}. 
Currently says it succeeds but does not actually update. 



######################### Future Plans #########################

-Fix bugs (obvi)

-Add duplicate protection for proposed sessions

-continue to develop endpoints and methods for full functionality

-Add frontend
