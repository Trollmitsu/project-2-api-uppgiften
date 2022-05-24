namespace Project_2_Mvc_homepage.ViewModel.CustomerViewModels
{
    public class CustomersViewModel
    {

        public List<CustomerListItemViewModel> Customers { get; set; }
        public class CustomerListItemViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
