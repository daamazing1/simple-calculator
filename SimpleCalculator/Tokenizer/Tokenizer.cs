namespace SimpleCalculator.Tokenizer;

/// <summary>
/// Tokenization breaks expression into smaller parts for easier analysis by parser.
/// </summary>
public class Tokenizer : ITokenizer
{
    private List<TokenDefinition> _tokenDefinitions = new List<TokenDefinition>();
    private ILogger _logger;

    /// <summary>
    /// Constructor, also where the initial token definitions will be created.
    /// </summary>
    public Tokenizer(ILogger logger)
    {
        _logger = logger;
        _logger.LogDebug($"method Tokenizer::ctor, building definitions start.");
        // Configure the token defintions, as more functionality this list will grow.
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Number,@"(?<!-?\d+(?:\.\d+)?)-?\d+(?:\.\d+)?",1));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Addition, @"\+",2));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Subtraction, @"\-",2));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Multiplication, @"\*",3));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Division, @"\/",3));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, @"\(", 4));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, @"\)", 4));
        _logger.LogDebug($"method Tokenizer::ctor, building definitions end.");
    }

    /// <summary>
    /// Take a string expression and return it as tokens
    /// </summary>
    /// <param name="input">string expression</param>
    /// <returns>Tokens</returns>
    public IEnumerable<Token> Tokenize(string input)
    {
        var tokenMatches = FindTokenMatches(input).OrderBy(t=>t.StartIndex).ToList();
        _logger.LogDebug($"Tokenizer::Tokenize, Found token matches: {tokenMatches.Count}");
        
        TokenMatch lastMatch = new TokenMatch(TokenType.None, string.Empty,0,0,1);
        for (int i = 0; i < tokenMatches.Count; i++)
        {
            var bestMatch = tokenMatches[i];

            // skip matches that have already been captured by other rules
            if (bestMatch.StartIndex < lastMatch.EndIndex)
                continue;
            // identify area of input string with a gap, so there were no matches for these.
            if (bestMatch.StartIndex > lastMatch.EndIndex)
                yield return new Token(TokenType.Invalid, input.Substring(lastMatch.EndIndex, bestMatch.StartIndex-lastMatch.EndIndex), 1);

            yield return new Token(bestMatch.TokenType, bestMatch.Value, bestMatch.Precedence);

            lastMatch = bestMatch;
        }

        yield return new Token(TokenType.None,string.Empty, 1);
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
