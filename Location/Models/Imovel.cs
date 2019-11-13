using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Models
{
    public class Imovel
    {
        [Key]
        [Column("ImovelId")]
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }


        [DisplayName("Tipo")]
        public Guid TipoId { get; set; }
        [ForeignKey("TipoId")]
        public virtual Tipo Tipo { get; set; }

        [Required]
        [DisplayName("Quartos")]
        public int QUartos { get; set; }

        [Required]
        public int Banheiros { get; set; }

        [Required]
        public int Vagas { get; set; }

        [Required]
        [DisplayName("Área")]
        public float Area { get; set; }
                
        [Required]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        [Required]
        [DisplayName("Número")]
        public string Numero { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        [DisplayName("UF")]
        public string Uf { get; set; }

       
        
        public virtual ICollection<Foto> Fotos { get; set; }

        public Contrato Contrato { get; set; }

        public string Diretorio(IHostingEnvironment _environment,Guid IdImovel)
        {
            string path = _environment.WebRootPath + "\\Storage\\" + IdImovel;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
