using SimpleCalculator.Parser;
using SimpleCalculator.Tokenizer;

namespace SimpleCalculator.ValueBuilder;

public class ValueBuilder : INodeVisitor
{
    public object VisitNumber(Token numberToken)
    {
        return decimal.Parse(numberToken.Value);
    }

    public object VisitBinary(Token operationToken, INode Left, INode Right)
    {
        switch(operationToken.TokenType)
        {
            case TokenType.Addition:
                return (decimal) Left.Accept(this) + (decimal) Right.Accept(this);
            case TokenType.Subtraction:
                return (decimal) Left.Accept(this) - (decimal) Right.Accept(this);
            case TokenType.Multiplication:
                return (decimal) Left.Accept(this) * (decimal) Right.Accept(this);
            case TokenType.Division:
                return (decimal) Left.Accept(this) / (decimal) Right.Accept(this);
            default:
                throw new Exception($"Token type of {operationToken.TokenType.ToString()} cannot be evaluated.");
        }
    }
}
