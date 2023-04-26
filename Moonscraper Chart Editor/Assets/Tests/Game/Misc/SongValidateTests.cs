using System;
using MoonscraperChartEditor.Song;
using NUnit.Framework;

namespace Tests.Game.Misc
{
    public class SongValidateTests
    {
        [Test]
        public void EmptyCharacterizationTestWithoutValidationOptions()
        {
            var song = new Song();
            var reportContent = SongValidate.GenerateReport(default, song, default, out var validationParams);
            Assert.That(reportContent, Is.EqualTo(
                "Moonscraper validation report: \r\n"
                + "\tFound synctrack object beyond the length of the song-\n"
                + "\t\tType = MoonscraperChartEditor.Song.TimeSignature, time = 00:00.00, position = 0\n"
                + "\tFound synctrack object beyond the length of the song-\n"
                + "\t\tType = MoonscraperChartEditor.Song.BPM, time = 00:00.00, position = 0\n\n"));
            Assert.That(validationParams, Is.True);
        }
        
        [Test]
        public void EmptyCharacterizationTestWithGh3AndChValidationOptionsAndSomeValidationParameters()
        {
            const SongValidate.ValidationOptions validationOptions =
                SongValidate.ValidationOptions.CloneHero
                | SongValidate.ValidationOptions.GuitarHero3;
            var song = new Song();
            var validationParameters = new SongValidate.ValidationParameters
            {
                songLength = 100,
                checkMidiIssues = true
            };
            var reportContent = SongValidate.GenerateReport(validationOptions, song, validationParameters, out var validationParams);
            Assert.That(reportContent, Is.EqualTo(
                $"Moonscraper validation report: {Environment.NewLine}"
                + $"\tNo errors detected{Environment.NewLine}\n"
                + $"Guitar Hero 3 validation report: {Environment.NewLine}"
                + $"\tNo errors detected{Environment.NewLine}\n"
                + $"Clone Hero validation report: {Environment.NewLine}"
                + $"\tNo errors detected{Environment.NewLine}\n"));
            Assert.That(validationParams, Is.False);
        }
    }
}
