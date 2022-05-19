namespace project_2.DTO.TimeRegDTO
{
    public class DeleteTimeRegDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProjectId { get; set; }

        public DateTime Date { get; set; }

        public int AmountTime { get; set; }
        public string Description { get; set; }
    }
}
