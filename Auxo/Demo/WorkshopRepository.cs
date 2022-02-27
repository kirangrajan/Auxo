using System.Collections.Generic;

using static Demo.WorkshopRepository;

namespace Demo
{
    public interface IWorkshopRepository
    {
        /// <summary>
        /// Logs a time entry
        /// </summary>
        /// <param name="entry">Time entry to log</param>
        void AddEntry(TimeEntry entry);

        /// <summary>
        /// Returns all time entries for a given job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        List<TimeEntry> GetTimeEntries(string jobId);
    }

    public class WorkshopRepository : IWorkshopRepository
    {
        public record TimeEntry(string JobId, string Time, bool IsClockOn);

        private readonly List<TimeEntry> _timeEntries = new();

        public WorkshopRepository() { 
        }

        public void AddEntry(TimeEntry entry)
        {
            _timeEntries.Add(entry);
        }

        public List<TimeEntry> GetTimeEntries(string jobId)
        {
            return _timeEntries.Where(entry => entry.JobId == jobId).ToList();
        }
    }
}
