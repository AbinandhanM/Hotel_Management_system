
using HotelAPI.Models.DTO;
using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly HotelService _hotelService;

        public FilterController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> HotelByCity(HotelFilterDTO hotelFilterDTO)
        {
            if (hotelFilterDTO.city == null)
                return BadRequest(new Error(12, "City should not be Empty"));
            var rooms = _hotelService.Hotel_By_City(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(13, $"There is No rooms available for the City {hotelFilterDTO.city}"));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> HotelByCountry(HotelFilterDTO hotelFilterDTO)
        {
            if (hotelFilterDTO.country == null)
                return BadRequest(new Error(14, "Country should not be Empty"));
            var rooms = _hotelService.Hotel_By_Country(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(15, $"There is No rooms available for the Country {hotelFilterDTO.country}"));
        }


      
        [HttpPost]
        public ActionResult<List<Room>> RoomByPrice(PriceDTO priceDTO)
        {
           
                if (priceDTO.H_Id <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel Id"));
                if (priceDTO.MinValue <= 0 && priceDTO.MazValue <= 0)
                    return BadRequest(new Error(20, "Enter a valid Prices"));
                var rooms = _hotelService.Room_By_Price(priceDTO);
                if (rooms != null)
                    return Ok(rooms);
                return NotFound(new Error(19, $"There is no room for Hotel id {priceDTO.H_Id}"));
           
         
        }

        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> RoomByCapacity(CapacityAndTypeDTO capacityAndTypeDTO)
        {
            if (capacityAndTypeDTO.H_id <= 0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            if (capacityAndTypeDTO.Capacity <= 0)
                return BadRequest(new Error(21, "Enter valid Capacity to filter"));
            var rooms = _hotelService.Room_By_Capacity(capacityAndTypeDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(11, $"There is No rooms available for the hotel id {capacityAndTypeDTO.H_id}"));
        }

      

        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> RoomsCount(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            int roomsCount = _hotelService.Rooms_Count(idDTO);
            if (roomsCount > 0)
                return Ok(roomsCount);
            return NotFound(new Error(11, $"There is No rooms available for the hotel id {idDTO.ID}"));
        }

    
        [HttpGet]    
        public ActionResult<List<HotelAndRooms>> TotalHotelsRooms()
        {
            var hotelAndRooms = _hotelService.Total_Hotels_Rooms();
            if (hotelAndRooms != null)
                return Ok(hotelAndRooms);
            return BadRequest(new Error(23, "Unable to fetch details"));
        }
    }
}

