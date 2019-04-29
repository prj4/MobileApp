using System;
using System.Threading;

namespace Photobook.Models
{
    public delegate void ServerNotice(MediaEventArgs e);

    public class MediaEventArgs : EventArgs
    {
        public bool SendSucceeded { get; set; }
    }

    internal interface IMediaUploader
    {
        event ServerNotice NotifyDone;
        void SendMedia(string path, string eventId, DataType d);
    }

    public class MediaUploader : IMediaUploader
    {
        private readonly Guest currentGuest;

        public MediaUploader(Guest g)
        {
            currentGuest = g;
        }

        public event ServerNotice NotifyDone;

        public void SendMedia(string path, string eventId, DataType d)
        {
            var t = new Thread(() => SendThread(path, eventId, d));
            t.Start();
        }

        private async void SendThread(string path, string eventId, DataType d)
        {
            IServerCommunicator com = new ServerCommunicator();

            var ps = new PhotoToServer
            {
                Path = path,
                Pin = eventId
            };
            var cookies = await SettingsManager.GetCookies($"{currentGuest.Username}");

            com.AddCookies(cookies);

            var success = await com.SendDataReturnIsValid(ps, DataType.Picture);

            NotifyDone?.Invoke(new MediaEventArgs
            {
                SendSucceeded = success
            });
        }
    }
}