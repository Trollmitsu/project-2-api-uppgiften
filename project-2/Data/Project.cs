﻿namespace project_2.Data
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public Customer Customer { get; set; } = new Customer();
    }
}
