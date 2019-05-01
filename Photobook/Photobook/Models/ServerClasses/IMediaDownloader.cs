using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public delegate void ImageDownload(ImageDownloadEventArgs e);

    public delegate void ImageCount(int count);

    public delegate void Done();

    public class ImageDownloadEventArgs : EventArgs
    {
        public byte[] FileBytes { get; set; }
        public bool StatusOk { get; set; }
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
        private readonly CookieCollection cookies;
        public event ImageCount DownloadStarted;
        public event ImageDownload Downloading;
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
            Parallel.ForEach(urls, url => { DownloadImage(url); });
        }

        private async void DownloadImage(string url)
        {
            HttpClient _httpClient;
            var clientHandler = new HttpClientHandler();
            if (cookies != null)
            {
                clientHandler.CookieContainer = new CookieContainer();
                clientHandler.CookieContainer.Add(cookies);
                clientHandler.UseCookies = true;
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

                        Downloading?.Invoke(image);
                    }
                    else
                    {
                        Downloading?.Invoke(image);
                    }
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                Downloading?.Invoke(image);
            }
        }
    }
}