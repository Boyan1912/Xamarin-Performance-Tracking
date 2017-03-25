using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinWithTestsProject
{
    public class App : Application
    {
        string baseUrl = "http://cdn.ebaumsworld.com/mediaFiles/picture/2192630/";
        //    List<string> images = new List<string> { "http://www.funnyjunksite.com/pictures/wp-content/uploads/2015/09/You-Come-To-Me-On-This.jpg"
        //};
        public App()
        {
            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome!"
                        },
                    }
            };

            var memoryView = new MemoryDetailsView.MemoryDetailsView();
            stack.Children.Add(memoryView);

            var scroll = new ScrollView
            {
                Content = stack
            };

            var content = new ContentPage
            {
                Title = "XamarinWithTestsProject",
                Content = scroll
            };

            MainPage = new NavigationPage(content);

            Task.Factory.StartNew(async() =>
            {
                for (int i = 82173712; i < 82173729; i++)
                {
                    string url = baseUrl + i.ToString() + ".jpg";
                    var file = await DependencyService.Get<IImageDownload>().GetImageFromInternet(url);
                    var image = new Image()
                    {
                        Source = ImageSource.FromStream(() =>
                        {
                            using (file)
                            {
                                var stream = file.GetStream();
                                return stream;
                            }
                        })
                    };
                    Device.BeginInvokeOnMainThread(() => stack.Children.Add(image));
                    Device.BeginInvokeOnMainThread(() => stack.Children.Add(new Label { Text = memoryView.MemoryUsage.ToString() }));

                    i = i >= 82173729 ? 82173712 : i;
                }
            });
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
