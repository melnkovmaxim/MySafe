using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ContainsAttachments { get; set; }

        public DelegateCommand<object> DelCommand { get; set; }

        public Document(INavigationService service)
        {
            DelCommand = new DelegateCommand<object>(async (x) =>
            {
                await service.NavigateAsync(nameof(FolderDocPage), new NavigationParameters{{"id", x}});
            });
        }
    }
}
