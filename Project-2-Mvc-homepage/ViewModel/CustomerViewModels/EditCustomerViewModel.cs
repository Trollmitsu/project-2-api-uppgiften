using System.ComponentModel.DataAnnotations;

namespace Project_2_Mvc_homepage.ViewModel.CustomerViewModels
{
    public class EditCustomerViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
