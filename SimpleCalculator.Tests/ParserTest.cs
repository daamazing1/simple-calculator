namespace SimpleCalculator.Tests;

using SimpleCalculator.Tokenizer;
using SimpleCalculator.Parser;

public class ParserTests
{
    [Test]
    public void Parser_Test_AST()
    {
        var parser = new Parser(new Tokenizer(),"1*2*3+5/2-1+23");
        var actual = (BinaryExpression) parser.Parse();

        Assert.That(actual.Token.TokenType, Is.EqualTo(TokenType.Addition));

        var left = (BinaryExpression) actual.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Multiplication));

        // down left side of tree
        var rightnum = (NumberExpression) left.Right;
        left = (BinaryExpression) left.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Multiplication));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("3"));

        rightnum = (NumberExpression) left.Right;
        var leftnum = (NumberExpression) left.Left;
        Assert.That(leftnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(leftnum.Token.Value, Is.EqualTo("1"));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("2"));

        // down right side of tree
        var right = (BinaryExpression) actual.Right;
        Assert.That(right.Token.TokenType, Is.EqualTo(TokenType.Addition));
        
        rightnum = (NumberExpression) right.Right;
        left = (BinaryExpression) right.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Subtraction));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("23"));

        rightnum = (NumberExpression) left.Right;
        left = (BinaryExpression) left.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Division));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("1"));

        rightnum = (NumberExpression) left.Right;
        leftnum = (NumberExpression) left.Left;
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("2"));
        Assert.That(leftnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(leftnum.Token.Value, Is.EqualTo("5"));
    }
}