﻿namespace project_2.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<Project> Projects { get; set; } = new();
    }
}
