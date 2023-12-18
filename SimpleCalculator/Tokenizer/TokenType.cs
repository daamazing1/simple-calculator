using System.Text.RegularExpressions;

/// <summary>
/// Tokens produced when tokenizing input string.
/// Precedence follows:
///     Parentheses have precedence over all operators.
///     * and / have precedence over unary - and binary - and +.
/// </summary>
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