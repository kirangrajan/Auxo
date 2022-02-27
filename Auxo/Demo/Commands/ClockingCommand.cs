using System;

namespace Demo.Commands
{
    public interface IClockingCommand
    {
        /// <summary>
        /// Begins the clocking on a given job
        /// </summary>
        /// <param name="jobId">ID of the job to clock onto</param>
        void ClockOn(string jobId);

        /// <summary>
        /// Completes the clocking on a given job
        /// </summary>
        /// <param name="jobId">ID of the job to clock off of</param>
        void ClockOff(string jobId);
    }

    public class ClockingCommand : IClockingCommand
    {
        private readonly IWorkshopRepository _workshopRepository;

        public ClockingCommand(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public void ClockOff(string jobId)
        {
            _workshopRepository.AddEntry(new WorkshopRepository.TimeEntry(jobId, DateTime.Now.AddHours(11).ToString(), false));
        }

        public void ClockOn(string jobId)
        {
            _workshopRepository.AddEntry(new WorkshopRepository.TimeEntry(jobId, DateTime.Now.ToString(), true));
        }
    }
}
