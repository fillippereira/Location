using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Models
{
    public class Foto
    {
        [Key]
        [Column("FotoId")]
        public Guid Id { get; set; }

        public Guid ImovelId { get; set; }
        [ForeignKey("ImovelId")]
        public virtual Imovel Imovel { get; set; }

        public string Caminho { get; set; }
    }
}
