using Discogs.Api.Core.Models;
using Discogs.Api.Core.Models.Extensions;
using FluentAssertions;
using Xunit;

namespace Discogs.Api.Tests.Models.Extensions
{
    public class ModelExtensionsTests
    {
        [Fact]
        public void MapDescription_SingleArtist_ReturnDescription()
        {
            var artist = CreateArtist(name: "Artist 1");
            var artists = new Artist[] { artist };

            var artistDescription = artists.MapDescription();

            artistDescription.Should().Be(artist.name);
        }

        [Fact]
        public void MapDescription_SingleArtistWithAlternativeName_ReturnDescription()
        {
            var artist = CreateArtist(name: "Artist 1", anv: "Artist Name");
            var artists = new Artist[] { artist };

            var artistDescription = artists.MapDescription();

            artistDescription.Should().Be(artist.anv);
        }

        [Fact]
        public void MapDescription_MultipleArtists_ReturnDescription()
        {
            var artist1 = CreateArtist(name: "Artist 1", join: "With");
            var artist2 = CreateArtist(name: "Artist 2", join: "And");
            var artist3 = CreateArtist(name: "Artist 3");
            var artists = new Artist[] { artist1, artist2, artist3 };

            var artistDescription = artists.MapDescription();

            artistDescription.Should().Be($"{artist1.name} {artist1.join} {artist2.name} {artist2.join} {artist3.name}");
        }

        [Fact]
        public void MapDescription_ArtistWithSuffix_ReturnDescription()
        {
            var artist = CreateArtist(name: "Artist 1 (2)");
            var artists = new Artist[] { artist };

            var artistDescription = artists.MapDescription();

            artistDescription.Should().Be(artist.name.Replace(" (2)", ""));
        }

        [Fact]
        public void MapDescription_SingleLabel_ReturnDescription()
        {
            var label = CreateLabel("Label 1");
            var labels = new Label[] { label };

            var labelDescription = labels.MapDescription();

            labelDescription.Should().Be(label.name);
        }

        [Fact]
        public void MapDescription_MultipleLabels_ReturnDescription()
        {
            var label1 = CreateLabel("Label 1");
            var label2 = CreateLabel("Label 2");
            var label3 = CreateLabel("Label 3");
            var labels = new Label[] { label1, label2, label3 };

            var labelDescription = labels.MapDescription();

            labelDescription.Should().Be($"{label1.name}, {label2.name}, {label3.name}");
        }

        [Fact]
        public void MapDescription_SingleFormatWithSingleDescription_ReturnDescription()
        {
            var format = CreateFormat("Format 1", new string[] { "Description 1" });
            var formats = new Format[] { format };

            var formatDescription = formats.MapDescription();

            formatDescription.Should().Be($"{format.name}, {format.descriptions[0]}");
        }

        [Fact]
        public void MapDescription_SingleFormatWithMultipleDescriptions_ReturnDescription()
        {
            var format = CreateFormat("Format 1", new string[] { "Description 1", "Description 2" });
            var formats = new Format[] { format };

            var formatDescription = formats.MapDescription();

            formatDescription.Should().Be($"{format.name}, {format.descriptions[0]}");
        }

        [Fact]
        public void MapDescription_MultipleFormatsWithSingleDescription_ReturnDescription()
        {
            var format1 = CreateFormat("Format 1", new string[] { "Description 1" });
            var format2 = CreateFormat("Format 2", new string[] { "Description 2" });
            var formats = new Format[] { format1, format2 };

            var formatDescription = formats.MapDescription();

            formatDescription.Should().Be($"{format1.name}, {format1.descriptions[0]} + {format2.name}, {format2.descriptions[0]}");
        }

        [Fact]
        public void MapDescription_MulipleFormatsWithMultipleDescriptions_ReturnDescription()
        {
            var format1 = CreateFormat("Format 1", new string[] { "Description 1", "Description 2" });
            var format2 = CreateFormat("Format 2", new string[] { "Description 3", "Description 4" });
            var formats = new Format[] { format1, format2 };

            var formatDescription = formats.MapDescription();

            formatDescription.Should().Be($"{format1.name}, {format1.descriptions[0]} + {format2.name}, {format2.descriptions[0]}");
        }

        private static Artist CreateArtist(string name, string anv = "", string join = "")
        {
            return new() { name = name, anv = anv, join = join };
        }

        private static Label CreateLabel(string name)
        {
            return new() { name = name };
        }

        private static Format CreateFormat(string name, string[] descriptions)
        {
            return new() { name = name, descriptions = descriptions };
        }
    }
}
