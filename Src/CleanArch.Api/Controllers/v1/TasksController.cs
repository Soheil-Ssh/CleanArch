using CleanArch.Api.Contracts.ToDo.Requests;
using CleanArch.Application.Features.ToDo.Commands.CreateToDo;

namespace CleanArch.Api.Controllers.v1;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
public class TasksController(IMediator mediator, IMapper mapper) : BaseController
{
    /// <summary>
    /// Post action for create to do
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    public async Task<IActionResult> CreateToDo(CreateToDoRequest request)
    {
        var result = await mediator
            .Send(new CreateToDoCommand(request.Title, request.Description));
        return result.ToActionResult();
    }
}