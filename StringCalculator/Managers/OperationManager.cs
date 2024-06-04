using StringCalculator.Models;

namespace StringCalculator.Managers;

public class OperationManager : IOperationManager
{
    private const string CustomDelimiterStart = "//";
    private const string CustomDelimiterEnd = "\\n";
    
    public AddResponse Add(AddRequest request)
    {
        var normalizedInput = NormalizeInput(request.Input);
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

    private string? NormalizeInput(string? input)
    {
        // Handle custom delimiters. Must start with delimiter declaration and the ending or we will just treat this
        // as an invalid number later
        var delimiterEnd = input?.IndexOf(CustomDelimiterEnd, StringComparison.Ordinal) ?? 0;
        if (!string.IsNullOrWhiteSpace(input) && input.StartsWith(CustomDelimiterStart) && delimiterEnd > 0)
        {
            var delimiterDeclaration = input.Substring(CustomDelimiterStart.Length, delimiterEnd - CustomDelimiterEnd.Length);
            var numbers = input.Substring(CustomDelimiterStart.Length + delimiterDeclaration.Length + CustomDelimiterEnd.Length);
            
            delimiterDeclaration = delimiterDeclaration.TrimStart('[').TrimEnd(']');

            return numbers.Replace(delimiterDeclaration, ",");
        }
        
        // default behavior
        return input?.Replace("\\n", ",");
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