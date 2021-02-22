using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RestaurantBookingService.Helpers
{
    public static class ObjectConverter
    {
        public static V ToVo<P, V>(P obj) where V : class, new() where P : class
        {
            var VOresult = new V();
            var POproperties = obj.GetType().GetProperties();
            var VOproperties = VOresult.GetType().GetProperties();

            foreach (var POproperty in POproperties)
            {
                foreach (var VOproperty in VOproperties)
                {
                    if (POproperty.Name == VOproperty.Name && POproperty.PropertyType == VOproperty.PropertyType)
                    {
                        VOproperty.SetValue(VOresult, POproperty.GetValue(obj));
                        break;
                    }
                }
            }

            return VOresult;
        }
    }
}
