using HotelAPI.Models;
using HotelAPI.Models.DTO;
using HotelAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _hotelService;

        public HotelController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }             

        [HttpPost]
        public ActionResult<Room> AddRoom(Room room)
        {

            if (room.R_id != 0)
                throw new ArgumentException(); 
                var myroom = _hotelService.AddRoom(room);
                if (myroom != null)
                    return Created("Room created Successfully", myroom);
                return BadRequest(new Error(3, "Unable to add Room"));           
        }


        [HttpDelete]
        public ActionResult<Room> DeleteHotel(IdDTO idDTO)
        {
           
                if (idDTO.ID <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.DeleteHotel(idDTO);
                if (myHotel != null)
                    return Created("Hotel deleted Successfully", myHotel);
                return BadRequest(new Error(5, $"There is no hotel present for the id {idDTO.ID}"));           
         
        }

        [HttpDelete]
        [Authorize(Roles = "staff")]

        public ActionResult<Room> DeleteRoom(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(4, "Enter Valid Room Number"));
            var myroom = _hotelService.DeleteRoom(idDTO);
            if (myroom != null)
                return Created("Room delete Successfully", myroom);
            return BadRequest(new Error(7, $"There is no Room present for the Number {idDTO.ID}"));
        }

        [HttpPut]
        [Authorize(Roles = "staff")]
        public ActionResult<Hotel> UpdateHotel(Hotel hotel)
        {
          
                if (hotel.HotelId <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.UpdateHotel(hotel);
                if (myHotel != null)
                    return Created("Hotel Updated Successfully", myHotel);
                return BadRequest(new Error(8, $"There is no hotel present for the id {hotel.HotelId}"));         
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Hotel> ViewHotel(IdDTO idDTO)
        {
            
                if (idDTO.ID <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.GetHotel(idDTO);
                if (myHotel != null)
                    return Created("Hotel", myHotel);
                return BadRequest(new Error(9, $"There is no hotel present for the id {idDTO.ID}"));            
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Hotel> ViewAllHotels()
        {
           
                var myHotels = _hotelService.GetAllHotels();
                if (myHotels.Count > 0)
                    return Ok(myHotels);
                return BadRequest(new Error(10, "No Hotels are Existing"));            
            
        }

    }
}
