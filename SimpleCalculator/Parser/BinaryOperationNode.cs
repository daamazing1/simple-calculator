using SimpleCalculator.Tokenizer;
namespace SimpleCalculator.Parser;

public record BinaryOperationNode(Token Token, INode Left, INode Right) : INode
{
    public object Accept(INodeVisitor visitor)
    {
        return visitor.VisitBinary(Token, (INode) Left, (INode) Right);
    }
}