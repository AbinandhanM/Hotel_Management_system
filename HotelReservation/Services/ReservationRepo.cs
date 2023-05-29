using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelReservation.Services
{
    public class ReservationRepo : IReservation<Reservation, Id>
    {
        private readonly ReservationContext _context;
        public ReservationRepo(ReservationContext context)
        {
            _context=context;
        }
        public Reservation Add(Reservation item)
        {
            _context.Reservations.Add(item);
            _context.SaveChanges();
            return item;           
           
        }

        public Reservation Delete(Id item)
        {
            var reservations = _context.Reservations.ToList();
            var reservation = reservations.FirstOrDefault(r => r.RoomId == item.ID);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
                return reservation;
            }
        return null;           
          
        }

        public ICollection<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetValue(Id item)
        {           
            var reservations = _context.Reservations.ToList();
            var reservation = reservations.FirstOrDefault(r => r.RoomId == item.ID);
            if (reservation != null)
                return reservation;         
          
            return null;
        }

        public Reservation Update(Reservation item)
        {           
            var reservations = _context.Reservations.ToList();
            var reservation = reservations.FirstOrDefault(r => r.RoomId == item.RoomId);
            if (reservation != null)
            {
                reservation.HotelId = item.HotelId != 0 ? item.HotelId : reservation.HotelId;
                reservation.UserId = item.UserId != 0 ? item.UserId : reservation.UserId;
                reservation.RoomNumber = item.RoomNumber != 0 ? item.RoomNumber : reservation.RoomNumber;
                reservation.CheckInDate = DateTime.Compare(item.CheckInDate,DateTime.Now)!=0 ? item.CheckInDate.Date : reservation.CheckInDate.Date;
                reservation.CheckOutDate = DateTime.Compare(item.CheckOutDate,DateTime.Now)!=0 ? item.CheckOutDate.Date : reservation.CheckOutDate.Date;
                _context.SaveChanges();
                return reservation;
            }          
       
            return null;
        }
    }
}
