using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Core.Interfaces.Services
{
    public interface ICalculationSafeCapacityService
    {
        double GetUsedCapacityInPercents(double maxCapacity, double usedCapacity);
    }
}
