using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        private const double ROTATATION_DEGRESS = 90d;
        private Image _currentCarouselImage => Carousel.VisibleViews.Last() as Image;
        private List<Image> _rotatedImages;

        public DocumentPage()
        {
            InitializeComponent();
            
            _rotatedImages = new List<Image>();

            RefreshView.Refreshing += (s, e) => Refreshing();
            ButtonRotateLeft.Clicked += (s, e) => Rotate(RotateEnum.Left);
            ButtonRotateRight.Clicked += (s, e) => Rotate(RotateEnum.Right);

            Task.Run(async () =>
            {
                while (true)
                {
                    await _spinnetImage.RelRotateTo(360, 3000); 
                }
            });
        }

        private void Refreshing()
        {
            foreach (var item in _rotatedImages)
            {
                item.RotateTo(0d);
            }

            _rotatedImages.Clear();
        }

        private void Rotate(RotateEnum rotateEnum)
        {
            var degrees = rotateEnum == RotateEnum.Right ? ROTATATION_DEGRESS : ROTATATION_DEGRESS * -1;

            _currentCarouselImage.RotateTo(_currentCarouselImage.Rotation + degrees);

            if (!_rotatedImages.Contains(_currentCarouselImage))
            {
                _rotatedImages.Add(_currentCarouselImage);
            }
        }

        private enum RotateEnum
        {
            Left,
            Right
        }
    }
}