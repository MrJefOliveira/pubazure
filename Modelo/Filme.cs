using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Table("Filme")]
    public class Filme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Display(Name = "Data de criação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DtaCriacao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Selecione um gênero")]
        [ForeignKey("Genero")]
        public int IdGenero { get; set; }
        
        public virtual ICollection<Locacao> Locacao { get; set; }

        public virtual Genero Genero { get; set; }

        public virtual List<Genero> ListaGenero()
        { return new List<Genero>(); }
    }
}
