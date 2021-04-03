using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySafe.Presentation.CustomControls
{
    public class CustomButton : Button
    {
        public Command LongPressCommand { get; set; }
    }
}
