using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Create
{
    public class CreateTaskBoardCommandHandler : ICommandHandler<CreateTaskBoardCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskBoardCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTaskBoardCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTaskBoardValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<TaskBoard>(request);
                data.TaskBoardStateList.Add(new TaskBoardStates() { StatesId = request.DefaultStatesId } );

                var result = await _unitOfWork.TaskBoardRepository.Add(data, cancellationToken);
                //var taskBoardEntity = new TaskBoardStates() { TaskBoardId = result.Id, StatesId = request.DefaultStatesId };
                //await _unitOfWork.TaskBoardStatesRepository.Add(taskBoardEntity, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result.Id;
            }
        }
    }
}
