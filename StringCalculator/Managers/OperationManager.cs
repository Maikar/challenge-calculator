using StringCalculator.Models;

namespace StringCalculator.Managers;

public class OperationManager : IOperationManager
{
    public AddResponse Add(AddRequest request)
    {
        var normalizedInput = request.Input?.Replace("\\n", ",");
        var numbers = GetRawNumbers(normalizedInput);

        var result = numbers.Sum();
        var formula = $"{string.Join("+", numbers)} = {result}";
        
        return new AddResponse(result, formula);
    }

    private int[] GetRawNumbers(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return [0];
        }
        
        var values = input.Split(',');

        return values.Select(item => int.TryParse(item, out var parseResult) ? parseResult : 0)
            .ToArray();
    }
}