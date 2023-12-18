namespace SimpleCalculator.Tokenizer;

public interface ITokenizer
{
    public IEnumerable<Token> Tokenize(string input);
}
