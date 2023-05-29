using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace HotelAPI.Services
{
    public class RoomRepo : IHotel<Room, IdDTO>
    {
        private readonly HotelContext _context;

        public RoomRepo(HotelContext context)
        {
            _context = context;
        }
        public Room Add(Room item)
        {
            _context.Rooms.Add(item);
            _context.SaveChanges();
            return item;         
            
        }

        public Room Delete(IdDTO item)
        {            
            var rooms = _context.Rooms.ToList();
            var myRoom = rooms.SingleOrDefault(r => r.RoomNumber == item.ID);
            if (myRoom != null)
            {
                _context.Rooms.Remove(myRoom);
                _context.SaveChanges();
                return myRoom;
            }
        return null;
         
        }

        public ICollection<Room> GetAll()
        {           
            var rooms = _context.Rooms.ToList();
            if (rooms!=null)
                return rooms;
        return null;

        }

        public Room GetValue(IdDTO item)
        {
            
                var rooms = _context.Rooms.ToList();
                var room = rooms.SingleOrDefault(r => r.RoomNumber == item.ID);
                if (room != null)
                    return room;
           
            return null;
        }

        public Room Update(Room item)
        {
           
                var rooms = _context.Rooms.ToList();
                var room = rooms.SingleOrDefault(r => r.RoomNumber == item.R_id);
                if (room != null)
                {
                    room.RoomNumber = item.R_id!=0?item.R_id:room.RoomNumber;
                    var hotels=_context.Hotels.ToList();
                    if (item.H_id != 0)
                    {
                        var hotel = hotels.SingleOrDefault(h => h.HotelId == item.H_id);
                        if(hotel!=null)
                            room.H_id=item.H_id; 
                    }
                    room.Price= item.Price!=0?item.Price:room.Price;
                    room.Capacity=item.Capacity!=0?item.Capacity:room.Capacity;
                    room.Type=item.Type!=null?item.Type:room.Type;
                    _context.Rooms.Update(item);
                    _context.SaveChanges();
                    return room;
                }
          
            return null;
        }
    }
}
