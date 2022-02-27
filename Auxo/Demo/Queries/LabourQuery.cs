namespace Demo.Queries
{
    public interface ILabourQuery
    {
        /// <summary>
        /// Totalises the labour costs from clockings on a job
        /// </summary>
        /// <param name="jobId">ID of the job to get total cost frome</param>
        /// <param name="rate">The hourly charge out rate for labour work</param>
        /// <returns>The dollar amount for the total labour cost of this job</returns>
        decimal GetTotalLabourCost(string jobId, decimal rate);
    }

    public class LabourQuery : ILabourQuery
    {
        private readonly IWorkshopRepository _workshopRepository;

        public LabourQuery(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public decimal GetTotalLabourCost(string jobId, decimal rate)
        {
            var entries = _workshopRepository.GetTimeEntries(jobId);

            var working = false;
            var clocking = DateTime.MinValue;
            var hours = 0d;
            foreach (var entry in entries)
            {
                var time = DateTime.Parse(entry.Time);

                if (working)
                {
                    var worked = time - clocking;
                    hours += worked.Hours + (worked.Minutes / 60d);
                }

                clocking = time;
                working = entry.IsClockOn;
            }
            var charge = Math.Round((decimal)hours * rate, 2);
            return charge;
        }
    }
}
