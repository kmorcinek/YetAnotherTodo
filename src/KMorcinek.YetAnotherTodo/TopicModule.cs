using System.Collections.Generic;
using System.Linq;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicModule : NancyModule
    {
        public TopicModule()
            : base("/api/topic")
        {
            this.RequiresAuthentication();

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

            Get["/"] = _ =>
            {
                var db = DbRepository.GetDb();
                var topics = db.UseOnceTo().Query<Topic>().ToArray();

                var slimTopics = topics.Select(t => new SlimTopic(t));

                return Response.AsJson(slimTopics);
            };

            Post["/insert"] = _ =>
            {
                var slimTopic = this.Bind<SlimTopic>();

                var topic = new Topic
                {
                    Name = slimTopic.Name,
                    Notes = new List<Note>(),
                    IsShown = true,
                };

                var db = DbRepository.GetDb();

                db.UseOnceTo().InsertAs<Topic>(topic);

                var slimTopicToReturn = new SlimTopic(topic);

                return Response.AsJson(slimTopicToReturn);
            };

            Post["/update"] = _ =>
            {
                var slimTopic = this.Bind<SlimTopic>();

                var db = DbRepository.GetDb();

                var topic = db.UseOnceTo().Query<Topic>().Where(t => t.Id == slimTopic.Id).Single();

                topic.Name = slimTopic.Name;
                topic.IsShown = slimTopic.IsShown;

                db.UseOnceTo().Update(topic);

                return Response.AsJson(true);
            };

            Get["/delete/{topicId}"] = parameters =>
            {
                int topicId = int.Parse(parameters.topicId.Value);

                var db = DbRepository.GetDb();

                db.UseOnceTo().DeleteById<Topic>(topicId);

                return Response.AsJson(true);
            };
        }
    }
}