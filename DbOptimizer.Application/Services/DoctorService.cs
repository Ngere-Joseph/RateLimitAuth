using DbOptimizer.Core.Entities;
using DbOptimizer.Core.Interfaces;

namespace DbOptimizer.Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repository;
        public DoctorService(IDoctorRepository repository)
            => _repository = repository;

        public async Task<(List<Doctor> highFeeDocs, List<Doctor> lowFeeDocs)> GetDocAsync()
            =>  await _repository.GetDocsAsync();

        public async Task<string> GetDocSqlAsync()
            => await _repository.GetDocsSqlAsync();
    }
}
