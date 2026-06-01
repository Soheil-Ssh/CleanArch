using CleanArch.Application.Features.ToDo.Commands.CreateToDo;
using CleanArch.Core.Entities.ToDo;

namespace CleanArch.Application.Mappers;

public class ToDoMapperProfile : Profile
{
    public ToDoMapperProfile()
    {
        // Map CreateToDoRequest => CreateToDoCommand
        //CreateMap<CreateToDoRequest, CreateToDoCommand>();
        // Map CreateToDoCommand => ToDo
        CreateMap<CreateToDoCommand, ToDo>();
    }
}