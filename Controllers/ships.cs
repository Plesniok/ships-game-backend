using Microsoft.AspNetCore.Mvc;

using Ships.Model;
using Response.Model;

using Ships.Service;
using Response.Service;


namespace ShipBackend.Controllers;

[ApiController]
[Route("/ships")]
public class ShipsApiController : ControllerBase
{
    ShipsService ShipsServiceInstance = new ShipsService();
    

    [HttpGet("all/available")]
    public IActionResult GetAvailable(
        [FromQuery] string tableName,
        [FromQuery] string playerName
    )
    {
        if(!ShipsServiceInstance.ifTableExist(tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int? ifPlayerExists = ShipsServiceInstance.GetPlayerIdByName(
            tableName,
            playerName
        );

        if(ifPlayerExists == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Get player id by name service unavailable",
                    "10003"
                )
            );
        }

        if(ifPlayerExists == 0){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Player does not exist in given Game",
                    "10002"
                )
            );
        }

        List<Point> ships = ShipsServiceInstance.GetPlayerAvailableShips(
            tableName,
            ifPlayerExists
        );

        return Ok(
            ResponseService.GetPlayerShips(
                ships
            )
        );
    }

    [HttpGet("all/not-available")]
    public IActionResult GetNotAvailable(
        [FromQuery] string tableName,
        [FromQuery] string playerName
    )
    {
        if(!ShipsServiceInstance.ifTableExist(tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int? ifPlayerExists = ShipsServiceInstance.GetPlayerIdByName(
            tableName,
            playerName
        );

        if(ifPlayerExists == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Get player id by name service unavailable",
                    "10003"
                )
            );
        }

        if(ifPlayerExists == 0){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Player does not exist in given Game",
                    "10002"
                )
            );
        }

        List<Point> ships = ShipsServiceInstance.GetPlayerAvailableShips(
            tableName,
            ifPlayerExists
        );

        return Ok(
            ResponseService.GetPlayerShips(
                ships
            )
        );
    }

    [HttpPut("destroy")]
    public IActionResult DestroyShip(
        [FromBody] DestroyShip requestData 
    )
    {
        if(!ShipsServiceInstance.ifTableExist(requestData.tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int? ifPlayerExists = ShipsServiceInstance.GetPlayerIdByName(
            requestData.tableName,
            requestData.playerName
        );

        if(ifPlayerExists == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Get player id by name service unavailable",
                    "10003"
                )
            );
        }

        if(ifPlayerExists == 0){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Player does not exist in given Game",
                    "10002"
                )
            );
        }
        int? enemyPlayerId = ShipsServiceInstance.GetEnemyPlayer(
            ifPlayerExists
        );

        int? shipsDestroyed = ShipsServiceInstance.DestroyEnemyShip(
            requestData.tableName,
            enemyPlayerId,
            requestData.ship
        );

        if(shipsDestroyed == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Destroy enemy ship service unavailable",
                    "10003"
                )
            );
        }
        if(shipsDestroyed == 0){
            bool ? shipMissed = ShipsServiceInstance.AssignMissShot(
                requestData.tableName,
                enemyPlayerId,
                requestData.ship
            );

            if(shipMissed == null){
                return StatusCode(
                    503,
                    ResponseService.InfoResponse(
                        "Assign miss service unavailable",
                        "10003"
                    )
                );
            }

            return Ok(
                ResponseService.InfoResponse(
                    "You missed",
                    "10000"
                )
            );
        }

        return Ok(
            ResponseService.CreateSuccessBodyResponse()
        );
    }
    
    [HttpGet("all/missed")]
    public IActionResult GetAllMissed(
        [FromQuery] string tableName,
        [FromQuery] string playerName
    )
    {
        if(!ShipsServiceInstance.ifTableExist(tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int? ifPlayerExists = ShipsServiceInstance.GetPlayerIdByName(
            tableName,
            playerName
        );

        if(ifPlayerExists == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Get player id by name service unavailable",
                    "10003"
                )
            );
        }

        if(ifPlayerExists == 0){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Player does not exist in given Game",
                    "10002"
                )
            );
        }

        List<Point> ships = ShipsServiceInstance.GetPlayerMissedShips(
            tableName,
            ifPlayerExists
        );

        return Ok(
            ResponseService.GetPlayerShips(
                ships
            )
        );
    }
}
