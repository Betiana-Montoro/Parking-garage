using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.MsSQL.GarageRepository
{
    public class GarageRepository : IGarageRepository
    {
        public async Task<int> GetAvailableSlots()
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetAvailableSlots", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                int AvailableSlots = 0;
                if (reader.Read()){
                    AvailableSlots = (int)reader["SlotsAvailable"];
                }
                return AvailableSlots;
            }
        }

        public async Task<Unit> TakeSlot()
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                await conn.OpenAsync();
                SqlCommand updateSqlCommand = new SqlCommand("Update garage SET slotsAvailable= (slotsAvailable - 1)", conn);
                await updateSqlCommand.ExecuteNonQueryAsync();
            }
            return Unit.Value;
        }

        public async Task<Unit> ReleaseSlot()
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                await conn.OpenAsync();
                SqlCommand updateSqlCommand = new SqlCommand("Update garage SET slotsAvailable= (slotsAvailable + 1)", conn);
                await updateSqlCommand.ExecuteNonQueryAsync();
            }
            return Unit.Value;
        }

    }
}

