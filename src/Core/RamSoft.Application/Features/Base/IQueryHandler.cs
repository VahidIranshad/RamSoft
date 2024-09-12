using MediatR;

namespace RamSoft.Application.Features.Base
{
    public interface IQueryHandler<in TQuery, TResponse>
     : IRequestHandler<TQuery, TResponse>
     where TQuery : IQuery<TResponse>
     where TResponse : notnull
    {
    }
}
