using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Domain.Services
{
    public interface ICalculationSafeCapacityService
    {
        double GetUsedCapacityInPercents(double maxCapacity, double usedCapacity);
    }
}
