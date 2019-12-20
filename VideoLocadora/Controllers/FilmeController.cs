using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoLocadora.Contexto;
using System.Data.Entity;

namespace VideoLocadora.Controllers
{
    public class FilmeController : Controller
    {
        private EFContext contexto = new EFContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(contexto.Filme.ToList());
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            //Carrega o id do genero ativo para o Dropdown
            ViewBag.IdGenero = new SelectList(contexto.Genero.ToList().Where(x => x.Ativo), "Id", "Nome");

            Filme filme = new Filme();
            filme.DtaCriacao = System.DateTime.Now;

            return View(filme);
        }

        [HttpPost]
        public ActionResult Cadastrar(Filme filme)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdGenero = new SelectList(contexto.Genero.ToList(), "Id", "Nome");

                return View("Cadastrar", filme);
            }

            if (filme.Nome == null || filme.Nome == "")
            {
                ModelState.AddModelError("Nome", "Informar o Nome");

                return View("Cadastrar");
            }

            if (filme.IdGenero == 0)
            {
                TempData["Mensagem"] = "Não existe nenhum Gênero cadastrado, favor efetuar o cadastro.";
                return View("Cadastrar");
            }

            //Retorna o Id do genero
            ViewBag.IdGenero = new SelectList(new Filme().ListaGenero(), "Id", "Nome", filme.IdGenero);
    
            contexto.Filme.Add(filme);
            contexto.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Alterar(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = contexto.Filme.Find(Id);
            if (filme == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = new SelectList(contexto.Genero.ToList(), "Id", "Nome");
            ViewBag.dtCriacao = filme.DtaCriacao;


            return View(filme);
        }

        [HttpPost]
        public ActionResult Alterar([Bind(Include = "Id, Nome, DtaCriacao, Ativo, IdGenero")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                ViewBag.IdGenero = new SelectList(new Filme().ListaGenero(), "Id", "Nome", filme.IdGenero);
                contexto.Entry(filme).State = EntityState.Modified;
                contexto.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(filme);
        }
        public ActionResult Excluir(int[] Id)
        {
            if (Id == null)
            {
                TempData["Mensagem"] = "Nenhum item foi selecionado para excluir.";
            }

            if(Id!=null)
            {
                foreach (int item in Id)
                {
                    Filme filme = contexto.Filme.Find(item);
                    contexto.Filme.Remove(filme);
                    contexto.SaveChanges();
                }
            }

            return View("Index", contexto.Filme.ToList());
        }
    }
}