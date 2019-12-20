using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Table("Genero")]
    public class Genero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome do gênero")]
        [MaxLength(20)]
        public string Nome { get; set; }

        [Display(Name = "Data de criação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DtaCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
