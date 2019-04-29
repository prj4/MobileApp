using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public delegate void ImageDownload(ImageDownloadEventArgs e);

    public class ImageDownloadEventArgs : EventArgs
    {
        public byte[] FileBytes { get; set; }
        public bool StatusOk { get; set; }
    }

    internal interface IMediaDownloader
    {
        void DownloadSingleImage(string url);
        void DownloadAllImages(List<string> urls);
        event ImageDownload DownloadReady;
    }

    public class MediaDownloader : IMediaDownloader
    {
        private readonly CookieCollection cookies;

        public MediaDownloader(CookieCollection _cookies)
        {
            cookies = _cookies;
        }

        public void DownloadSingleImage(string url)
        {
            Parallel.Invoke(() => DownloadImage(url));
        }

        public event ImageDownload DownloadReady;

        public void DownloadAllImages(List<string> urls)
        {
            Parallel.ForEach(urls, url => { DownloadImage(url); });
        }

        private async void DownloadImage(string url)
        {
            HttpClient _httpClient;
            if (cookies != null)
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.CookieContainer = new CookieContainer();
                clientHandler.CookieContainer.Add(cookies);
                _httpClient = new HttpClient(clientHandler);
            }
            else
            {
                return;
            }

            var image = new ImageDownloadEventArgs();


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