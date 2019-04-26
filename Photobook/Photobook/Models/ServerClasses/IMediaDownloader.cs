using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public delegate void ImageDownload(ImageDownloadEventArgs e);

    public class ImageDownloadEventArgs : EventArgs
    {
        public byte[] FileBytes { get; set; } = null;
        public bool StatusOk { get; set; } = false;
    }
    interface IMediaDownloader
    {
        void DownloadSingleImage(string url);
        void DownloadAllImages(List<string> urls);
        event ImageDownload DownloadReady;
    }

    public class MediaDownloader : IMediaDownloader
    {
        public void DownloadSingleImage(string url)
        {
            Task downloadTask = DownloadImage(url);
            downloadTask.Start();

        }

        public event ImageDownload DownloadReady;

        public void DownloadAllImages(List<string> urls)
        {
            foreach (var url in urls)
            {
                Task donwloadTask = DownloadImage(url);
                donwloadTask.Start();
            }
        }

        private async Task  DownloadImage(string url)
        {
            HttpClient _httpClient = new HttpClient();
            ImageDownloadEventArgs image = new ImageDownloadEventArgs();
           
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(url))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        image.FileBytes = await httpResponse.Content.ReadAsByteArrayAsync();
                        image.StatusOk = true;

                        DownloadReady?.Invoke(image);
                    }
                    else
                    {
                        DownloadReady.Invoke(image);
                    }
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                DownloadReady?.Invoke(image);
            }
        }
    }
}
