using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Navigation;

namespace ReactInXamarin
{
    public static class PrismExtensions
    {
        public static Task<INavigationResult> NavigateTo<T>(this INavigationService navigationService,
            bool putOnTopOfTheNavigationStack = false)
        {
            return NavigateTo<T>(navigationService, null, putOnTopOfTheNavigationStack);
        }
    
        public static Task<INavigationResult> NavigateTo<T>(this INavigationService navigationService,
            INavigationParameters parameters, bool putOnTopOfTheNavigationStack = false)
        {
            var prefix = putOnTopOfTheNavigationStack ? "NavigationPage/" : "";
            var screenName = prefix + typeof(T).Name;
            return Navigate(navigationService, screenName, parameters);
        }
    
        private static async Task<INavigationResult> Navigate(this INavigationService navigationService, string screenName, INavigationParameters parameters)
        {
            var result = await navigationService.NavigateAsync(screenName, parameters);
            
            if(!result.Success)
            {
                Console.WriteLine(result.Exception);
                Debugger.Break();
            }
            return result;
        }
    }
}