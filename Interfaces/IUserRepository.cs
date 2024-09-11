using System;
namespace FitInsight.Interfaces
{
	public interface IUserRepository
	{
		Task<string> GetUserNameByIdAsync(Guid userId);
	}
}

