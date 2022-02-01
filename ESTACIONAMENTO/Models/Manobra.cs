using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESTACIONAMENTO.Models
{
    public class Manobra : Base
    {

        [ScaffoldColumn(false)]
        public DateTime DataManobra { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Classificacao { get; set; }

        public int TipoManobra { get; set; }

        public virtual int CarroId { get; set; }
        public virtual Carro Carro { get; set; }
        
        public virtual int ManobristaId { get; set; }
        public virtual Manobrista Manobrista { get; set; }

    }
}
