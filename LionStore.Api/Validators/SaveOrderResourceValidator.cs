using FluentValidation;
using LionStore.Api.Resources.Order;
using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Validators
{
    public class SaveOrderResourceValidator : AbstractValidator<SaveOrderResource>
    {
        public SaveOrderResourceValidator()
        {
            RuleFor(p => p.Status)
                .NotEqual(EOrderStatus.Created);
        }
    }
}
