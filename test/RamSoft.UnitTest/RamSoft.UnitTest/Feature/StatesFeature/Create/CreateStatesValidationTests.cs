using Bogus;
using RamSoft.Application.Features.StatesFeature.Commands.Create;

namespace RamSoft.UnitTest.Feature.StatesFeature.Create
{
    public class CreateStatesValidationTests
    {
        CreateStatesValidation _validator;
        public CreateStatesValidationTests()
        {
            _validator = new CreateStatesValidation();
        }

        [Test]
        public async Task CreateStatesValidation_HappyScenario()
        {
            var data = new CreateStatesCommand() { Name = new Faker().Random.String2(100) };
            var validationResult = await _validator.ValidateAsync(data);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("             ")]
        public async Task Name_Is_Null_OR_WhiteSpace_Check_NumberOfErrors(string? name)
        {
            var data = new CreateStatesCommand() { Name = name };
            var validationResult = await _validator.ValidateAsync(data);
            Assert.IsFalse(validationResult.IsValid);
            Assert.That(1 == validationResult.Errors.Count);
        }

        [Test]
        public async Task Name_Is_Greater_Than_100_Check_NumberOfErrors()
        {
            var data = new CreateStatesCommand() { Name = new Faker().Random.String2(101) };
            var validationResult = await _validator.ValidateAsync(data);
            Assert.IsFalse(validationResult.IsValid);
            Assert.That(1 == validationResult.Errors.Count);
        }
    }
}
