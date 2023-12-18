namespace SimpleCalculator.Tokenizer;

public record Token(TokenType TokenType, string Value, int Precedence);
