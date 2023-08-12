using VehicleManager.Models;
using VehicleManagerApi.Data;

namespace VehicleManagerApi.Controllers
{
    public class CarsController : BaseController<Car>
    {
        public CarsController(IRepository<Car> repo) : base(repo) { }
    }

    public class CustomersController : BaseController<Customer>
    {
        public CustomersController(IRepository<Customer> repo) : base(repo) { }
    }

    public class RentalsController : BaseController<Rental>
    {
        public RentalsController(IRepository<Rental> repo) : base(repo) { }
    }

    public class VehicleCategoriesController : BaseController<VehicleCategory>
    {
        public VehicleCategoriesController(IRepository<VehicleCategory> repo) : base(repo) { }
    }
}
