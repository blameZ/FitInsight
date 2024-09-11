using System;
using FitInsight.Models.UserModels;

namespace FitInsight.Interfaces
{
	public interface IUserRepository
	{
		Task<string> GetUserNameByIdAsync(Guid userId);
		Task AddUserWeightHistoryAsync(string userId, float weight);
		Task<List<UserWeightHistory>> GetUserWeightHistoriesByUserIdAsync(string userId);
    }
}

