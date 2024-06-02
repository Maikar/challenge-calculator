using StringCalculator.Models;

namespace StringCalculator.Managers;

public interface IOperationManager
{
    AddResponse Add(AddRequest request);
}