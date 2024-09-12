using MediatR;

namespace RamSoft.Application.Features.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
