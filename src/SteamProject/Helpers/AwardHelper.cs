﻿using SteamProject.DAL.Abstract;
using SteamProject.Models;

namespace SteamProject.Helpers;

public class AwardHelper
{
    private readonly IUserBadgeRepository _userBadgeRepository;
    private readonly IBadgeRepository _badgeRepository;

    public AwardHelper(IUserBadgeRepository userBadgeRepository, IBadgeRepository badgeRepository)
    {
        _userBadgeRepository = userBadgeRepository;
        _badgeRepository = badgeRepository;
    }

    public async Task<bool> CheckAndAwardAsync(User user, IAwardCondition condition, int badgeId)
    {
        if (await _badgeRepository.BadgeExistsAsync(badgeId) && await condition.IsFulfilledAsync(user) && !await _userBadgeRepository.UserHasBadge(user.Id, badgeId))
        {
            await _badgeRepository.AwardBadgeAsync(user, badgeId);
            return true;
        }
        return false;
    }
}