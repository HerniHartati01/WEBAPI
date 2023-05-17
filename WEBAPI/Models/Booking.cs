﻿namespace WEBAPI.Models
{
    public class Booking : BaseEntity
    {
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public DateTime ModifiedOne { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
    }
}