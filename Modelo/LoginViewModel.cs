using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Modelo
{
    public class LoginViewModel
    {
        
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}