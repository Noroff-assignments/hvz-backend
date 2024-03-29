﻿using hvz_backend.Models;

namespace hvz_backend.Services.PlayerServices
{
    public interface IPlayerService
    {
        Task<Player> CreatePlayer(Player supply);
        Task<IEnumerable<Player>> GetAllPlayersInGame(int gameId);
        Task<IEnumerable<Player>> GetAllZombiesInGame(int gameId);
        Task<IEnumerable<Player>> GetAllHumansInGame(int gameId);
        Task<List<string>> GetAllBiteCodeInGame(int gameId);
        Task<Player> GetPlayerByIdInGame(int gameId, int id);

        Task<Player> GetPlayerByBiteCodeInGame(int gameId, string biteCode);

        Task<Player> GetPlayerByUserIdInGame(int gameId, string userId);

        Task<Player> UpdatePlayer(Player player);
        Task DeletePlayer(int id);

        Task PatchLatPlayer(int gameId, int id, double lat);
        Task PatchlongPlayer(int gameId, int id, double lon);
        Task PatchSquadPlayer(int gameId, int id, int squadId);
        Task PatchIsZombiePlayer(int gameId, int id, bool zombie);
    }
}
