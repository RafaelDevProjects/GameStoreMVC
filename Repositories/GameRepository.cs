using GameStoreMVC.Data;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreMVC.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> ObterTodosAsync()
        {
            return await _context.Games
                .OrderByDescending(g => g.CriadoEm)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> ObterDestaqueAsync()
        {
            return await _context.Games
                .Where(g => g.Destaque)
                .OrderByDescending(g => g.CriadoEm)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> ObterPorCategoriaAsync(string categoria)
        {
            return await _context.Games
                .Where(g => g.Categoria.ToLower() == categoria.ToLower())
                .OrderByDescending(g => g.CriadoEm)
                .ToListAsync();
        }

        public async Task<Game?> ObterPorIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task AdicionarAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public async Task AtualizarAsync(Game game)
        {
            _context.Games.Update(game);
        }

        public Task RemoverAsync(int id)
        {
            var game = _context.Games.Find(id);
            if (game != null)
                _context.Games.Remove(game);
            return Task.CompletedTask;
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
