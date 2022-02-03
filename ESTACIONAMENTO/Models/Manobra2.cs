using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESTACIONAMENTO.Models
{
    public class Manobra2 : Base
    {
        
        [ScaffoldColumn(false)]
        public DateTime DataEntrada { get; set; }
        
        public DateTime DataSaida { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Classificacao { get; set; }

        public string Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        public virtual int CarroId { get; set; }
        public virtual Carro Carro { get; set; }

        public virtual int ManobristaId { get; set; }
        public virtual Manobrista Manobrista { get; set; }
    }
}
