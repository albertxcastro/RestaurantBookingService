using RestaurantBookingService.DataAccess.Context;

namespace RestaurantBookingService.Managers
{
    public class FactoryManager
    {
        public T CreateManager<T>(Context context) where T : BaseManager, new()
        {
            var manager = new T();
            manager.SetContext(context);
            return manager;
        }
    }
}
