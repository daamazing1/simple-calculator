namespace SimpleCalculator.Tests;

using System.Runtime.CompilerServices;
using SimpleCalculator.Parser;
using SimpleCalculator.Tokenizer;
using SimpleCalculator.ValueBuilder;

public class ValueBuilderTest
{
    [Test]
    public void VisitNumber_Returns_Decimal()
    {
        var builder = new ValueBuilder();
        var actual = builder.VisitNumber(new Token(TokenType.Number, "100", 1));
        Assert.That(actual, Is.EqualTo(100M));
        actual = builder.VisitNumber(new Token(TokenType.Number, "100.12", 1));
        Assert.That(actual, Is.EqualTo(100.12M));
    }

    [Test]
    public void VisitBinary_Addition_Returns_Sum()
    {
        var builder = new ValueBuilder();
        var actual = builder.VisitBinary(
            new Token(TokenType.Addition, "+", 1),
            new NumberNode(new Token(TokenType.Number, "2" ,1)), 
            new NumberNode(new Token(TokenType.Number, "5", 1)));
        Assert.That(actual, Is.EqualTo(7M));        
    }

    [Test]
    public void VisitBinary_Subtraction_Returns_Difference()
    {
        var builder = new ValueBuilder();
        var actual = builder.VisitBinary(
            new Token(TokenType.Subtraction, "-", 1),
            new NumberNode(new Token(TokenType.Number, "10" ,1)), 
            new NumberNode(new Token(TokenType.Number, "5", 1)));
        Assert.That(actual, Is.EqualTo(5M));        
    }

    [Test]
    public void VisitBinary_Multiplication_Returns_Product()
    {
        var builder = new ValueBuilder();
        var actual = builder.VisitBinary(
            new Token(TokenType.Multiplication, "*", 1),
            new NumberNode(new Token(TokenType.Number, "10" ,1)), 
            new NumberNode(new Token(TokenType.Number, "5", 1)));
        Assert.That(actual, Is.EqualTo(50M));        
    }

    [Test]
    public void VisitBinary_Division_Returns_Quotient()
    {
        var builder = new ValueBuilder();
        var actual = builder.VisitBinary(
            new Token(TokenType.Division, "/", 1),
            new NumberNode(new Token(TokenType.Number, "10" ,1)), 
            new NumberNode(new Token(TokenType.Number, "5", 1)));
        Assert.That(actual, Is.EqualTo(2M));        
    }
}
