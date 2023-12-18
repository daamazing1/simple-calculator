namespace SimpleCalculator.Tokenizer;

using System.Text.RegularExpressions;

/// <summary>
/// Defines the token defintion and provides a utility function to find all matches based on 
/// defintion pattern.
/// </summary>
public class TokenDefinition
{
    private Regex _regex;
    private readonly TokenType _returnsTokenType;
    private readonly int _precedence;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="returnsTokenType">TokenType</param>
    /// <param name="pattern">RegEx Pattern</param>
    public TokenDefinition(TokenType returnsTokenType, string pattern, int precedence)
    {
        _regex = new Regex(pattern, RegexOptions.IgnoreCase|RegexOptions.Compiled);
        _returnsTokenType = returnsTokenType;
        _precedence = precedence;
    }

    /// <summary>
    /// Find all the matches in the input string globally for the token type
    /// </summary>
    /// <param name="input">input string</param>
    /// <returns>Matches based on the regex pattern for TokenType</returns>
    public IEnumerable<TokenMatch> FindMatches(string input)
    {
        var matches = _regex.Matches(input);
        for(int i=0; i<matches.Count; i++)
        {
            yield return new TokenMatch(_returnsTokenType, matches[i].Value, matches[i].Index, matches[i].Index + matches[i].Length, _precedence);
        }
    }    
}
