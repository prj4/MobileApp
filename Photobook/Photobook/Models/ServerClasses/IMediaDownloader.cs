using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public delegate void ImageDownload(ImageDownloadEventArgs e);

    public delegate void ImageCount(int count);
    

    public class ImageDownloadEventArgs : EventArgs
    {
        public byte[] FileBytes { get; set; }
        public bool StatusOk { get; set; }
        public string PictureId { get; set; }
        public string PinId { get; set; }
    }

    internal interface IMediaDownloader
    {
        void DownloadSingleImage(string url);
        void DownloadAllImages(List<string> urls);
        event ImageCount DownloadStarted;
        event ImageDownload Downloading;
    }

    public class MediaDownloader : IMediaDownloader
    {
        public event ImageCount DownloadStarted;
        public event ImageDownload Downloading;
        private readonly CookieCollection cookies;
        private readonly object _lock = new object();
        public MediaDownloader(CookieCollection _cookies)
        {
            cookies = _cookies;
        }

        public void DownloadSingleImage(string url)
        {
            Parallel.Invoke(() => DownloadImage(url));
        }

        

        public void DownloadAllImages(List<string> urls)
        {
            DownloadStarted?.Invoke(urls.Count);
            Parallel.ForEach(urls, DownloadImage);
        }

        private async void DownloadImage(string url)
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(cookies);
            handler.UseCookies = true;

            var client = new HttpClient(handler);

            var image = new ImageDownloadEventArgs();

            try
            {
                var content = url.Split('/');
                
                var httpResponse = await client.GetAsync(url);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    image.FileBytes = await httpResponse.Content.ReadAsByteArrayAsync();
                    
                    image.StatusOk = true;

                    image.PictureId = httpResponse.Content.Headers.ContentDisposition?.FileName;

                    image.PinId = $"{content[content.Length - 2]}/{content[content.Length - 1]}";

                    Downloading?.Invoke(image);
                }
                else
                {
                    Downloading?.Invoke(image);

                }
                
            }
            catch (Exception e)
            {
                //Handle Exception
                Downloading?.Invoke(image);
                Debug.WriteLine(e.Message, "Exception in download");
            }
        }
    }
}