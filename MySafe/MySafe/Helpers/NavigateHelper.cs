using System.Threading.Tasks;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.Helpers
{
    public class NavigateHelper
    {
        private static AsyncCommand<(INavigationService, string, NavigationParameters)> _navigateCommand;

        static NavigateHelper()
        {
            _navigateCommand = new AsyncCommand<(INavigationService, string, NavigationParameters)>(async (turple) =>
            {
                var navigateService = turple.Item1;
                var pagePath = turple.Item2;

                await navigateService.NavigateAsync(pagePath);
            }, canExecuteMethod: (s) => true, allowMultipleExecution: false);
        }

        public static async Task NavigateAsync(INavigationService navigationService, string pageName, NavigationParameters @params = null)
        {
            await _navigateCommand.ExecuteAsync((navigationService, pageName, @params));
        }
    }
}
