using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickAPI.Controllers.Model
{
	public class Score
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(20)]
		public string HomeName { get; set; }
		[Required]
		[MaxLength(20)]
		public string HomeScore { get; set; }
		[Required]
		[MaxLength(20)]
		public string AwayName { get; set; }
		[Required]
		[MaxLength(20)]
		public string AwayScore { get; set; }
	}
}
