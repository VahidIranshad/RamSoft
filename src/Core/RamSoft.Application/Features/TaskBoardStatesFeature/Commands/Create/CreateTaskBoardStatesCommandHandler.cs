using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create
{
    public class CreateTaskBoardStatesCommandHandler : ICommandHandler<CreateTaskBoardStatesCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskBoardStatesCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTaskBoardStatesCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTaskBoardStatesValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<TaskBoardStates>(request);
                var result = await _unitOfWork.TaskBoardStatesRepository.Add(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result.Id;
            }
        }
    }
}
