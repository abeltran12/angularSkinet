using System.Security.Claims;
using angularSkinet.Api.Controllers;
using angularSkinet.Api.Requests;
using angularSkinet.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return Problem(
            title: "Invalid input",
            detail: "The provided parameters are invalid.",
            statusCode: StatusCodes.Status400BadRequest
        );
    }

    [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("internalerror")]
    public IActionResult GetInternalError()
    {
        throw new Exception("This is a test exception");
    }

    [HttpPost("validationerror")]
    public IActionResult GetValidationError(ProductRequest product)
    {
        return Ok();
    }
}