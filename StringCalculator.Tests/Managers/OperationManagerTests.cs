using StringCalculator.Managers;
using StringCalculator.Models;

namespace StringCalculator.Tests.Managers;

public class OperationManagerTests
{
    private readonly IOperationManager _sut;

    public OperationManagerTests()
    {
        _sut = new OperationManager();
    }
    
    [InlineData("5,", 5, "5+0 = 5")]
    [InlineData("5,tytyt", 5, "5+0 = 5")]
    [InlineData("4,-3", 1, "4+-3 = 1")]
    [InlineData((string?)null, 0, "0 = 0")]
    [InlineData(" ", 0, "0 = 0")]
    [InlineData("1,5000", 5001, "1+5000 = 5001")]
    [InlineData("20", 20, "20 = 20")]
    [Theory]
    public void Add_ReturnsExpectedResult(string input, int expectedResult, string expectedFormula)
    {
        var response = _sut.Add(new AddRequest(input));
        
        Assert.Equal(expectedResult, response.Result);
        Assert.Equal(expectedFormula, response.Formula);
    }

    [Fact]
    public void Add_MoreThan2_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            _sut.Add(new AddRequest("1,1,1"));
        });
    }
}