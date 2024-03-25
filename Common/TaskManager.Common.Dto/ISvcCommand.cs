using MediatR;


namespace TaskManager.Common.Dto;

public interface ISvcBase<TResponse> : IRequest<TResponse> { }

public interface ISvcCommand<TResponse> : ISvcBase<TResponse>, IRequest<TResponse> { }

public interface ISvcQuery<TResponse> : ISvcBase<TResponse>, IRequest<TResponse> { }

public class EmptyResponseDto
{
    public static EmptyResponseDto Instance { get; } = new EmptyResponseDto();
}