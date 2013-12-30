using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using KMorcinek.YetAnotherTodo.Models;
using NUnit.Framework;

namespace KMorcinek.YetAnotherTodo.FromXmlConverter
{
    [TestFixture]
    public class ConvertingTests
    {
        private const string XmlPath = @"a7711849_morcine.xml";

        [Test]
        public void CheckDB()
        {
            var db = DbRepository.GetDb();
            var topics = db.UseOnceTo().Query<Topic>().ToArray();
            var note = db.UseOnceTo().Query<Note>().ToArray();
        }

        [Test]
        public void Convert()
        {
            var notes = GetNotes().ToArray();
            var topics = GetTopics().ToArray();

            foreach (var topic in topics)
            {
                var topicId = topic.Id;
                var relatedNotes = notes.Where(p => p.TopicId == topicId);
                var list = relatedNotes.Select(Convert).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Id = i;
                }

                topic.Notes = list;
            }

            var db = DbRepository.GetDb();
            db.EnsureNewDatabase();
            db.UseOnceTo().InsertMany(topics);
        }

        private static Note Convert(RawNote raw)
        {
            return new Note
            {
                Content = raw.Content
            };
        }

        public IEnumerable<RawNote> GetNotes()
        {
            var document = GetTopicsHtmlDocument();

            var someNodes = document.DocumentNode.SelectNodes("//messages");

            foreach (var node in someNodes)
            {
                int topicId = int.Parse(node.SelectSingleNode("topic_id_fk").InnerText);
                string content = node.SelectSingleNode("content").InnerText;

                content = EncodingImprover.Improve(content);

                var note = new RawNote
                {
                    TopicId = topicId,
                    Content = content,
                };

                yield return note;
            }
        }

        public IEnumerable<Topic> GetTopics()
        {
            var document = GetTopicsHtmlDocument();

            var someNodes = document.DocumentNode.SelectNodes("//topics");

            foreach (var node in someNodes)
            {
                int id = int.Parse(node.SelectSingleNode("topic_id").InnerText);
                string name = node.SelectSingleNode("topic_name").InnerText;

                name = EncodingImprover.Improve(name);

                var topic = new Topic
                {
                    Id = id,
                    Name = name,
                    IsShown = true,
                };

                yield return topic;
            }
        }

        private static HtmlDocument GetTopicsHtmlDocument()
        {
            var readAllText = File.ReadAllText(XmlPath);
            return GetHtmlDocument(readAllText);
        }

        private static HtmlDocument GetHtmlDocument(string nodeHtml)
        {
            string html = string.Format("<html><head></head><body>{0}</body></html>", nodeHtml);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        [Test]
        public void CreateTestDB()
        {
            var topics = new[]
            {
                new Topic
                {
                    Name = "For now",
                    Notes = new[]
                    {
                        new Note {Content = "Drink", Id = 1},
                        new Note {Content = "Eat", Id = 2},
                    }.ToList(),
                    IsShown=true,
                },
                new Topic
                {
                    Name = "For later",
                    Notes = new[]
                    {
                        new Note {Content = "Sleep", Id = 1},
                        new Note {Content = "Sleep better", Id = 2},
                    }.ToList(),
                    IsShown=true,
                },
                new Topic
                {
                    Name = "Obsolete stuff",
                    Notes = new[]
                    {
                        new Note {Content = "Learn PHP", Id = 1},
                        new Note {Content = "Learn spanish", Id = 2},
                    }.ToList(),
                },
            };

            var db = DbRepository.GetDb();
            db.EnsureNewDatabase();
            db.UseOnceTo().InsertMany(topics);
        }
    }
}