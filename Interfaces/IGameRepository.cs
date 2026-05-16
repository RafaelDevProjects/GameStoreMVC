using GameStoreMVC.Models;

namespace GameStoreMVC.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> ObterTodosAsync();
        Task<IEnumerable<Game>> ObterDestaqueAsync();
        Task<IEnumerable<Game>> ObterPorCategoriaAsync(string categoria);
        Task<Game?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Game game);
        Task AtualizarAsync(Game game);
        Task RemoverAsync(int id);
        Task SalvarAsync();



    }
}
