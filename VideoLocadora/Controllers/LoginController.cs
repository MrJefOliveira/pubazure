using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using VideoLocadora.Contexto;
using Utilitarios;
using System.Security.Claims;

namespace VideoLocadora.Controllers
{
    public class LoginController : Controller
    {
        private EFContext contexto = new EFContext();

        [HttpGet]
        public ActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroUsuario(UsuarioViewModel usuarioViewModel)
        {   
            if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);
            }
            
            //Verifica se já existe usuário cadastrado com mesmo login
            if (contexto.Usuario.Count(u => u.Login == usuarioViewModel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Já existe usuário com este Login");
                return View(usuarioViewModel);
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioViewModel.Nome,
                Login = usuarioViewModel.Login,
                Senha = GeradorHash.GeraHash(usuarioViewModel.Senha)
            };

            contexto.Usuario.Add(usuario);
            contexto.SaveChanges();

            
            TempData["Mensagem"] = "Cadastro realizado com sucesso. Favor efetuar login.";

            return RedirectToAction("Login");
        }

        public ActionResult Login(string ReturnUrl)
        {
            var loginViewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(loginViewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            //verificar erro de validação
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            //localizar usuario no DB
            var usuario = contexto.Usuario.FirstOrDefault(u => u.Login == loginViewModel.Login);
            //verificando dados login
            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(loginViewModel);
            }
            if (usuario.Senha != GeradorHash.GeraHash(loginViewModel.Senha)) 

            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(loginViewModel);
            }

            //define identidade do usuario
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            //redirecionamento do usuario logado
            if (!String.IsNullOrWhiteSpace(loginViewModel.UrlRetorno) || Url.IsLocalUrl(loginViewModel.UrlRetorno))
                return Redirect(loginViewModel.UrlRetorno);
            else
                return RedirectToAction("Index", "Genero");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}