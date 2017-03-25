using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinWithTestsProject
{
    public interface IImageDownload
    {
        Task<MediaFile> GetImageFromInternet(string address);
    }
}
