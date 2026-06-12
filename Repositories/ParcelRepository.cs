using Microsoft.EntityFrameworkCore;
using ParcelTrackingSystem.Data;
using ParcelTrackingSystem.Models;

namespace ParcelTrackingSystem.Repositories
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly AppDbContext _context;

        public ParcelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parcel>> GetAllParcelsAsync()
        {
            return await _context.Parcels.ToListAsync();
        }

        public async Task<Parcel?> GetParcelByIdAsync(int id)
        {
            return await _context.Parcels.FindAsync(id);
        }

        public async Task<Parcel?> GetParcelByTrackingNumberAsync(string trackingNumber)
        {
            trackingNumber = trackingNumber.Trim().ToUpper();

            return await _context.Parcels
                .FirstOrDefaultAsync(p => p.TrackingNumber.ToUpper() == trackingNumber);
        }

        public async Task AddParcelAsync(Parcel parcel)
        {
            parcel.TrackingNumber = $"TRK-{Guid.NewGuid().ToString()[..8].ToUpper()}";
            parcel.status = "Pending";

            await _context.Parcels.AddAsync(parcel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParcelAsync(Parcel parcel)
        {
            var existingParcel = await _context.Parcels.FindAsync(parcel.Id);

            if (existingParcel == null)
                throw new KeyNotFoundException($"Parcel with id {parcel.Id} not found.");

            existingParcel.SenderName = parcel.SenderName;
            existingParcel.ReceiverName = parcel.ReceiverName;
            existingParcel.PickupAddress = parcel.PickupAddress;
            existingParcel.DeliveryAddress = parcel.DeliveryAddress;
            existingParcel.Weight = parcel.Weight;
            existingParcel.status = parcel.status;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteParcelAsync(int id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
                throw new KeyNotFoundException($"Parcel with id {id} not found.");

            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();
        }
    }
}