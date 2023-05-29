using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;

namespace HotelReservation.Services
{
    public class ReservationService
    {
        private readonly IReservation<Reservation, Id> _reservationRepo;
        public ReservationService(IReservation<Reservation,Id> productRepo)
        {
            _reservationRepo = productRepo;
        }
        public Reservation Add(Reservation reservation)
        {
            var reservations= _reservationRepo.GetAll();
            var myReservations = reservations.Where(R=>R.HotelId ==reservation.HotelId && R.RoomNumber==reservation.RoomNumber).ToList();
            if (myReservations!=null)
            {
                var newReservation = myReservations.SingleOrDefault(R=>R.CheckInDate.Date <=reservation.CheckInDate.Date && R.CheckOutDate.Date >=reservation.CheckInDate.Date);
                    return null;
            }
            reservation.CheckInDate = reservation.CheckInDate.Date;
            reservation.CheckOutDate = reservation.CheckOutDate.Date;
            var reservationObj= _reservationRepo.Add(reservation);
            if(reservationObj!=null)
                return reservationObj;
            return null;
        }

        public Reservation Delete(Id idDTO)
        {
            var reservations= _reservationRepo.GetAll();
            var myReservation = reservations.SingleOrDefault(R=>R.RoomId ==idDTO.ID);
            if (myReservation != null)
            {
                var reservation = _reservationRepo.Delete(idDTO);
                if (reservation != null)
                    return reservation;
            }
            return null;
        }

        public Reservation GetReservation(Id idDTO)
        {
            var reservations = _reservationRepo.GetAll();
            var myReservation = reservations.SingleOrDefault(R => R.RoomId == idDTO.ID);
            if (myReservation != null)
            {
                var reservation = _reservationRepo.GetValue(idDTO);
                if (reservation != null)
                    return reservation;
            }
            return null;
        }

        public Reservation Update(Reservation reservation)
        {
            var reservations = _reservationRepo.GetAll();
            var myReservation = reservations.SingleOrDefault(R => R.RoomId == reservation.RoomId);
            if (myReservation != null)
            {
                var newReservation = _reservationRepo.Update(reservation);
                if (newReservation != null)
                    return newReservation;
            }
            return null;
        }

        public List<Reservation> GetAll()
        {
            var reservartions= _reservationRepo.GetAll();
            if (reservartions != null)
                return reservartions.ToList();
            return null;
        }

        public int BookedRoomsCount(Id idDTO)
        {
            var reservations = _reservationRepo.GetAll().ToList();
            var bookedRooms = reservations.Where(R => R.HotelId == idDTO.ID).ToList();
            int count = bookedRooms.Count();
            if (count > 0)
                return count;
            return 0;
        }

        public bool CheckAvailability(HotelAvailability availabilityChecking)
        {
            var reservations= _reservationRepo.GetAll().ToList();
            
            var reservation = reservations.SingleOrDefault(R=>R.HotelId == availabilityChecking.HotelId
                                                   && R.RoomNumber==availabilityChecking.RoomNumber
                                                   &&R.CheckInDate.Date<=availabilityChecking.CheckInDate.Date
                                                   && R.CheckOutDate.Date>=availabilityChecking.CheckInDate.Date);
            if(reservations!=null)
                return false;
            return true;
        }
    }
}
