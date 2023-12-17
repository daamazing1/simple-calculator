using System.Text.RegularExpressions;

/// <summary>
/// Holds immuntable data for token matches
/// </summary>
/// <param name="TokenType">TokenType</param>
/// <param name="Value">Value</param>
/// <param name="StartIndex">Start position in input string</param>
/// <param name="EndIndex">End position in input string</param>
public record TokenMatch(TokenType TokenType, string Value, int StartIndex, int EndIndex);
