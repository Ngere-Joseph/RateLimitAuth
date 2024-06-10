using DbOptimizer.Core.Entities;

namespace DbOptimizer.Core.Interfaces
{
    public interface IDoctorRepository
    {
        Task<(List<Doctor> highFeeDocs, List<Doctor> lowFeeDocs)> GetDocsAsync();
        Task<string> GetDocsSqlAsync();
        Task UpdateHighFeeDoctorsStatusAsync();
    }
}
