using System;
using Discogs.Api.Models;
using System.Linq;
using System.Text;

namespace Discogs.Api.Mappers
{
    public static class ReleaseMapper
    {
        private const string Separator = ", ";
        private const string Add = " + ";

        public static string MapArtistDescription(Artist[] artists)
        {
            var artistDescription = new StringBuilder();

            for (var index = 0; index < artists.Length; index++)
            {
                var name = string.IsNullOrWhiteSpace(artists[index].anv) ? artists[index].name : artists[index].anv;

                artistDescription.Append(name);

                if (artists.Length > 1 && index < artists.Length - 1)
                {
                    if (!string.IsNullOrWhiteSpace(artists[index].join))
                        artistDescription.Append(" " + artists[index].join + " ");
                }
            }

            return artistDescription.ToString();
        }

        public static string MapLabelDescription(Label[] labels)
        {
            return string.Join(Separator, labels.Select(label => label.name));
        }

        public static string MapFormatDescription(Format[] formats)
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
    }
}
