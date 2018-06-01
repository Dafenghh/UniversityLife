using Hamburger1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class CaculatePage : Page
    {
        public CaculatePage()
        {
            this.InitializeComponent();
        }

        private async System.Threading.Tasks.Task GetResult()
        {
            
            Rootobject myCaculator = await WolframealphaProxy.GetResult(Caculate_textBox.Text);
            Result_textBlock.Text = myCaculator.queryresult.pods[1].subpods[1].plaintext;
            //Debug.WriteLine(myCaculator.queryresult.pods[1].subpods[1].plaintext);
        }

        private void Caculate_button_click(object sender, RoutedEventArgs e)
        {
            GetResult();
            Debug.WriteLine(Uri.EscapeDataString(Caculate_textBox.Text));
        }
    }
}
