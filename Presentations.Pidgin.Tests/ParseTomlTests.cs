using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

// Mention usings

namespace Presentations.Pidgin.Tests
{
    public static class ParseTomlTests
    {
        public static string ParseTomlComment(string input)
        {
            // Build parser
            Parser<char, char> hash = Char('#');
            Parser<char, string> comment = hash
                .Then(SkipWhitespaces.Then(AnyCharExcept('\r', '\n').ManyString()));

            // Parse
            var output = comment.ParseOrThrow(input);
            return output;
        }

        [Fact]
        public static void Toml_Comment_Parses_Correctly()
        {
            // Arrange
            var stringToParse = "# This is a TOML document.\n";
            var expectedComment = "This is a TOML document.";

            // Act
            var actualComment = ParseTomlComment(stringToParse);

            // Assert
            Assert.Equal(expectedComment, actualComment);
        }
    }

    public interface IToml
    {
    }
}
