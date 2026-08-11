using FluentAssertions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Tests.Domain.ValueObjects
{
    public class StateUnitTest
    {
        [Fact]
        public void State_ShouldBeValid_WhenGivenValidState()
        {
            // Arrange
            var validState = "SP";
            // Act
            var state = new State(validState);
            // Assert
            state.Value.Should().Be(validState);
        }

        [Fact]
        public void State_ShouldThrowException_WhenGivenInvalidState()
        {
            // Arrange
            var invalidState = "BBB";
            // Act
            Action act = () => new State(invalidState);
            // Assert
            act.Should().Throw<Exception>();
        }
    }
}
