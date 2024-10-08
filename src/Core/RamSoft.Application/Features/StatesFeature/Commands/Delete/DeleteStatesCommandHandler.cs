﻿using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Commands.Delete
{

    public class DeleteStatesCommandHandler : ICommandHandler<DeleteStatesCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteStatesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteStatesCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteStatesValidation(_unitOfWork.StatesRepository, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {

                await _unitOfWork.StatesRepository.Delete(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
