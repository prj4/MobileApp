using System;
using System.Collections.Generic;
using System.Text;
using PB.Dto;

namespace Photobook.Models.ServerClasses
{
    public interface IUrlPostfix
    {
        string Generate(object o);
    }

    public class EventPostFix : IUrlPostfix
    {
        public string Generate(object o)
        {
            EventModel em = o as EventModel;

            return em.Pin;
        }
    }

    public class PicturePostfix : IUrlPostfix
    {
        public string Generate(object o)
        {
            TestImage ti = o as TestImage;
            return ti.PinId;
        }
    }

    public class GuestPostfix : IUrlPostfix
    {
        public string Generate(object o)
        {
            Guest g = o as Guest;

            return g.Username;
        }
    }
}
