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
  
    [InlineData("//[*][!!][r9r]\\n11r9r22*hh*33!!44", 110, "11+22+0+33+44 = 110")]
    [InlineData("//[***]\\n11***22***33", 66, "11+22+33 = 66")]
    [InlineData("//,6", 6, "0+6 = 6")]
    [InlineData("//,\\n2,ff,100", 102, "2+0+100 = 102")]
    [InlineData("//#\\n2#5", 7, "2+5 = 7")]
    [InlineData("2,1001,6", 8, "2+0+6 = 8")]
    [InlineData("1\\n2,3", 6, "1+2+3 = 6")]
    [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78, "1+2+3+4+5+6+7+8+9+10+11+12 = 78")]
    [InlineData("5,", 5, "5+0 = 5")]
    [InlineData("5,tytyt", 5, "5+0 = 5")]
    [InlineData(null, 0, "0 = 0")]
    [InlineData(" ", 0, "0 = 0")]
    [InlineData("1,5000", 1, "1+0 = 1")]
    [InlineData("20", 20, "20 = 20")]
    [Theory]
    public void Add_ReturnsExpectedResult(string input, int expectedResult, string expectedFormula)
    {
        var response = _sut.Add(new AddRequest(input));
        
        Assert.Equal(expectedResult, response.Result);
        Assert.Equal(expectedFormula, response.Formula);
    }

    [InlineData("-3,-5", "Negative numbers are not allowed. Found: -3, -5 (Parameter 'Input')")]
    [InlineData("4,-3", "Negative numbers are not allowed. Found: -3 (Parameter 'Input')")]
    [Theory]
    public void Add_ThrowsOnNegativeNumbers(string input, string expectedExceptionMessage)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _sut.Add(new AddRequest(input));
        });
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}