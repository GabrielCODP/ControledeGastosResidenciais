using CommonTestUtilities.Requests.Category;
using FluentAssertions;
using MyExpenseControl.Application.UseCases.Category.Register;
using MyExpenseControl.Exceptions;

namespace Validators.Test.Category
{
    public class RegisterCategoryValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterCategoryValidator();

            var request = RequestRegisterCategoryJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error__When_Description_Is_Too_Long()
        {
            var validator = new RegisterCategoryValidator();

            var request = RequestRegisterCategoryJsonBuilder.Build();
            request.Description = new string('a', 500);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CATEGORY_DESCRIPTION_MAX_LENGTH_400));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Error_Invalid_Description(string description)
        {
            var validator = new RegisterCategoryValidator();

            var request = RequestRegisterCategoryJsonBuilder.Build();
            request.Description = description;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CATEGORY_EMPTY));
        }

        [Fact]
        public void Error_Invalid_Purpose()
        {
            var validator = new RegisterCategoryValidator();

            var request = RequestRegisterCategoryJsonBuilder.Build();
            request.Purpose = (MyExpenseControl.Communication.Enums.Purpose)10;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PURPOSE_NOT_SUPPORTED));
        }
    }
}
