using System;

namespace GetServiceApi.DTOs
{
    public class MensagemDto
    {
        public string remetenteUserName { get; set; }

        public string DestinatarioUserName { get; set; }
        
        public string Texto { get; set; }

        public DateTime Data { get; set; }
    }
}