using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Hamburger1.Converters
{
    class MusicConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimeSpan ts = (TimeSpan)value;
            return ts.TotalMilliseconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //double tmp = (double)value;
            //TimeSpan ts = new TimeSpan(0,0,0,0,(int)tmp);
            return null;
        }
    }
}
