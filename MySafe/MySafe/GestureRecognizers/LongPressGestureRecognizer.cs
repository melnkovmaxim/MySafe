using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySafe.Presentation.GestureRecognizers
{
    public class LongPressGestureRecognizer : GestureRecognizer
    {
        public static readonly BindableProperty IsLongPressingProperty;
        public bool IsLongPressing { get; set; }

        public ICommand FinishedCommand { get; set; }
        public object FinishedCommandParameter { get; set; }
	
        // Bindable Property will live on GestureRecognizer
        public ICommand CancelledCommand { get; set; }
        public object CancelledCommandParameter { get; set; }
	
        // Bindable Property will live on GestureRecognizer
        public ICommand StartedCommand { get; set; }
        public object StartedCommandParameter { get; set; }
	
        public static readonly BindableProperty NumberOfTouchesRequiredProperty;
        public int NumberOfTouchesRequired { get; set;}

        public static readonly BindableProperty NumberOfTapsRequiredProperty;
        public int NumberOfTapsRequired { get; set; }

        public static readonly BindableProperty MinimumPressDurationProperty;
        public double MinimumPressDuration { get; set; }

        public static readonly BindableProperty AllowableMovementProperty;
        public uint AllowableMovement { get; set; }

        public event EventHandler LongPressed;
        public event EventHandler<LongPressUpdatedEventArgs> LongPressUpdated;
    }

    public class LongPressUpdatedEventArgs : GestureEventArgs
    {	
        public GestureStatus StatusType { get; }
    }

    public class GestureEventArgs : EventArgs
    {
        public TouchEvent TouchEvent { get; private set; }
    }

    public class TouchEvent
    {
        public IReadOnlyList<Touch> Touches { get; set; }
        public IReadOnlyList<Touch> ChangedTouches { get; set; }
        public IReadOnlyList<Touch> TargetTouches { get; set; }
        View Target { get; }
    }
    public class Touch
    {
        public int TouchIndex { get; set; }
        public Point ViewPosition { get; set; }
        public Point PagePosition { get; set; }
        public Point ScreenPosition { get; set; }
        View Target { get; }
    }

}
