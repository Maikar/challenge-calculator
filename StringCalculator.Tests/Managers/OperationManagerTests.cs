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
  
    [InlineData("1\\n2,3", 6, "1+2+3 = 6")]
    [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78, "1+2+3+4+5+6+7+8+9+10+11+12 = 78")]
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
}