using AutoMapper;
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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var mapper = config.CreateMapper();

		    container.Register(mapper);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "demo:demo"));
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