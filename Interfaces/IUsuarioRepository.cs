using GameStoreMVC.Models;

namespace GameStoreMVC.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<bool> EmailExisteAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task SalvarAsync();
    }
}
