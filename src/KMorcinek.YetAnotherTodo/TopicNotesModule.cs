using KMorcinek.YetAnotherTodo.DataLayer;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Note = KMorcinek.YetAnotherTodo.DomainClasses.Note;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicNotesModule : NancyModule
    {
        public TopicNotesModule()
            : base("/api/topics/{topicId:int}/notes")
        {
            this.RequiresAuthentication();

            Post["/"] = parameters =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var topicId = (int)parameters.topicId.Value;
                    var note = this.Bind<DomainClasses.Note>();

                    DomainClasses.Topic topic = todoModelContext.Topics.Find(topicId);

                    topic.Notes.Add(note);
                    todoModelContext.Notes.Add(note);

                    todoModelContext.SaveChanges();

                    return HttpStatusCode.Created; 
                }
            };

            Delete["/{noteId:int}"] = parameters =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var topicId = (int)parameters.topicId.Value;
                    var noteId = (int)parameters.noteId.Value;

                    DomainClasses.Topic topic = todoModelContext.Topics.Find(topicId);
                    Note note = todoModelContext.Notes.Find(noteId);
                    topic.Notes.Remove(note);
                    todoModelContext.Notes.Remove(note);

                    todoModelContext.SaveChanges();

                    return HttpStatusCode.NoContent; 
                }
            };
        }
    }
}