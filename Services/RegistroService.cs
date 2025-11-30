using GymManager.Data;
using GymManager.Data.Gym;
using GymManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services
{
    public class RegistroService
    {
        private readonly ApplicationDbContext _context;

        public RegistroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Gym.GymManager>> ObterTodosAsync()
        {
            return await _context.Registros.OrderByDescending(x => x.DataCriacao).ToListAsync();
        }

        public async Task CriarAsync(Data.Gym.GymManager registro, string usuarioLogado)
        {
            bool jaExiste = await _context.Registros
                .AnyAsync(x => x.GymId == registro.GymId);
            if (!jaExiste)
            {
                registro.CriadoPorUsuario = usuarioLogado;
                registro.Status = EnumStatus.Novo;
                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();
            }

            return;
        }

        public async Task AtualizarStatusAsync(int id, EnumStatus novoStatus)
        {
            var item = await _context.Registros.FindAsync(id);
            if (item != null)
            {
                item.Status = novoStatus;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<RegistroPaginado> ObterPaginadoAsync(string termo, int paginaAtual, int tamanhoPagina)
        {
            var query = _context.Registros.AsQueryable();

            if (!string.IsNullOrWhiteSpace(termo))
            {
                query = query.Where(x => x.IdAcademia.Contains(termo) ||
                                         x.GymId.Contains(termo) ||
                                         x.CriadoPorUsuario.Contains(termo));
            }

  
            var total = await query.CountAsync();

            var itens = await query
                .OrderByDescending(x => x.DataCriacao)
                .Skip((paginaAtual - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new RegistroPaginado
            {
                Itens = itens,
                TotalRegistros = total
            };
        }
    }
}
