using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Models
{
    public class Tipo
    {   [Key]
        [Column("TipoId")]
        public Guid Id { get; set; }
        public string TipoImovel { get; set; }
    }
}
