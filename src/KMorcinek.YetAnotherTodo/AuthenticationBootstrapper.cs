using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace KMorcinek.YetAnotherTodo
{
    public class AuthenticationBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));
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