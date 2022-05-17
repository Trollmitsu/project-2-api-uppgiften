using project_2.Data;

namespace project_2.DTO.TimeRegDTO
{
    public class GetTimeRegDTO
    {
        public int Id { get; set; }
        public int AmountTime { get; set; }


        public string Description { get; set; }

        public int Customer { get; set; }
        public int Project { get; set; }
    }
}
