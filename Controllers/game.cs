using Microsoft.AspNetCore.Mvc;

using Ships.Model;
using Response.Model;

using Ships.Service;
using Response.Service;
namespace ShipBackend.Controllers;


[ApiController]
[Route("/game")]
public class GameApiController : ControllerBase
{
    ShipsService ShipsServiceInstance = new ShipsService();

    [HttpPost()]
    public IActionResult Post([FromBody] CreateGame requestBody)
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

        TableName serviceResult = ShipsServiceInstance
            .CreateGame(requestBody.playerName, requestBody.ships);
        if(serviceResult == new TableName()){
            return StatusCode(
                503,
                ResponseService.InfoResponse(
                    "Create game service unavailable",
                    "10001"
                )
            );
        }
        return Ok(
            ResponseService.CreateGameResponse(
                serviceResult.tableName
            )
        );
    }
}
