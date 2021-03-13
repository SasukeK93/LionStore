using FluentValidation;
using LionStore.Api.Resources.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Validators
{
    public class BuyProductResourceValidator : AbstractValidator<BuyProductResource>
    {
        public BuyProductResourceValidator()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty();

            RuleFor(p => p.Quantity)
                .NotEmpty();
        }
    }
}
