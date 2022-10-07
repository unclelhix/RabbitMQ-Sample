using FluentAssertions;
using HashGenerator.Shared.Contracts;
using HashGenerator.Shared.Services;
using NSubstitute;
using Xunit;

namespace HashGenerator.Shared.Tests
{
    public class Sha1ServiceTests
    {
        private readonly IDateTimeService _dateTimeService = Substitute.For<IDateTimeService>();
       
        [Fact]
        public void Generate_CreatedAtFromService_DateTimeServiceNow()
        {
            //Arrage
            var count = 10;
            var expectedTime = DateTime.Now;
            var sha1Service = new Sha1Service(_dateTimeService);
            
            _dateTimeService.Now.Returns(expectedTime);

            //Act
            var result = sha1Service.Generate(count);

            //Assert
            result.All(x => x.CreatedOn == expectedTime).Should().Be(true);
        }
    }
}