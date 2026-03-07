using MediatR;
using TelecomBillingAndConsumption.Infrastructure.InfrastructureBases;

namespace TelecomBillingAndConsumption.Core.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IGenericRepository<object> _repository;

        public TransactionBehavior(IGenericRepository<object> repository)
        {
            _repository = repository;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            using var transaction = _repository.BeginTransaction();

            try
            {
                var response = await next();

                transaction.Commit();

                return response;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
