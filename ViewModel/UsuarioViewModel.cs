using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O nome deve ser preenchido")]
        [MaxLength(100, ErrorMessage = "O nome deve ter até 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe corretamente o seu login")]
        [MaxLength(50, ErrorMessage = "O login deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "É obrigatório a criação de senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha digitada no campo acima")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a respectiva confirmação não conferem")]
        public string ConfirmacaoSenha { get; set; }
    }
}