namespace project_2.Data
{
    public class TimeRegistration
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        public Project Project { get; set; }

        public DateTime Date { get; set; }

        public int AmountTime { get; set; }
        public string Description { get; set; }
    }
}
