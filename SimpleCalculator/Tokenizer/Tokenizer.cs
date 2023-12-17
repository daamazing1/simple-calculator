namespace SimpleCalculator.Tokenizer;

/// <summary>
/// Tokenization breaks expression into smaller parts for easier analysis by parser.
/// </summary>
public class Tokenizer
{
    private List<TokenDefinition> _tokenDefinitions = new List<TokenDefinition>();

    /// <summary>
    /// Constructor, also where the initial token definitions will be created.
    /// </summary>
    public Tokenizer()
    {
        // Configure the token defintions, as more functionality this list will grow
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Number,@"(?<!-?\d+(?:\.\d+)?)-?\d+(?:\.\d+)?",4));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Addition, @"\+",3));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Subtraction, @"\-",3));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Multiplication, @"\*",2));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Division, @"\/",2));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, @"\(", 1));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, @"\)", 1));
    }

    /// <summary>
    /// Take a string expression and return it as tokens
    /// </summary>
    /// <param name="input">string expression</param>
    /// <returns>Tokens</returns>
    public IEnumerable<Token> Tokenize(string input)
    {
        var tokenMatches = FindTokenMatches(input).OrderBy(t=>t.StartIndex).ToList();
        // check for any not matched tokens
        
        
        TokenMatch lastMatch = new TokenMatch(TokenType.None, string.Empty,0,0);
        for (int i = 0; i < tokenMatches.Count; i++)
        {
            var bestMatch = tokenMatches[i];

            // skip matches that have already been captured by other rules
            if (bestMatch.StartIndex < lastMatch.EndIndex)
                continue;
            // identify area of input string with a gap, so there were no matches for these.
            if (bestMatch.StartIndex > lastMatch.EndIndex)
                yield return new Token(TokenType.Invalid, input.Substring(lastMatch.EndIndex, bestMatch.StartIndex-lastMatch.EndIndex));

            yield return new Token(bestMatch.TokenType, bestMatch.Value);

            lastMatch = bestMatch;
        }

        yield return new Token(TokenType.None,string.Empty);
    }

    /// <summary>
    /// Helper method that loops through all token defintions and returns an unordered list of token matches
    /// </summary>
    /// <param name="input">String expression</param>
    /// <returns>List of token matches</returns>
    private List<TokenMatch> FindTokenMatches(string input)
    {
        var tokenMatches = new List<TokenMatch>();
        foreach(var tokenDefinition in _tokenDefinitions)
            tokenMatches.AddRange(tokenDefinition.FindMatches(input).ToList());
        
        return tokenMatches;
    }
}
