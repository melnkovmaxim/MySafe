using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageSource Base64 { get; set; }
    }
}
