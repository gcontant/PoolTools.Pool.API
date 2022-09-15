using FluentValidation;

namespace Application.Pools.Queries.GetPools;

public class GetPoolByIdQueryValidator : AbstractValidator<GetPoolByIdQuery>
{
    public GetPoolByIdQueryValidator()
    {
        RuleFor(q => q.poolId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PoolId must be greater than or equal to 1.");
    }
}
