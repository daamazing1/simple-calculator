namespace SimpleCalculator.Tokenizer;

using System.Linq.Expressions;

public class Calculator : ICalculator
{
    public string EvaluateExpression(Expression expression)
    {
        throw new NotImplementedException();
    }
}

public interface ICalculator{
    string EvaluateExpression(Expression expression);
}