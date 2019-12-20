using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Modelo
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O campo nome deve ser preenchido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo login deve ser preenchido")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo senha deve ser preenchido")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo confirme a senha deve ser preenchido")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a respectiva confirmação não conferem")]
        public string ConfirmacaoSenha { get; set; }
    }
}