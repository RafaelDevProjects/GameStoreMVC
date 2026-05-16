using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStoreMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _usuarioRepository.ObterPorEmailAsync(model.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.SenhaHash))
            {
                ModelState.AddModelError("", "Email ou senha inválidos.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("IsAdmin", usuario.IsAdmin.ToString())
            };

            if (usuario.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(7) });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(CadastroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _usuarioRepository.EmailExisteAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Este email já está cadastrado.");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha),
                IsAdmin = false
            };

            await _usuarioRepository.AdicionarAsync(usuario);
            await _usuarioRepository.SalvarAsync();

            TempData["Sucesso"] = "Conta criada com sucesso! Faça login.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
