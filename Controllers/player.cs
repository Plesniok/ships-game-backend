using Microsoft.AspNetCore.Mvc;

using Ships.Model;
using Response.Model;

using Ships.Service;
using Response.Service;

namespace ShipBackend.Controllers;

[ApiController]
[Route("/player")]
public class PlayerApiController : ControllerBase
{
    ShipsService ShipsServiceInstance = new ShipsService();


    [HttpPut("two")]
    public IActionResult Put([FromBody] AddPlayer requestBody)
    {
        if(requestBody.ships.Count() != 10){
            return BadRequest(
                ResponseService.InfoResponse(
                    "player must have 10 ships",
                    "10001"
                )
            );
        } 

        if(requestBody.ships.Count() != requestBody.ships.Distinct().Count()){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Player ships are not unique",
                    "10001"
                )
            );
        }

        if(!ShipsServiceInstance.ifTableExist(requestBody.tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        if(ShipsServiceInstance.ifTableIsLocked(requestBody.tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game is locked",
                    "10002"
                )
            );
        }

        bool serviceStatus = ShipsServiceInstance.UpdatePlayer(
            requestBody.tableName,
            requestBody.playerName,
            requestBody.ships
        );

        if(!serviceStatus){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Add 2nd player service unavailable",
                    "10003"
                )
            );
        }
        return Ok(
            ResponseService.CreateSuccessBodyResponse()
        );
    }
    
    [HttpPut("tour")]
    public IActionResult PutPlayerTour([FromBody] SetPlayerTour requestBody)
    {
        
        if(!ShipsServiceInstance.ifTableExist(requestBody.tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int? ifPlayerExists = ShipsServiceInstance.GetPlayerIdByName(
            requestBody.tableName,
            requestBody.playerName
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

        bool ? serviceStatus = ShipsServiceInstance.UpdatePlayerTour(
            requestBody.tableName,
            ifPlayerExists
        );

        if(serviceStatus == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Add 2nd player service unavailable",
                    "10003"
                )
            );
        }
        return Ok(
            ResponseService.CreateSuccessBodyResponse()
        );
    }

    [HttpGet("tour")]
    public IActionResult GetPlayerTour([FromQuery] string tableName)
    {
        
        if(!ShipsServiceInstance.ifTableExist(tableName)){
            return BadRequest(
                ResponseService.InfoResponse(
                    "Game does not exist",
                    "10002"
                )
            );
        }

        int ? playerWithTour = ShipsServiceInstance.GetPlayerTour(
            tableName
        );

        if(playerWithTour == null){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Get player with tour service unavailable",
                    "10003"
                )
            );
        }
        return Ok(
            ResponseService.GetPlayerWithTour(
                playerWithTour
            )
        );
    }
}
