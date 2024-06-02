using StringCalculator.Models;

namespace StringCalculator.Managers;

public class OperationManager : IOperationManager
{
    public AddResponse Add(AddRequest request)
    {
        var normalizedInput = request.Input?.Replace("\\n", ",");
        var numbers = GetRawNumbers(normalizedInput);

        var negativeNumbers = numbers.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(request.Input), $"Negative numbers are not allowed. Found: {string.Join(", ",negativeNumbers)}");
        }

        numbers = ReplaceInvalidNumbers(numbers).ToArray();
        
        var result = numbers.Sum();
        var formula = $"{string.Join("+", numbers)} = {result}";
        
        return new AddResponse(result, formula);
    }

    private IEnumerable<int> ReplaceInvalidNumbers(int[] numbers)
    {
        foreach (var number in numbers)
        {
            if (number > 1000)
            {
                // Invalid numbers should be converted to 0
                yield return 0;
            }
            else
            {
                yield return number;
            }

        }
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