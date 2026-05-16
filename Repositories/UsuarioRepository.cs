using GameStoreMVC.Data;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreMVC.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
