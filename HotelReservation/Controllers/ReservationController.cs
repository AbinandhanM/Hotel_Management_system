using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;
using HotelReservation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
       
        public ReservationController(ReservationService reservationService)
        {
            _reservationService= reservationService;
        }

        [HttpPost]
        public ActionResult<string> CheckAvailability(HotelAvailability availabilityChecking)
        {
            if (availabilityChecking.HotelId <= 0 && availabilityChecking.RoomNumber <= 0)
                return BadRequest(new Error(10, "Check Hotel ID and RoomNumber"));
            if (availabilityChecking.CheckInDate == default(DateTime))
                return BadRequest(new Error(11, "CheckIn date should not be Empty"));
            bool flag = _reservationService.CheckAvailability(availabilityChecking);
            if (!flag)
                return Ok($"Room No {availabilityChecking.RoomNumber} was already Booked");
            return Ok($"Room No {availabilityChecking.RoomNumber} is Available for Booking");
        }

        [HttpPost]
        public ActionResult<Reservation> RoomBooking(Reservation reservation)
        {
            if (reservation.RoomId != 0)
                throw new ArgumentNullException();
            if (reservation.CheckInDate.Date >= reservation.CheckOutDate.Date)
                return BadRequest(new Error(1,"ChecK Dates"));
            var myReservation = _reservationService.Add(reservation);
            if (myReservation != null)
                return Created("Room Booked Successfully", myReservation);
            return BadRequest(new Error(2, $"The Room {reservation.RoomNumber} is Already Booked"));           
           
        }
        [HttpDelete]
        public ActionResult<Reservation> CancelBooking(Id idDTO)
        {
            if(idDTO.ID<=0)
                return BadRequest(new Error(5,"Check Reservation ID"));
            var reservation = _reservationService.Delete(idDTO);
            if (reservation != null)
                return Ok(reservation);
            return BadRequest(new Error(6, $"There is No Bookings for the Resevation id: {idDTO.ID}"));
        }

        [HttpPost]
        public ActionResult<Reservation> GetBooking(Id idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(5, "Check Reservation ID"));
            var reservation = _reservationService.GetReservation(idDTO);
            if (reservation != null)
                return Ok(reservation);
            return NotFound(new Error(6, $"There is No Bookings for the Reservation id: {idDTO.ID}"));
        }

        [HttpGet]
        public ActionResult<Reservation> ViewBookings()
        {
            var reservations = _reservationService.GetAll();
            if (reservations != null)
                return Ok(reservations);
            return NotFound(new Error(7, "No Bookings"));
        }

        [HttpPost]
        public ActionResult<Reservation> UpdateBooking(Reservation reservation)
        {
            if (reservation.RoomId <= 0)
                return BadRequest(new Error(5, "Check Reservation ID"));
            var newReservation = _reservationService.Update(reservation);
            if (newReservation != null)
                return Ok(newReservation);
            return BadRequest(new Error(6, $"There is No Bookings for the Reservation id: {reservation.RoomId}"));
        }
        [HttpPost]
        public ActionResult<int> BookedRoomparticularhotel(Id idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(8, "Check Hotel ID"));
            var count = _reservationService.BookedRoomsCount(idDTO);
            if (count > 0)
                return Ok(count);
            return BadRequest(new Error(9, $"No Rooms Booked for the hotel id: {idDTO.ID}"));
        }
        
    }
}
