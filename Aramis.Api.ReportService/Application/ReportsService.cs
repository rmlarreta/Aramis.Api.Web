using Aramis.Api.ReportService.Interfaces;
using Aramis.Api.Repository.Interfaces.Reports;
using AutoMapper;

namespace Aramis.Api.ReportService.Application
{
    public class ReportsService : IReportsService
    {
        private IOperacionsReports _operacionsReports = null!;
        private readonly IReportsRepository _repository;
        private readonly IMapper _mapper;
        public ReportsService(IReportsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IOperacionsReports Operaciones => _operacionsReports ??= new OperacionsReports(_repository, _mapper);
    }
}
