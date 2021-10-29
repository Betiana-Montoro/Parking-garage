using Parking_garage.Model;
using Parking_garage.MsSQL.GarageRepository;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Parking_garage.MsSQL.GarageRepository
{
    public class ReservationRepository : IReservationRepository
    {
        readonly IQueueService _queueService;

        public ReservationRepository(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task AddReservation(Reservation reservation)
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("AddReservation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
                command.Parameters.AddWithValue("@CheckIn", reservation.CheckIn);
                command.Parameters.AddWithValue("@CheckOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@CarPlate", reservation.CarPlate);
                command.Parameters.AddWithValue("@CarType", reservation.CarType);
                command.Parameters.AddWithValue("@CarColor", reservation.CarColor);
                command.Parameters.AddWithValue("@Cost", reservation.Cost);
                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task UpdateReservation(Reservation reservation)
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("UpdateReservation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
                command.Parameters.AddWithValue("@CheckOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@Cost", reservation.Cost);
                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Reservation> GetReservationByID(Guid Id) 
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\Localhost;Initial Catalog=parkingDB;Persist Security Info=False;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand("GetReservation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ReservationID", Id);
                await conn.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Reservation reservation = new Reservation();
                while (reader.Read())
                {
                    reservation.ReservationId = (Guid)reader["ReservationID"];
                    reservation.CheckIn = (DateTime)reader["CheckIn"];
                    reservation.CheckOut = (DateTime)reader["CheckOut"];
                    reservation.CarPlate = reader["CarPlate"].ToString();
                    reservation.CarType = reader["CarType"].ToString();
                    reservation.CarColor = reader["CarColor"].ToString();
                    reservation.Cost = (decimal)reader["Cost"];
                }
                return reservation;
            }
        }
    }
}
