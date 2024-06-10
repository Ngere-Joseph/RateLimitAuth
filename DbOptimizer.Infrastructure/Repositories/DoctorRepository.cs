using DbOptimizer.Core.Entities;
using DbOptimizer.Core.Interfaces;
using DbOptimizer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DbOptimizer.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public DoctorRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<(List<Doctor> highFeeDocs, List<Doctor> lowFeeDocs)> GetDocsAsync()
        {
            var cacheKeyHighFee = "High-Consultancy";
            var cacheKeyLowFee = "Low-Consultancy";

            var highFeeTask = _cache.GetOrCreateAsync(cacheKeyHighFee, async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(5));
                return await _context.Doctors
                    .AsNoTracking()
                    .Where(i => i.ConsultFee > 5000)
                    .OrderBy(i => i.Name)
                    .ToListAsync();
            });

            var lowFeeTask = _cache.GetOrCreateAsync(cacheKeyLowFee, async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(5));
                return await _context.Doctors
                    .AsNoTracking()
                    .Where(i => i.ConsultFee <= 5000)
                    .OrderBy(i => i.Name)
                    .ToListAsync();
            });

            await Task.WhenAll(highFeeTask, lowFeeTask);

            return (await highFeeTask, await lowFeeTask);
        }


        public async Task<string> GetDocsSqlAsync()
        {
            var query = "SELECT * FROM Doctors WHERE ConsultFee > 5000 ORDER BY Name";
            var docs = await _context.Doctors.FromSqlRaw(query).ToListAsync();
            return $"Retrieved {docs.Count} Doctors.";
        }

        // Bg job method, for Task 4
        public async Task UpdateHighFeeDoctorsStatusAsync()
        {
            var doctors = await _context.Doctors
                .Where(d => d.ConsultFee > 5000 && d.DstType != "HighFee")
                .ToListAsync();

            foreach (var doctor in doctors)
            {
                doctor.DstType = "HighFee";
            }

            await _context.SaveChangesAsync();
        }

    }



}
