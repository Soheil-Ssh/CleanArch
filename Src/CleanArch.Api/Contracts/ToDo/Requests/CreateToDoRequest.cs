namespace CleanArch.Api.Contracts.ToDo.Requests;

public record CreateToDoRequest(string Title, string? Description);