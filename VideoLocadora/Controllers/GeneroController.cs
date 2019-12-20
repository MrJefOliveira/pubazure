using Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using VideoLocadora.Contexto;
using System.Net;
using System.Data.Entity;
using PersistenciaDapper;

namespace VideoLocadora.Controllers
{
    public class GeneroController : Controller
    {
        private EFContext contexto = new EFContext();
        Persistencia persistencia = new Persistencia();

        [HttpGet]
        public ActionResult Index()
        {
            List<Genero> generos = new List<Genero>();

            //Utilizando Dapper para consultar          
            generos = persistencia.ConsultarGenero();

            return View(generos);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            Genero genero = new Genero();
            genero.DtaCriacao = System.DateTime.Now;

            return View(genero);
        }

        [HttpPost]
        public ActionResult Cadastrar(Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return View("CadastroGenero", genero);
            }

            if (genero.Nome == null || genero.Nome == "")
            {
                ModelState.AddModelError("Nome", "Informar o Gênero");
                return View("CadastroGenero");
            }

            //Utilizando Dapper para incluir
            persistencia.IncluirGenero(genero);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Alterar(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genero genero = contexto.Genero.Find(Id);

            if (genero == null)
            {
                return HttpNotFound();
            }

            ViewBag.dtCriacao = genero.DtaCriacao;
            
            return View(genero);
        }

        [HttpPost]
        public ActionResult Alterar([Bind(Include = "Id, Nome, DtaCriacao, Ativo")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                //Utilizando Dapper para alterar
                persistencia.AlterarGenero(genero);

                return RedirectToAction("Index");
            }
            return View(genero);
        }

        [HttpGet]
        public ActionResult Excluir(int[] Id)
        {
            if (Id == null)
            {
                TempData["Mensagem"] = "Nenhum item foi selecionado para excluir.";
            }
            else
            {
                foreach (int item in Id)
                {
                    //Utilizando Dapper para excluir
                    persistencia.ExcluirGenero(item);
                }
            }
            
            return View("Index", contexto.Genero.ToList());
        }
    }
}