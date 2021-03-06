﻿using System;
using System.IO;
using System.Threading;

namespace Photobook.Models
{
    public delegate void ServerNotice(MediaEventArgs e);

    public class MediaEventArgs : EventArgs
    {
        public bool SendSucceeded { get; set; }
    }

    public interface IMediaUploader
    {
        event ServerNotice NotifyDone;
        void SendMedia(string path, string eventId, DataType d);
    }

    public class MediaUploader : IMediaUploader
    {
        private readonly Guest currentGuest;
        private IMemoryManager _memoryManager;

        public MediaUploader(Guest g, IMemoryManager memoryManager = null)
        {
            currentGuest = g;
            _memoryManager = memoryManager ?? MemoryManager.GetInstance();
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
                Bytes = File.ReadAllBytes(path),
                Pin = eventId
            };
            var cookies = await _memoryManager.GetCookies($"{currentGuest.Username}");

            com.AddCookies(cookies);

            var success = await com.SendDataReturnIsValid(ps, DataType.Picture);

            NotifyDone?.Invoke(new MediaEventArgs
            {
                SendSucceeded = success
            });
        }
    }
}