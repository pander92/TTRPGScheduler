using System;
using System.ComponentModel.DataAnnotations;
namespace TTRPGScheduler.Models
{
	public class ProposedSession
	{
		[Key]
		public int sessionId { get; set;  }
		public DateOnly sessionDate { get; set; }
		public bool viability { get; set; }
		public bool allPlayers { get; set; }
		public ProposedSession()
		{
		}
	}
}

