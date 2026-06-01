namespace CleanArch.Application.Features.ToDo.Commands.CreateToDo;

public record CreateToDoCommand(string Title, string? Description) : IRequest<Result>;