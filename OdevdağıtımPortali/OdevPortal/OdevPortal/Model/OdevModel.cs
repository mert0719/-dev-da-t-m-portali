﻿using OdevPortal.Entity;

namespace OdevPortal.Model
{
    public class OdevModel
    {
        public int id { get; set; }
        public string odevAdi { get; set; }
        public string odevKonusu { get; set; }
        public int dersId { get; set; }
        public string? dersAdi { get; set; }
    }
}
