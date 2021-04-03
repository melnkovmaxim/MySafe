using System;
using Android.Animation;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using MySafe.Droid.Renderers;
using MySafe.Presentation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]

namespace MySafe.Droid.Renderers
{
    internal class GradientButtonRenderer : ButtonRenderer
    {
        #region constructor

        public GradientButtonRenderer(Context context) : base(context)
        {
        }

        #endregion

        #region overridable

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null) Control.Touch -= ButtonTouched;

            if (Control != null)
                try
                {
                    Control.Touch += ButtonTouched;
                    Control.StateListAnimator = new StateListAnimator();
                    Control.SetBackground(DrawGradient(e));
                }
                catch (Exception ex)
                {
                    // handle exception
                }
        }

        #endregion

        #region privates

        /// <summary>
        ///     Create the gradient for the button background
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GradientDrawable DrawGradient(ElementChangedEventArgs<Button> e)
        {
            var button = e.NewElement as GradientButton;
            var orientation = button.GradientOrientation == GradientButton.GradientOrientationStates.Horizontal
                ? GradientDrawable.Orientation.LeftRight
                : GradientDrawable.Orientation.TopBottom;

            _backgroundColor = new GradientDrawable(orientation, new[]
            {
                button.BackgroundColor.ToAndroid().ToArgb(),
                button.BackgroundColor.ToAndroid().ToArgb()
            });

            _defaultGradient = new GradientDrawable(orientation, new[]
            {
                button.StartColor.ToAndroid().ToArgb(),
                button.EndColor.ToAndroid().ToArgb()
            });

            _defaultGradient.SetCornerRadius(button.CornerRadius * 10);
            _defaultGradient.SetStroke(0, button.StartColor.ToAndroid());

            return _defaultGradient;
        }

        #endregion

        #region instances

        private GradientDrawable _defaultGradient;
        private Drawable _backgroundColor;

        #endregion

        #region EventHandler

        /// <summary>
        ///     Draw the gradient with the correct oppacity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTouched(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (e.Event.Action == MotionEventActions.Down)
                //_defaultGradient.Alpha = 200;
                Control.SetBackground(_backgroundColor);
            else if (e.Event.Action == MotionEventActions.Up) Control.SetBackground(_defaultGradient);
        }

        private void ButtonReleased(object sender, TouchEventArgs e)
        {
        }

        #endregion
    }
}