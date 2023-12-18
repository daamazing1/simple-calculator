using SimpleCalculator.Tokenizer;

namespace SimpleCalculator.Parser;

public record NumberExpression (Token Token): IExpression;
public record BinaryExpression (Token Token, IExpression Left, IExpression Right) : IExpression;