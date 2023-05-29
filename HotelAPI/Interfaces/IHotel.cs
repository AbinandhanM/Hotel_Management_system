namespace HotelAPI.Interfaces
{
    public interface IHotel<T,K>
    {
        T Add(T item);
        T Update(T item);
        T Delete(K item);
        T GetValue(K item);
        ICollection<T> GetAll();
    }
}
