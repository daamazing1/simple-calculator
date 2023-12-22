using System.Xml;
using SimpleCalculator.Tokenizer;

namespace SimpleCalculator.Parser;

/// <summary>
/// Shunting yard parser that returns an abstract syntax tree. 
/// The idea of the shunting yard algorithm is to keep operators on a stack until both their operands have been parsed. The operands are kept on a second stack. 
/// </summary>
public class Parser
{
    private readonly ITokenizer _tokenizer;
    private readonly string _input;
    private readonly IEnumerator<Token> _tokens;
    private readonly Stack<INode> _operands = new Stack<INode>();
    private readonly Stack<Token> _operators = new Stack<Token>();
    private ILogger _logger;
    
    /// <summary>
    /// Constructor taking in a tokenizer implementation and input string
    /// </summary>
    /// <param name="tokenizer">Tokenizer</param>
    /// <param name="input">Input string</param>
    public Parser(ITokenizer tokenizer, ILogger logger, string input)
    {
        _logger = logger;
        _tokenizer = tokenizer;
        _input = input;
        _tokens = _tokenizer.Tokenize(_input).GetEnumerator();
    }

    /// <summary>
    /// Parse the result of the Tokenizer and return the abstract syntax tree.
    /// </summary>
    /// <returns>Abstract syntax tree</returns>
    /// <exception cref="Exception"></exception>
    public INode Parse()
    {
        while(_tokens.MoveNext())
        {
            _logger.LogDebug($"Parser::Parse, Current Token: {_tokens.Current.TokenType}, Token Value: {_tokens.Current.Value}");
            switch(_tokens.Current.TokenType)
            {
                case TokenType.Number:
                    _operands.Push(new NumberNode(_tokens.Current));
                    break;
                case TokenType.Addition:
                case TokenType.Multiplication:
                case TokenType.Division:
                case TokenType.Subtraction:
                    // compare the precedence of the operator with the previous operator in the stack
                    if(_operators.TryPeek(out var previousOperator))
                    {
                        if(_tokens.Current.Precedence > previousOperator.Precedence)
                        {
                            _operators.Push(_tokens.Current);
                        }
                        else
                        {
                            var right = _operands.Pop();
                            var left = _operands.Pop();
                            var op = _operators.Pop();
                            _operands.Push(new BinaryOperationNode(op, left, right));
                            _operators.Push(_tokens.Current);
                        }
                    }
                    else
                    {
                       // no previous operator
                       _operators.Push(_tokens.Current);
                    }
                    break;
                case TokenType.None:
                    // we are done with parsing, pop the stacks until operator stack is empty
                    while(_operators.Count>0)
                    {
                        var right = _operands.Pop();
                        var left = _operands.Pop();
                        var op = _operators.Pop();
                        _operands.Push(new BinaryOperationNode(op, left, right));
                    }
                    break;
                case TokenType.Invalid:
                    // throw and exception 
                    throw new InvalidTokenException($"Invalid input: {_tokens.Current.Value}");
            }    
        }
        return _operands.Peek();
    }
}
