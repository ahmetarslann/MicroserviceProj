using FluentValidation;

namespace MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator:AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator() 
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        }
    }
}
