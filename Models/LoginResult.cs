using System;
namespace FitInsight.Models
{
	public class LoginResult
	{
		public bool Succeeded { get; set; }
		public bool IsLockedOut { get; set; }
		public bool RequiresTwoFactor { get; set; }
		public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
	}
}

