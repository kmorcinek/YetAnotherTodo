using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KMorcinek.YetAnotherTodo.DataLayer;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using SisoDb;

namespace KMorcinek.YetAnotherTodo
{
    public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "demo:demo"));

            DbRepository.Initialize();

		    ConfigureAutoMapper();

		    ConvertToEF();
		}

        private void ConvertToEF()
        {
            ISisoDatabase sisoDatabase = DbRepository.GetDb();
            IList<Topic> topics = sisoDatabase.UseOnceTo().Query<Topic>().ToList();

            using (var todoModelContext = new TodoModelContext())
            {
                foreach (var topic in topics)
                {
                    InsertTopicIntoEF(topic, todoModelContext); 
                }

                todoModelContext.SaveChanges();
            }
        }

        private static void InsertTopicIntoEF(Topic topic1, TodoModelContext todoModelContext)
        {
            var topic = Convert(topic1);

            todoModelContext.Topics.Add(topic);
            foreach (var note in topic.Notes)
            {
                todoModelContext.Notes.Add(note);
            }
        }

        private static DomainClasses.Topic Convert(Topic source)
        {
            DomainClasses.Topic topic = new DomainClasses.Topic
            {
                Name = source.Name,
                IsShown = source.IsShown,
                Notes = source.Notes.Select(Convert).ToList(),
            };
            return topic;
        }

        private static DomainClasses.Note Convert(Note source)
        {
            return new DomainClasses.Note
            {
                Content = source.Content
            };
        }

        private void ConfigureAutoMapper()
        {
            Mapper.CreateMap<DomainClasses.Topic, Topic>();
            Mapper.CreateMap<DomainClasses.Note, Note>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(origin => origin.NoteId));
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            var directories = new[] {
                "/Scripts",
                "/app",
            };

            foreach (var directory in directories)
            {
                conventions.StaticContentsConventions.Add(
                    StaticContentConventionBuilder.AddDirectory(directory, directory)
                );
            }
        }
	}
}