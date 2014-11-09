using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using KMorcinek.YetAnotherTodo.DataLayer;
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
                using (var todoModelContext = new TodoModelContext())
                {
                    var topics = todoModelContext.Topics.ToList().Select(t => new SlimTopic(t));
                    return Response.AsJson(topics);
                }
            };

            Get["/{id:int}"] = parameters =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var id = (int)parameters.id.Value;

                    var topic = todoModelContext.Topics.SingleOrDefault(d => d.TopicId == id);

                    return Response.AsJson(topic);
                }
            };

            Post["/"] = _ =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var slimTopic = this.Bind<SlimTopic>();

                    var topic = new DomainClasses.Topic
                    {
                        Name = slimTopic.Name,
                        IsShown = true,
                    };

                    todoModelContext.Topics.Add(topic);
                    todoModelContext.SaveChanges();

                    return Response.AsJson(new { id = topic.TopicId });
                }
            };

            Post["/{id:int}"] = parameters =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var id = (int)parameters.id.Value;

                    DomainClasses.Topic topic = todoModelContext.Topics.Find(id);
                    var db = DbRepository.GetDb();

                    this.BindTo(topic, ReflectionHelper.GetPropertyName<Topic>(p => p.Notes));

                    db.UseOnceTo().Update(topic);

                    return HttpStatusCode.OK; 
                }
            };

            Delete["/{id:int}"] = parameters =>
            {
                using (var todoModelContext = new TodoModelContext())
                {
                    var id = (int)parameters.id.Value;

                    DomainClasses.Topic topic = todoModelContext.Topics.Find(id);
                    todoModelContext.Topics.Remove(topic);
                    todoModelContext.SaveChanges();

                    return HttpStatusCode.NoContent; 
                }
            };
        }
    }
}