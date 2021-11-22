using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Discogs.Api.Core.Models.Extensions;

public static class ModelExtensions
{
    private const string Separator = ", ";
    private const string Add = " + ";

    public static string MapDescription(this Artist[] artists)
    {
        var artistDescription = new StringBuilder();

        for (var index = 0; index < artists.Length; index++)
        {
            var name = string.IsNullOrWhiteSpace(artists[index].anv) ? artists[index].name : artists[index].anv;

            artistDescription.Append(name.StripSuffix());

            if (artists.Length > 1 && index < artists.Length - 1)
            {
                if (!string.IsNullOrWhiteSpace(artists[index].join))
                    artistDescription.Append(" " + artists[index].join + " ");
            }
        }

        return artistDescription.ToString();
    }

    public static string MapDescription(this Label[] labels)
    {
        return string.Join(Separator, labels.Select(label => StripSuffix(label.name)));
    }

    public static string MapDescription(this Format[] formats)
    {
        var formatDescription = new StringBuilder();

        for (var index = 0; index < formats.Length; index++)
        {
            formatDescription.Append(formats[index].name);

            if (formats[index].descriptions != null && formats[index].descriptions.Length > 0)
            {
                // TODO Currently only get first description, doesn't work for digital release?
                formatDescription.Append(Separator);
                formatDescription.Append(formats[index].descriptions.FirstOrDefault());
            }

            if (formats.Length > 1 && index < formats.Length - 1)
                formatDescription.Append(Add);
        }

        return formatDescription.ToString();
    }

    private static string StripSuffix(this string name)
    {
        // Check for and remove artist or label name numbered suffix, for example 'We Love Music (3)' will be 'We Love Music'

        var words = name.Split();

        if (words.Length > 1)
        {
            var suffixRegEx = new Regex(@"^\(\d+\)");

            var lastWord = words[^1];

            if (suffixRegEx.IsMatch(lastWord))
            {
                var wordsRemoveLast = words.Take(words.Length - 1).ToArray();
                return string.Join(" ", wordsRemoveLast);
            }
            else
                return name;
        }
        else
            return name;
    }
}
