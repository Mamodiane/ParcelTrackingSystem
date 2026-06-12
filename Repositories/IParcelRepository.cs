using ParcelTrackingSystem.Models;

namespace ParcelTrackingSystem.Repositories
{
    public interface IParcelRepository
    {
        Task<IEnumerable<Parcel>> GetAllParcelsAsync();
        Task<Parcel?> GetParcelByIdAsync(int id);
        Task<Parcel?> GetParcelByTrackingNumberAsync(string trackingNumber);
        Task AddParcelAsync(Parcel parcel);
        Task UpdateParcelAsync(Parcel parcel);
        Task DeleteParcelAsync(int id);
    }
}