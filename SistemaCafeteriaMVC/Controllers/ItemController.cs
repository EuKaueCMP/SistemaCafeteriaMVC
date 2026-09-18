using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SistemaCafeteriaMVC.Data;
using SistemaCafeteriaMVC.Models;

namespace SistemaCafeteriaMVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext ctx;
        public ItemController(AppDbContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioLogado") == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            List<ItensCafeteria> itens = ctx.ItensCafeteria.ToList();

            return View(itens);
        }

        [HttpGet]
        public IActionResult CadastrarItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string descricao, decimal preco)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) || preco <= 0)
            {
                ViewBag.Erro = "Preencha todos os campos corretamente!";
                return View("Cadastro");
            }

            ItensCafeteria item = new Models.ItensCafeteria
            {
                Nome = nome,
                Descricao = descricao,
                Preco = preco
            };

            ctx.ItensCafeteria.Add(item);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int itemId)
        {
            var itens = ctx.ItensCafeteria.ToList();
            if (itemId == null || itemId <= 0)
            {
                ViewBag.Erro = "Nenhum item encontrado";
                return View("Index", itens);
            }

            var item = ctx.ItensCafeteria.Find(itemId);
            if (item == null)
            {
                ViewBag.Erro = "Item não encontrado";
                return View("Index", itens);
            }

            ctx.ItensCafeteria.Remove(item);
            ctx.SaveChanges();
            return View("Index", itens);
        }

        [HttpGet]
        public IActionResult EditarItem(int id)
        {
            var item = ctx.ItensCafeteria.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult EditarItem(int id, string nome, string descricao, decimal preco)
        {
            var itens = ctx.ItensCafeteria.ToList();
            if (id == null || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) || preco == null)
            {
                ViewBag.Erro = "Todos os campos são obrigatorios!";
                return View("Index", itens);
            }

            var item = ctx.ItensCafeteria.Find(id);
            item.Nome = nome;
            item.Descricao = descricao;
            item.Preco = preco;

            ctx.ItensCafeteria.Update(item);
            ctx.SaveChanges();

            return View("Index", itens);    
        }
    }
}
