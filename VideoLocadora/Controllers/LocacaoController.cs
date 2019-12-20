using Dapper;
using Modelo;
using PersistenciaDapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoLocadora.Contexto;

namespace VideoLocadora.Controllers
{
    public class LocacaoController : Controller
    {
        private EFContext contexto = new EFContext();
        Persistencia persistencia = new Persistencia();

        [HttpGet]
        public ActionResult Index()
        {
            return View(contexto.Locacao.ToList());
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            //Carrega lista de filmes na ViewBag
            ViewBag.Filme = contexto.Filme;

            Locacao locacao = new Locacao();
            locacao.DtaLocacao = System.DateTime.Now;

            return View(locacao);
            
        }

        [HttpPost]
        public ActionResult Cadastrar(int[] FilmeId, Locacao locacao)
        {

            if (!ModelState.IsValid)
            {
                return View("Cadastrar");

            }

            if (locacao.CPF == null || locacao.CPF == "")
            {
                ModelState.AddModelError("CPF", "Informar o CPF");
                return View("Cadastrar");
            }

            //Busca os filmes selecionados para incluir
            locacao.Filme = new List<Filme>();

            if(FilmeId != null)
            {
                foreach (int filmeId in FilmeId)
                {
                    Filme filme = contexto.Filme.Find(filmeId);
                    ViewBag.Filme = filme;

                    locacao.Filme.Add(filme);
                }
            }

            if (FilmeId == null)
            {
                TempData["Mensagem"] = "Não existe nenhum Filme cadastrado, favor efetuar o cadastro.";
                return View("Cadastrar");
            }


            contexto.Locacao.Add(locacao);
            contexto.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Alterar(int Id)
        {
            var locacao = new List<Locacao>();
            var filme = new List<Filme>();

            //Seleciona dados da locação - tabela dbo.Locacao
            locacao = persistencia.ConsultarLocacao(Id);

            //Seleciona os filmes pertencentes a locação - tabela dbo.Filme e tabela dbo.LocacaoFilmes
            filme = persistencia.ConsultarFilmeLocado(Id);

            //Adiciona os filmes na ViewBag
            ViewBag.Filmes = filme;

            if (locacao == null)
            {
                return HttpNotFound();
            }

            Locacao loc = new Locacao();

            foreach (var item in locacao)
            {
                loc.Id = item.Id;
                loc.CPF = item.CPF;
                loc.DtaLocacao = item.DtaLocacao;
            }

            return View(loc);
        }

        [HttpPost]
        public ActionResult Alterar(Locacao locacao, int[] FilmeId)
        {
            if (ModelState.IsValid)
            {
                //Salva as alterações na tabela dbo.Locacao
                contexto.Entry(locacao).State = EntityState.Modified;
                contexto.SaveChanges();

                //Exclui os registros da tabela dbo.LocacaoFilmes
                persistencia.ExcluirLocacaoFilmes(locacao.Id);

                //Insere somente os filmes checkados na tabela dbo.LocacaoFilmes
                if (FilmeId != null)
                {
                    foreach (int filmeId in FilmeId)
                    {
                        persistencia.InserirLocacaoFilmes(locacao.Id, filmeId);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(locacao);
        }

        public ActionResult Excluir(int[] Id)
        {
            if (Id == null)
            {
                TempData["Mensagem"] = "Nenhum item foi selecionado para excluir.";
            }
            else
            {
                //Exclui as locações checkadas
                foreach (int item in Id)
                {
                    Locacao locacao = contexto.Locacao.Find(item);
                    contexto.Locacao.Remove(locacao);
                    contexto.SaveChanges();
                }
            }

            return View("Index", contexto.Locacao.ToList());
        }
    }
}