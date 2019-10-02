using AtUlisses.Dominio;
using AtUlisses.Repository;
using Dominio;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtUlisses.Controllers
{
    public class PessoaController : Controller
    {


       private IRepositorioPessoa repository = new PessoaRepository ();

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = repository.GetPessoas();
            
            ViewBag.Aniversariantes = AniversariantesDoDia(DateTime.Today);

            return View(pessoas);
          
        }


        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {
            return View(buscarPorId(id));
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                repository.Create(pessoa.Nome, pessoa.Sobrenome, pessoa.Aniversario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            return View(buscarPorId(id));
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pessoa pessoa)
        {


            Pessoa pessoaEdit = new Pessoa();
            pessoaEdit.Nome = pessoa.Nome;
            pessoaEdit.Sobrenome = pessoa.Sobrenome;
            pessoaEdit.Aniversario = pessoa.Aniversario;




            try
            {
                repository.update(id, pessoaEdit);



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            return View(buscarPorId(id));
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Pessoa pessoa)
        {
            Pessoa pessoaDelete = new Pessoa();
            pessoaDelete.Nome = pessoa.Nome;
            pessoaDelete.Sobrenome = pessoa.Sobrenome;
            pessoaDelete.Aniversario = pessoa.Aniversario;




            try
            {
                repository.delete(id, pessoaDelete);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public Pessoa buscarPorId(int id)
        {
            Pessoa resultado = new Pessoa();
            foreach (Pessoa p in repository.GetPessoas())
            {
                if (p.Id == id)
                {
                    resultado.Id = id;
                    resultado.Nome = p.Nome;
                    resultado.Sobrenome = p.Sobrenome;
                    resultado.Aniversario = p.Aniversario;
                    break;
                }
            }
            return resultado;
        }

        public List<Pessoa> AniversariantesDoDia(DateTime Aniversario)
        {

            List<Pessoa> aniversariantes = new List<Pessoa>();
            var listPessoas = repository.GetPessoas();
            foreach (Pessoa p in listPessoas)
            {
                if (p.Aniversario == DateTime.Now)
                {
                    aniversariantes.Add(p);
                }
            }
            return aniversariantes;
        }



            



        


        public List<Pessoa> buscarPorNome(string pesquisa)
        {
            List<Pessoa> resultadosList = new List<Pessoa>();
            var listPessoas = repository.GetPessoas();
            foreach (Pessoa p in listPessoas)
            {
                if (p.Nome.ToLower().Contains(pesquisa.ToLower()))
                {
                    resultadosList.Add(p);
                }
            }
            return resultadosList;
        }
        public ActionResult Search()
        {
            string pesquisa = "";
            return View(buscarPorNome(pesquisa));
        }
        [HttpPost]
        public ActionResult Search(string pesquisa)
        {
            List<Pessoa> listaDeBusca = buscarPorNome(pesquisa);

            try
            {
                return View(listaDeBusca);
            }
            catch
            {
                return View();
            }
        }
    }
}
















