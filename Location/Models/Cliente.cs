using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Models
{
    public class Cliente
    {
        [Key]
        [Column("ClienteId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve possuir 11 caracteres")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage="Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Número")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("UF")]
        public string Uf { get; set; }
    }
}
