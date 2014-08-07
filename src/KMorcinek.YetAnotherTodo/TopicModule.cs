using System.Collections.Generic;
using System.Linq;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using KMorcinek.YetAnotherTodo.Extensions;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicModule : NancyModule
    {
        public TopicModule()
            : base("/api/topics")
        {
            this.RequiresAuthentication();

            Get["/"] = _ =>
            {
                var db = DbRepository.GetDb();
                var topics = db.UseOnceTo().Query<Topic>().ToArray();

                var slimTopics = topics.Select(t => new SlimTopic(t));

                return Response.AsJson(slimTopics);

            };

            Get["/{id:int}"] = _ =>
            {
                var id = (int)_.id.Value;

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().GetById<Topic>(id);

                return Response.AsJson(topic);
            };

            Post["/"] = _ =>
            {
                var slimTopic = this.Bind<SlimTopic>();

                var topic = new Topic
                {
                    Name = slimTopic.Name,
                    Notes = new List<Note>(),
                    IsShown = true,
                };

                var db = DbRepository.GetDb();

                db.UseOnceTo().Insert(topic);

                return Response.AsJson(new { id = topic.Id });
            };

            Post["/{id:int}"] = _ =>
            {
                var id = (int)_.id.Value;

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().GetById<Topic>(id);

                this.BindTo(topic, ReflectionHelper.GetPropertyName<Topic>(p => p.Notes));

                db.UseOnceTo().Update(topic);

                return HttpStatusCode.OK;
            };

            Delete["/{id:int}"] = _ =>
            {
                var id = (int)_.id.Value;

                var db = DbRepository.GetDb();

                db.UseOnceTo().DeleteById<Topic>(id);

                return HttpStatusCode.NoContent;
            };

            Post["/insert/{topicId}"] = parameters =>
            {
                int topicId = int.Parse(parameters.topicId.Value);
                var note = this.Bind<Note>();

                var db = DbRepository.GetDb();

                var topic = db.UseOnceTo().GetById<Topic>(topicId);

                topic.Notes.Add(note);

                db.UseOnceTo().Update(topic);

                return Response.AsJson(true);
            };

            Get["/delete/{topicId}/{noteId}"] = parameters =>
            {
                int topicId = int.Parse(parameters.topicId.Value);
                int noteId = int.Parse(parameters.noteId.Value);

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().GetById<Topic>(topicId);

                topic.Notes.RemoveAll(n => n.Id == noteId);

                db.UseOnceTo().Update(topic);

                return Response.AsJson(true);
            };
        }
    }
}