namespace CleanArch.Application.Features.ToDo.Commands.CreateToDo;

public class CreateToDoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateToDoCommand, Result>
{
    public async Task<Result> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.ToDoRepository.AddAsync(mapper.Map<Core.Entities.ToDo.ToDo>(request));
        await unitOfWork.SaveAsync();
        return Result.Success();
    }
}