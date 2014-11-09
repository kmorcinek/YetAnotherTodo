using System.Collections.Generic;
using System.Linq;

namespace KMorcinek.YetAnotherTodo.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
        public bool IsShown { get; set; }

        public static Topic FromDomainModel(DomainClasses.Topic domainTopic)
        {
            return new Topic
            {
                Id = domainTopic.TopicId,
                Name = domainTopic.Name,
                Notes = domainTopic.Notes.Select(note => new Note{Id = note.NoteId, Content = note.Content}).ToList(),
                IsShown = domainTopic.IsShown,
            };
        }
    }
}