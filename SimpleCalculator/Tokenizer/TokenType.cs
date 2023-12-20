using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// Tokens produced when tokenizing input string.
/// Precedence follows:
///     Parentheses have precedence over all operators.
///     * and / have precedence over unary - and binary - and +.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TokenType
{
    None,
    Number,
    Addition,
    Subtraction,
    Multiplication,
    Division,
    OpenParenthesis,
    CloseParenthesis,
    Invalid
};