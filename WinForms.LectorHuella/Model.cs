using System;

namespace WinForms.LectorHuella
{
    public class Model
    {
        //public string id { get; set; }
        //public Guid id { get; } = Guid.NewGuid();
        public Guid id { get; set; }
        public string nombre { get; set; }
        public byte[] huella { get; set; }
        public DateTime fechaAlta { get; set; }
    }
}