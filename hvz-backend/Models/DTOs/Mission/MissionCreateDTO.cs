﻿using System.ComponentModel.DataAnnotations;

namespace hvz_backend.Models.DTOs.Mission
{
    public class MissionCreateDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool ZombieVisible { get; set; }
        public bool HumanVisible { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MapId { get; set; }
    }
}