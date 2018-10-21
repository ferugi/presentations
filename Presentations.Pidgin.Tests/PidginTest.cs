using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pidgin;
using Xunit;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace Presentations.Pidgin
{
    public static class PidginTest
    {
        public static IEnumerable<string> ParseCsv(string input)
        {
            // Build parser
            Parser<char, char> comma = Char(',');
            Parser<char, string> entry = AnyCharExcept(',').ManyString();
            Parser<char, IEnumerable<string>> entries = entry.SeparatedAndOptionallyTerminated(comma);
            
            // Parse
            var output = entries.ParseOrThrow(input);
            return output;
        }

        [Fact]
        public static void Csv_Parses_Correctly()
        {
            // Arrange
            var stringToParse = "cat,dog,owl,goat,rat";
            var comparableArray = new string[] { "cat", "dog", "owl", "goat", "rat" };

            // Act
            var parsedStringCollection = ParseCsv(stringToParse);

            // Assert
            Assert.Equal(parsedStringCollection.ToArray(), comparableArray);
        }

        public static IEnumerable<string> ParseJsonArrayOfStrings(string input)
        {
            // Build parser
            Parser<char, char> leftBrace = Char('[');
            Parser<char, char> rightBrace = Char(']');
            Parser<char, char> quote = Char('"');
            Parser<char, char> comma = Char(',');
            Parser<char, string> item = AnyCharExcept('"').ManyString().Between(quote).Between(SkipWhitespaces);
            Parser<char, IEnumerable<string>> array = item.SeparatedAndOptionallyTerminated(comma).Between(leftBrace, rightBrace);

            // Parse
            var output = array.ParseOrThrow(input);
            return output;
        }

        [Fact]
        public static void Json_Array_Parses_Correctly()
        {
            // Arrange
            var stringToParse = "[\"cat\", \"dog\", \"owl\", \"goat\", \"rat\"]";
            var comparableArray = new string[] { "cat", "dog", "owl", "goat", "rat" };

            // Act
            var parsedStringCollection = ParseJsonArrayOfStrings(stringToParse);

            // Assert
            Assert.Equal(parsedStringCollection.ToArray(), comparableArray);
        }
    }
}