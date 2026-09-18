using Microsoft.AspNetCore.Mvc;
using SistemaCafeteriaMVC.Data;
using SistemaCafeteriaMVC.Models;
using SistemaCafeteriaMVC.Services;

namespace SistemaCafeteriaMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext ctx;
        public UsuarioController(AppDbContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) | string.IsNullOrEmpty(senha))
            {
                ViewBag.Erro = "Preencha todos os campos!";
                return View();
            }

            Usuario usuario = ctx.Usuario.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                ViewBag.Erro = "Email ou senha incorretos!";
                return View();
            }

            byte[] senhaConvertida = HashService.Hash(senha);
            if (!senhaConvertida.SequenceEqual(usuario.Senha))
            {
                ViewBag.Erro = "Email ou senha incorretos!";
                return View();
            }

            HttpContext.Session.SetString("UsuarioLogado", usuario.Email);
            HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);

            // Login bem-sucedido
            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string senha)
        {
            if (string.IsNullOrEmpty(nome) | string.IsNullOrEmpty(email) | string.IsNullOrEmpty(senha))
            {
                ViewBag.Erro = "Preencha todos os campos!";
                return View();
            }
            Usuario usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = HashService.Hash(senha)
            };
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}
