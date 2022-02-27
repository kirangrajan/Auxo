namespace Demo.Queries
{
    public interface IJobQuery
    { 
        bool DoesJobExist(string jobId);
    }

    public class JobQuery : IJobQuery
    {
        private readonly IWorkshopRepository _workshopRepository;

        public JobQuery(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public bool DoesJobExist(string jobId)
        {
            return _workshopRepository.GetTimeEntries(jobId).Any();
        }
    }
}
