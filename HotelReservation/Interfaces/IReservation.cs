namespace HotelReservation.Interfaces
{
    public interface IReservation<T,K>
    {
        T Add(T item);
        T Update(T item);
        T Delete(K item);
        T GetValue(K item);
        ICollection<T> GetAll();
    }
}
