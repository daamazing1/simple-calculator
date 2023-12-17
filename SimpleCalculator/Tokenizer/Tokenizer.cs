namespace SimpleCalculator.Tokenizer;

public class Tokenizer
{
    private List<TokenDefinition> _tokenDefinitions = new List<TokenDefinition>();
    public Tokenizer()
    {
        // Configure the token defintions, as more functionality this list will grow
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Number,@"(?<!-?\d+(?:\.\d+)?)-?\d+(?:\.\d+)?"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Addition, @"\+"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Subtraction, @"\-"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Multiplication, @"\*"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Division, @"\/"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, @"\("));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, @"\)"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.None, @"\s+"));
    }

    /// <summary>
    /// Take a string expression and return it as tokens
    /// </summary>
    /// <param name="input">string expression</param>
    /// <returns>Tokens</returns>
    public IEnumerable<Token> Tokenize(string input)
    {
        var tokenMatches = FindTokenMatches(input).OrderBy(t=>t.StartIndex).ToList();
        
        TokenMatch lastMatch = new TokenMatch(TokenType.None, string.Empty,0,0);
        for (int i = 0; i < tokenMatches.Count; i++)
        {
            var bestMatch = tokenMatches[i];
            if (lastMatch != null && bestMatch.StartIndex < lastMatch.EndIndex)
                continue;

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
