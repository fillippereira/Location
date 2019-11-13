using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Models
{
    public class Contrato
    {
        [Key]
        [Column("ContratoId")]
        public Guid Id { get; set; }

        [DisplayName("Imóvel")]
        public Guid ImovelId { get; set; }
        [ForeignKey("ImovelId")]
        public virtual Imovel Imovel { get; set; }

        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required]
        public float Valor { get; set; }
    }
}
