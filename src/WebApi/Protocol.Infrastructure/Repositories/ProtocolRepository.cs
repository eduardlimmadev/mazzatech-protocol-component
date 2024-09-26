using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Protocol.Domain.Interfaces;
using Protocol.Infrastructure.Data;

namespace Protocol.Infrastructure.Repositories
{
    public class ProtocolRepository : IProtocolRepository
    {
        private readonly ProtocolDbContext _dbContext;
        private readonly ILogger<ProtocolRepository> _logger;

        public ProtocolRepository(ProtocolDbContext dbContext, ILogger<ProtocolRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Domain.Entities.Protocol> AddAsync(Domain.Entities.Protocol protocol)
        {
            try
            {
                await _dbContext.Protocols.AddAsync(protocol);
                await _dbContext.SaveChangesAsync();

                return protocol;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error when trying to save the protocol in the database - Protocol: {protocol}", protocol.ProtocolNumber);
                throw;
            }
        }

        public async Task<bool> ExistsByProtocolNumberAsync(long protocolNumber)
            => await _dbContext.Protocols
                .AsNoTracking()
                .AnyAsync(p => p.ProtocolNumber == protocolNumber);

        public async Task<bool> ExistsByRgOrCpfAndViaAsync(string rgOrCpf, int viaNumber)
            => await _dbContext.Protocols
                .AsNoTracking()
                .AnyAsync(p => (p.Rg.Number.Trim().Equals(rgOrCpf.Trim()) || p.Cpf.Number.Trim().Equals(rgOrCpf.Trim())) && p.ViaNumber == viaNumber);


        public async Task<IEnumerable<Domain.Entities.Protocol>> GetAllProtocolsByCpfAsync(string cpf)
            => await _dbContext.Protocols
                .AsNoTracking()
                .Where(p => p.Cpf.Number.Trim().Equals(cpf.Trim()))
                .ToListAsync();

        public async Task<IEnumerable<Domain.Entities.Protocol>> GetAllProtocolsByRgAsync(string rg)
            => await _dbContext.Protocols
                .AsNoTracking()
                .Where(p => p.Rg.Number.Trim().Equals(rg.Trim()))
                .ToListAsync();

        public async Task<Domain.Entities.Protocol?> GetProtocolByNumberAsync(long protocolNumber)
            => await _dbContext.Protocols
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProtocolNumber == protocolNumber);
    }
}
