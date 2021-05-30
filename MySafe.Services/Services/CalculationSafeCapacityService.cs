using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Interfaces.Services;

namespace MySafe.Services.Services
{
    public class CalculationSafeCapacityService: ICalculationSafeCapacityService
    {
        public double GetUsedCapacityInPercents(double maxCapacity, double usedCapacity)
        {
            var percentCapacity = maxCapacity / 100.0d;
            var usedCapacityInPercents = usedCapacity / percentCapacity;

            return usedCapacityInPercents;
        }
    }
}
