using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using NM.Modules.FlexEventsCreate.Models;

namespace NM.Modules.FlexEventsCreate.Components
{
    interface ILocationManager
    {
        List<Location> Get(int moduleId);
    }

    class LocationManager : ServiceLocator<ILocationManager, LocationManager>, ILocationManager
    {
        public List<Location> Get(int moduleId)
        {
            List<Location> locations;

            using (IDataContext ctx = DataContext.Instance())
            {
                var loc = ctx.GetRepository<Location>();
                locations = loc.Get(moduleId).OrderBy(x => x.Name).ToList();
            }
            return locations;
        }

        protected override Func<ILocationManager> GetFactory()
        {
            return () => new LocationManager();
        }
    }
}