using System.Linq.Expressions;
using AutoMapper;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

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