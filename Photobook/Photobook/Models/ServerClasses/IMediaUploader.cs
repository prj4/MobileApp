using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Photobook.Models
{
    public delegate void ServerNotice(MediaEventArgs e);

    public class MediaEventArgs : EventArgs
    {
        public bool SendSucceeded { get; set; }
    }
    interface IMediaUploader
    {
        event ServerNotice NotifyDone;
        void SendMedia(string path, string eventId, DataType d);
    }

    public class MediaUploader : IMediaUploader
    {
        public event ServerNotice NotifyDone;
        public void SendMedia(string path, string eventId, DataType d)
        {
           Thread t = new Thread(() => SendThread(path, eventId, d));
           t.Start();
        }

        private async void SendThread(string path, string eventId, DataType d)
        {
            IServerCommunicator com = new ServerCommunicator();

            PhotoToServer ps = new PhotoToServer
            {
                Path = path,
                Pin = eventId
            };

            bool success = await com.SendDataReturnIsValid(ps, DataType.Picture);

            NotifyDone?.Invoke(new MediaEventArgs
            {
                SendSucceeded = success
            });
        }
    }
}
