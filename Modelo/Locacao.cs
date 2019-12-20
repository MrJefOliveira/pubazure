using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Table("Locacao")]
    public class Locacao
    {
        public int Id { get; set; }
        public virtual List<Filme> Filme { get; set; }
        
        [Display(Name = "CPF")]
        [MaxLength(14)]
        [Required(ErrorMessage = "Preencha o CPF")]
        public string CPF { get; set; }

        [Display(Name = "Data da Locação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DtaLocacao { get; set; }
        
    }
}
