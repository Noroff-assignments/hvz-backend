﻿namespace hvz_backend.Models.DTOs.Player
{
    public class PlayerUpdateDTO
    {
        public int? SquadId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int GameId { get; set; }
        public string UserID { get; set; }
    }
}
