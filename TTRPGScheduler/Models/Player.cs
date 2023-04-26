using System;
using System.ComponentModel.DataAnnotations;

namespace TTRPGScheduler.Models
{
	public class Player
	{
		[Key]
		public int playerId { get; set; }
		public string playerName { get; set; } = "unknown";
	}
}

