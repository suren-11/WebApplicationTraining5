﻿using WebApplicationTraining5.Enums;

namespace WebApplicationTraining5.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nic { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime created {  get; set; }
        public DateTime updated { get; set; }
    }
}
