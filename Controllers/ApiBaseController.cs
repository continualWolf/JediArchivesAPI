using Microsoft.AspNetCore.Mvc;

namespace JediArchives.Controllers;
public class ApiBaseController : ControllerBase {
    /// <summary>
    /// Executes an asynchronous action that returns a result and wraps it in standardized HTTP responses.
    /// Automatically handles common exceptions and generates appropriate status codes.
    /// </summary>
    /// <typeparam name="T">The expected return type of the action.</typeparam>
    /// <param name="action">A delegate representing the asynchronous action to execute.</param>
    /// <returns>
    /// Returns:
    /// - 200 OK with the result if successful.
    /// - 404 Not Found if the result is null.
    /// - 400 Bad Request for argument validation failures.
    /// - 401 Unauthorized for authorization errors.
    /// - 500 Internal Server Error for any unhandled exceptions.
    /// </returns>
    protected async Task<IActionResult> Execute<T>(Func<Task<T>> action) {
        try {
            var result = await action();

            if (result is null) {
                return NotFound(new ProblemDetails {
                    Title = "Resource Not Found",
                    Detail = "The requested resource could not be found.",
                    Status = StatusCodes.Status404NotFound,
                    Instance = HttpContext.Request.Path
                });
            }

            return Ok(result);
        } catch (ArgumentNullException ex) {
            return BadRequest(new ProblemDetails {
                Title = "Bad Request",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path
            });
        } catch (UnauthorizedAccessException) {
            return Unauthorized(new ProblemDetails {
                Title = "Unauthorized",
                Detail = "You do not have permission to access this resource.",
                Status = StatusCodes.Status401Unauthorized,
                Instance = HttpContext.Request.Path
            });
        } catch (Exception ex) {
            return StatusCode(500, new ProblemDetails {
                Title = "Internal Server Error",
                Detail = ex.Message,
                Status = 500,
                Instance = HttpContext.Request.Path
            });
        }
    }

    /// <summary>
    /// Executes an asynchronous action that returns no result and wraps it in standardized HTTP responses.
    /// Automatically handles common exceptions and generates appropriate status codes.
    /// </summary>
    /// <param name="action">A delegate representing the asynchronous void action to execute.</param>
    /// <returns>
    /// Returns:
    /// - 204 No Content if successful.
    /// - 400 Bad Request for argument validation failures.
    /// - 401 Unauthorized for authorization errors.
    /// - 500 Internal Server Error for any unhandled exceptions.
    /// </returns>
    protected async Task<IActionResult> Execute(Func<Task> action) {
        try {
            await action();
            return NoContent();
        } catch (ArgumentNullException ex) {
            return BadRequest(new ProblemDetails {
                Title = "Bad Request",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path
            });
        } catch (UnauthorizedAccessException) {
            return Unauthorized(new ProblemDetails {
                Title = "Unauthorized",
                Detail = "You do not have permission to access this resource.",
                Status = StatusCodes.Status401Unauthorized,
                Instance = HttpContext.Request.Path
            });
        } catch (Exception ex) {
            return StatusCode(500, new ProblemDetails {
                Title = "Internal Server Error",
                Detail = ex.Message,
                Status = 500,
                Instance = HttpContext.Request.Path
            });
        }
    }
}
