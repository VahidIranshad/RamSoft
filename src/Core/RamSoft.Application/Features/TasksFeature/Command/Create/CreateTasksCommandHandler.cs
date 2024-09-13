using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TasksFeature.Command.Create
{
    public class CreateTasksCommandHandler : ICommandHandler<CreateTasksCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTasksCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTasksCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTasksValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<Tasks>(request);

                var result = await _unitOfWork.TasksRepository.Add(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result.Id;
            }
        }
    }
}
