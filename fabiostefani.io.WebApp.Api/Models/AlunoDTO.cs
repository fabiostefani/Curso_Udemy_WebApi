using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fabiostefani.io.WebApp.Api.Models
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int Ra { get; set; }
        public string Data { get; set; }
    }
}