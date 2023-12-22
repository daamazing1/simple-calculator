using System.Text.Json.Serialization;
using SimpleCalculator.Tokenizer;

namespace SimpleCalculator.Parser;

/// <summary>
/// Interface to support Visitor behavior pattern, this is added to all the tree nodes.
/// </summary>
public interface INode
{
    object Accept(INodeVisitor visitor);
}

/// <summary>
/// Interface to Suppoirt behavior pattern. There are two types of nodes for the 
/// AST, a number which is a leaf or binary operation which is an node with a degree of 2.
/// </summary>
public interface INodeVisitor
{
    object VisitNumber(Token numberToken);
    object VisitBinary(Token operationToken, INode Left, INode Right);
}
