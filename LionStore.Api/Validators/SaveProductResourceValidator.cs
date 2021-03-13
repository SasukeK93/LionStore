using FluentValidation;
using LionStore.Api.Resources.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Validators
{
    public class SaveProductResourceValidator : AbstractValidator<SaveProductResource>
    {
        public SaveProductResourceValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Quantity)
                .NotEmpty();

            RuleFor(p => p.Slug)
                .NotEmpty();

            RuleFor(p => p.Price)
                .NotEmpty();
        }
    }
}
