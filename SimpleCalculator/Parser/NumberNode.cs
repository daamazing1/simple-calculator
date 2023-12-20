using SimpleCalculator.Tokenizer;

namespace SimpleCalculator.Parser;

public record NumberNode(Token Token) : INode
{
    public object Accept(INodeVisitor visitor)
    {
        return visitor.VisitNumber(Token);
    }
}
