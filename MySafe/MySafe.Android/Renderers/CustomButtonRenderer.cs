using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using MySafe.Presentation.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MySafe.Droid.Renderers
{
    public class CustomButtonRenderer: ButtonRenderer
    {
        private CustomButton _view;  
  
        public CustomButtonRenderer(Context context) : base(context) { }  
  
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)  
        {  
            base.OnElementChanged(e);

            var view = (CustomButton)Element;

            this.Control.LongClick += (s, args) =>
            {
                view.LongPressCommand.Execute(view.CommandParameter);
            };
        }  
    }
}
