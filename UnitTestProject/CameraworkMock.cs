using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.Media.Abstractions;
using System.Net;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class CameraworkMock
    {
        string destinationDir = Path.Combine(Environment.CurrentDirectory, "..\\..\\PHOTOS_ON_DISK");
        string[] photoLinks = new string[]{ "http://www.funnyjunksite.com/pictures/wp-content/uploads/2015/09/You-Come-To-Me-On-This.jpg",
            "http://cdn.ebaumsworld.com/mediaFiles/picture/2192630/82173714.jpg",
        "http://cdn.ebaumsworld.com/mediaFiles/picture/2192630/82173713.jpg" };
        
        [TestMethod]
        public void DownloadFilesFromTheInternetAndSaveThemOnTheFileSystem()
        {
            if (Directory.Exists(destinationDir))
                Directory.Delete(destinationDir, true);
            
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < photoLinks.Length; i++)
                {
                    MediaFile photo = GetPictureFromInternet(photoLinks[i], client);
                    Assert.IsNotNull(photo);
                    Assert.IsTrue(File.Exists(photo.Path));
                }
            }
        }

        MediaFile GetPictureFromInternet(string address, WebClient client)
        {
            Stream stream = client.OpenRead(address);

            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            string fileName = address.Substring(address.LastIndexOf('/') + 1);
            string destinationName = Path.Combine(destinationDir, fileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] arr = memoryStream.ToArray();

                File.WriteAllBytes(destinationName, arr);
            }

            MediaFile file = new MediaFile(destinationName, () => client.OpenRead(address));

            return file;
        }
    }
}
