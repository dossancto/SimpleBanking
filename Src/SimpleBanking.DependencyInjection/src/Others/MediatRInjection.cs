namespace SimpleBanking.DependencyInjection.Others;

internal static class MediatRInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
      => services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Application.Application).Assembly));
}
