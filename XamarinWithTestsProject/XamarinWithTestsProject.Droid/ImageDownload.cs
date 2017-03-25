using Xamarin.Forms;
using System.Net;
using System.IO;
using Plugin.Media.Abstractions;
using XamarinWithTestsProject.Droid;
using System.Threading.Tasks;
using Plugin.Media;
using Android.OS;

[assembly: Xamarin.Forms.Dependency(typeof(ImageDownload))]
namespace XamarinWithTestsProject.Droid
{
    public class ImageDownload : IImageDownload
    {
        public async Task<MediaFile> GetImageFromInternet(string address)
        {
            WebClient client = new WebClient();

            Stream stream = client.OpenRead(address);

            string destinationDir = Path.Combine(Environment.ExternalStorageDirectory.Path, "Pictures/TESTAPP");
            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            string fileName = address.Substring(address.LastIndexOf('/') + 1);
            string destinationName = Path.Combine(destinationDir, fileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] arr = memoryStream.ToArray();

                while (File.Exists(destinationName))
                    destinationName.Insert(0, "I");

                File.WriteAllBytes(destinationName, arr);
            }

            MediaFile file = new MediaFile(destinationName, () => client.OpenRead(address));

            return file;
        }
    }
}