using Xunit;

namespace StudentProjectPlanner.Tests;

/// <summary>
/// Example test class - demonstrates project structure
/// </summary>
public class ExampleTests
{
    [Fact]
    public void Example_Test_Passes()
    {
        // Arrange
        var expected = 2;

        // Act
        var actual = 1 + 1;

        // Assert
        Assert.Equal(expected, actual);
    }
}
