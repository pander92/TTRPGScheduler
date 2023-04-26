using System;
using System.ComponentModel.DataAnnotations;
namespace TTRPGScheduler.Models
{
	public class PlayerAttendance
	{
		[Key]
		public int attendanceId { get; set; }
		public int sessionId { get; set; }
		public int playerId { get; set; }
		public bool availability { get; set; }
		public PlayerAttendance(int psessionId, int pplayerId, bool pavailability)
		{
			sessionId = psessionId;
			playerId = pplayerId;
			availability = pavailability;
		}
        public PlayerAttendance()
        {
        }
    }
}

