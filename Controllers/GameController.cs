using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET: /Game
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var games = await _gameRepository.ObterTodosAsync();
            return View(games);
        }

        // GET: /Game/Criar
        [Authorize(Roles = "Admin")]
        public IActionResult Criar()
        {
            return View();
        }



        // POST: /Game/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar(Game game)
        {
            if (!ModelState.IsValid)
                return View(game);

            game.CriadoEm = DateTime.UtcNow;
            await _gameRepository.AdicionarAsync(game);
            await _gameRepository.SalvarAsync();

            TempData["Sucesso"] = $"Jogo '{game.Titulo}' cadastrado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Game/Editar/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id)
        {
            var game = await _gameRepository.ObterPorIdAsync(id);
            if (game == null)
                return NotFound();
            return View(game);
        }

        // POST: /Game/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id, Game game)
        {
            if (id != game.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(game);

            await _gameRepository.AtualizarAsync(game);
            await _gameRepository.SalvarAsync();

            TempData["Sucesso"] = $"Jogo '{game.Titulo}' atualizado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        // POST: /Game/Excluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Excluir(int id)
        {
            var game = await _gameRepository.ObterPorIdAsync(id);
            if (game == null)
                return NotFound();

            await _gameRepository.RemoverAsync(id);
            await _gameRepository.SalvarAsync();

            TempData["Sucesso"] = $"Jogo excluído com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Game/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var game = await _gameRepository.ObterPorIdAsync(id);
            if (game == null)
                return NotFound();
            return View(game);
        }
    }
}
