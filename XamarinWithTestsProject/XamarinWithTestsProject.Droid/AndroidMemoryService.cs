using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinWithTestsProject.MemoryDetailsView;
using Xamarin.Forms;
using XamarinWithTestsProject.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMemoryService))]
namespace XamarinWithTestsProject.Droid
{
    public class AndroidMemoryService : IMemoryService
    {
        public AndroidMemoryService()
        {
        }

        public MemoryInfo GetInfo()
        {
            return MemoryHelper.GetMemoryInfo(Forms.Context);
        }

    }
}