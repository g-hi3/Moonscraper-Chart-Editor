#undef APPLICATION_MOONSCRAPER

using System.Collections.Generic;
using MoonscraperChartEditor.Song;
using NUnit.Framework;

namespace Tests.Game.Charts
{
    public class SongObjectHelperTests
    {
        [Test]
        public void InsertIntoEmptyList()
        {
            var item = new Note(default, default(int));
            var list = new List<Note>();
            var insertPosition = SongObjectHelper.Insert(item, list);
            Assert.That(insertPosition, Is.Zero);
            Assert.That(list, Contains.Item(item));
            Assert.That(item.previous, Is.Null);
            Assert.That(item.next, Is.Null);
        }

        [Test]
        public void InsertBeforeOther()
        {
            var item = new Note(15, default(int));
            var existingNote = new Note(30, default(int));
            var list = new List<Note> {existingNote};
            var insertPosition = SongObjectHelper.Insert(item, list);
            Assert.That(insertPosition, Is.Zero);
            Assert.That(list, Contains.Item(item));
            Assert.That(item.previous, Is.Null);
            Assert.That(item.next, Is.EqualTo(existingNote));
        }

        [Test]
        public void InsertAfterOther()
        {
            var item = new Note(45, default(int));
            var existingNote = new Note(30, default(int));
            var list = new List<Note> {existingNote};
            var insertPosition = SongObjectHelper.Insert(item, list);
            Assert.That(insertPosition, Is.EqualTo(1));
            Assert.That(list, Contains.Item(item));
            Assert.That(item.previous, Is.EqualTo(existingNote));
            Assert.That(item.next, Is.Null);
        }

        [Test]
        public void InsertBetweenOthers()
        {
            var item = new Note(40, default(int));
            var noteBefore = new Note(30, default(int));
            var noteAfter = new Note(45, default(int));
            var list = new List<Note>
            {
                noteBefore,
                noteAfter
            };
            var insertPosition = SongObjectHelper.Insert(item, list);
            Assert.That(insertPosition, Is.EqualTo(1));
            Assert.That(list, Contains.Item(item));
            Assert.That(item.previous, Is.EqualTo(noteBefore));
            Assert.That(item.next, Is.EqualTo(noteAfter));
        }
    }
}