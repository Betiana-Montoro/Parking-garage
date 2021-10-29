using MediatR;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.MsSQL.GarageRepository
{
    public interface IGarageRepository
    {
        Task<int> GetAvailableSlots();
        Task<Unit> TakeSlot();
        Task<Unit> ReleaseSlot();
    }
}
