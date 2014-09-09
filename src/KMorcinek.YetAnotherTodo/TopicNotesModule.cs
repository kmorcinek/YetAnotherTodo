using System.Collections.Generic;
using System.Linq;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using KMorcinek.YetAnotherTodo.Extensions;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicNotesModule : NancyModule
    {
        public TopicNotesModule()
            : base("/api/topics/{topicId:int}/notes")
        {
            this.RequiresAuthentication();

            Post[""] = parameters =>
            {
                var topicId = (int)parameters.topicId.Value;
                var note = this.Bind<Note>();

                var db = DbRepository.GetDb();

                var topic = db.UseOnceTo().GetById<Topic>(topicId);

                topic.Notes.Add(note);

                db.UseOnceTo().Update(topic);

                return HttpStatusCode.Created;
            };

            Delete["/{noteId:int}"] = parameters =>
            {
                var topicId = (int)parameters.topicId.Value;
                var noteId = (int)parameters.noteId.Value;

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().GetById<Topic>(topicId);

                topic.Notes.RemoveAll(n => n.Id == noteId);

                db.UseOnceTo().Update(topic);

                return HttpStatusCode.NoContent;
            };
        }
    }
}