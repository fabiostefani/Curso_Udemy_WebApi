using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fabiostefani.io.WebApp.Api.Models
{
    public class AlunoDto
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="O nome é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage ="O nome tem que ter no mínimo 2 caracteres e no máximo 50", MinimumLength =2)]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O RA é de preenchimento obrigatório")]
        [Range(1,999,ErrorMessage = "O intervalo para cadastro de RA está 1 e 999")]
        public int? Ra { get; set; }
        [RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "A data está fora do formato YYYY-MM")]
        public string Data { get; set; }
    }
}