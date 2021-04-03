using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Presentation.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace MySafe.iOS
{
    public class CustomButtonRenderer: ButtonRenderer
    {
        private CustomButton _view;  
        private readonly UILongPressGestureRecognizer _longPressRecognizer;  
        private readonly UITapGestureRecognizer _tapGestureRecognizer;  
  
        public CustomButtonRenderer()  
        {  
            //_longPressRecognizer = new UILongPressGestureRecognizer((s) =>  
            //{  
            //    if (s.State == UIGestureRecognizerState.Began && _view != null)  
            //    {  
            //        _view.LongPressedHandler?.Invoke(_view, null);  
            //        var command = _view.LongpressCommand;// CustomImage.GetCommand(_view);  
            //        command?.Execute(_view);  
            //    }  
            //});  
  
            //_tapGestureRecognizer = new UITapGestureRecognizer(() =>  
            //{  
                  
            //    _view.TappedHandler?.Invoke(_view, null);  
            //    var command = _view.Command;// CustomImage.GetCommand(_view);  
            //    command?.Execute(_view);  
            //});  
        }  
  
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)  
        {  
            base.OnElementChanged(e);  
  
            if (e.NewElement != null)  
            {  
                _view = e.NewElement as CustomButton;  
            }  
  
              
  
            if (Control != null)  
            {  
                Control.UserInteractionEnabled = true;  
                Control.AddGestureRecognizer(_longPressRecognizer);  
                Control.AddGestureRecognizer(_tapGestureRecognizer);  
            }  
        } 
    }
}
