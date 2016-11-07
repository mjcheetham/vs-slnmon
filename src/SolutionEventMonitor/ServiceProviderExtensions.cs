using System;

namespace SolutionEventMonitor
{
    internal static class ServiceProviderExtensions
    {
        public static TInstance GetService<TService, TInstance>(this IServiceProvider serviceProvider)
            where TInstance : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            return serviceProvider.GetService(typeof(TService)) as TInstance;
        }

        public static T GetService<T>(this IServiceProvider serviceProvider)
            where T : class
        {
            return GetService<T, T>(serviceProvider);
        }
    }
}
