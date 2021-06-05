using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using MySafe.Droid.Renderers;
using MySafe.Presentation;
using MySafe.Presentation.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace MySafe.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private CustomEntry _view;
        public CustomEntryRenderer(Context context) : base(context) { }


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
            {
                base.OnElementChanged(e);


                if (Control != null)
                    {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(global::Android.Graphics.Color.Transparent);
                    this.Control.SetBackgroundDrawable(gd);
                    this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Black));
                //Control.SetBackgroundColor(Android.Graphics.Color.Black);

                //this.Control.SetBackground(this.Resources.GetDrawable(Resource.Drawable.PlayerNameEntryBackground, null));
                this.Control.InputType = InputTypes.TextVariationVisiblePassword;
                this.Control.SetTextIsSelectable(true);

                // Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);

                //Control.SetPaddingRelative(Height, 200);
            }
            }
    }
}