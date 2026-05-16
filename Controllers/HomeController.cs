using GameStoreMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public HomeController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IActionResult> Index(string? categoria)
        {
            var games = string.IsNullOrEmpty(categoria)
                ? await _gameRepository.ObterTodosAsync()
                : await _gameRepository.ObterPorCategoriaAsync(categoria);

            var destaques = await _gameRepository.ObterDestaqueAsync();

            ViewBag.Categoria = categoria;
            ViewBag.Destaques = destaques;
            return View(games);
        }
    }
}
