using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hamburger1.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RelaxPage : Page
    {
        public RelaxPage()
        {
            this.InitializeComponent();
            Mp3MediaElement.Play();
        }

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = Mp3MediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            Mp3MediaElement.Stop();
            timelineSlider.Value = 0;
        }

        private void SeekToMediaPosition(object sender, RangeBaseValueChangedEventArgs e)
        {
            int SlideValue = (int)timelineSlider.Value;

            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SlideValue);
            Mp3MediaElement.Position = ts;
        }

        private void OnMouseDownPlayMedia(object sender, RoutedEventArgs e)
        {
            Mp3MediaElement.Play();
            EllStoryBoard.Begin();
        }

        private void InitializePropertyValues()
        {
            //Mp3MediaElement.Volume = (double)Mp3MediaElement.Value;
            timelineSlider.Value = 0;
        }

        private void OnMouseDownPauseMedia(object sender, RoutedEventArgs e)
        {
            Mp3MediaElement.Pause();
            EllStoryBoard.Pause();
        }

        private void OnMouseDownStopMedia(object sender, RoutedEventArgs e)
        {
            Mp3MediaElement.Stop();
            EllStoryBoard.Pause();
            InitializePropertyValues();
        }

        private void pageHeader_Opened(object sender, object e)
        {

        }
    }
}