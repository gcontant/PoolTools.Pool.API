using Application.Common.Behaviours;
using Application.Pools.Queries.GetPools;
using FluentAssertions;
using FluentValidation;

namespace Application.UnitTests.Behaviours;

//TODO: Change to GetPoolByIdValidatorTests
public class ValidationBehaviourTests
{
    [Fact]
    public async Task ShouldPassValidation()
    {
        var validators =  new List<IValidator<GetPoolByIdQuery>> { new GetPoolByIdQueryValidator() };

        var behaviour = new ValidationBehaviour<GetPoolByIdQuery,PoolDto>(validators);

        var request = new GetPoolByIdQuery { poolId = 1 };

        await behaviour.Handle(request, CancellationToken.None, delegate () { return Task.FromResult(new PoolDto()); });
    }

    [Fact]
    public void ShouldFailValidationForInvalidPoolId()
    {
        var validators = new List<IValidator<GetPoolByIdQuery>> { new GetPoolByIdQueryValidator() };

        var behaviour = new ValidationBehaviour<GetPoolByIdQuery, PoolDto>(validators);

        var request = new GetPoolByIdQuery { poolId = -1 };

        Func<Task> f = async () => { await behaviour.Handle(request, CancellationToken.None, delegate () { return Task.FromResult(new PoolDto()); }); };
        f.Should().ThrowAsync<ValidationException>().WithMessage("PoolId must be greater than or equal to 1.");
    }
}
