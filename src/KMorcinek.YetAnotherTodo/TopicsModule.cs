using System.Collections.Generic;
using System.Linq;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using KMorcinek.YetAnotherTodo.Extensions;

namespace KMorcinek.YetAnotherTodo
{
    public class TopicsModule : NancyModule
    {
        public TopicsModule()
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

            Get["/{id:int}"] = parameters =>
            {
                var id = (int)parameters.id.Value;

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

            Post["/{id:int}"] = parameters =>
            {
                var id = (int)parameters.id.Value;

                var db = DbRepository.GetDb();
                var topic = db.UseOnceTo().GetById<Topic>(id);

                this.BindTo(topic, ReflectionHelper.GetPropertyName<Topic>(p => p.Notes));

                db.UseOnceTo().Update(topic);

                return HttpStatusCode.OK;
            };

            Delete["/{id:int}"] = parameters =>
            {
                var id = (int)parameters.id.Value;

                var db = DbRepository.GetDb();

                db.UseOnceTo().DeleteById<Topic>(id);

                return HttpStatusCode.NoContent;
            };
        }
    }
}