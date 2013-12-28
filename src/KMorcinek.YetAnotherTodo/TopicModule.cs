using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.ModelBinding;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicModule : NancyModule
    {
        public TopicModule()
            : base("/api/topic")
        {
            Get["/{id}"] = parameters =>
            {
                int id = int.Parse(parameters.id.Value);

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().Query<Topic>().Where(t => t.Id == id).Single();

                return Response.AsJson(topic.Notes);
            };

            Post["/insert/{topicId}"] = parameters =>
            {
                int topicId = int.Parse(parameters.topicId.Value);
                var note = this.Bind<Note>();

                var db = DbRepository.GetDb();

                var topic = db.UseOnceTo().Query<Topic>().Where(t => t.Id == topicId).Single();
                
                topic.Notes.Add(note);

                db.UseOnceTo().Update(topic);

                return Response.AsJson(true);
            };

            Get["/delete/{topicId}/{noteId}"] = parameters =>
            {
                int topicId = int.Parse(parameters.topicId.Value);
                int noteId = int.Parse(parameters.noteId.Value);

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().Query<Topic>().Where(t => t.Id == topicId).Single();

                topic.Notes.RemoveAll(n => n.Id == noteId);

                db.UseOnceTo().Update(topic);

                return Response.AsJson(true);
            };
        }
    }
}