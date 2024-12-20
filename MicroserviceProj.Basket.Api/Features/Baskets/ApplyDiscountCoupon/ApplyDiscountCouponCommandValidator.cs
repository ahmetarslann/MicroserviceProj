using FluentValidation;

namespace MicroserviceProj.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");


            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        }
    }
}
