using Microsoft.AspNetCore.Mvc;
using ParcelTrackingSystem.Models;
using ParcelTrackingSystem.Repositories;

namespace ParcelTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelRepository _parcelRepository;

        public ParcelsController(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcel>>> GetAllParcels()
        {
            var parcels = await _parcelRepository.GetAllParcelsAsync();
            return Ok(parcels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parcel>> GetParcelById(int id)
        {
            var parcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (parcel == null)
                return NotFound();

            return Ok(parcel);
        }

        [HttpGet("track/{trackingNumber}")]
        public async Task<ActionResult<Parcel>> GetParcelByTrackingNumber(string trackingNumber)
        {
            var parcel = await _parcelRepository.GetParcelByTrackingNumberAsync(trackingNumber);

            if (parcel == null)
                return NotFound();

            return Ok(parcel);
        }

        [HttpPost]
        public async Task<ActionResult<Parcel>> CreateParcel(Parcel parcel)
        {
            await _parcelRepository.AddParcelAsync(parcel);

            return CreatedAtAction(
                nameof(GetParcelById),
                new { id = parcel.Id },
                parcel
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParcel(int id, Parcel parcel)
        {
            if (id != parcel.Id)
                return BadRequest("Parcel ID mismatch.");

            var existingParcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (existingParcel == null)
                return NotFound();

            await _parcelRepository.UpdateParcelAsync(parcel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            var existingParcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (existingParcel == null)
                return NotFound();

            await _parcelRepository.DeleteParcelAsync(id);

            return NoContent();
        }

        [HttpPut("{id}/collect")]
        public async Task<IActionResult> MarkAsCollected(int id)
        {
            var parcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (parcel == null)
                return NotFound();

            if (parcel.status != "Pending")
                return BadRequest("Parcel can only be collected when status is Pending.");

            parcel.status = "Collected";

            await _parcelRepository.UpdateParcelAsync(parcel);

            return NoContent();
        }

        [HttpPut("{id}/in-transit")]
        public async Task<IActionResult> MarkAsInTransit(int id)
        {
            var parcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (parcel == null)
                return NotFound();

            if (parcel.status != "Collected")
                return BadRequest("Parcel can only move to In Transit after it has been Collected.");

            parcel.status = "In Transit";

            await _parcelRepository.UpdateParcelAsync(parcel);

            return NoContent();
        }

        [HttpPut("{id}/deliver")]
        public async Task<IActionResult> MarkAsDelivered(int id)
        {
            var parcel = await _parcelRepository.GetParcelByIdAsync(id);

            if (parcel == null)
                return NotFound();

            if (parcel.status != "In Transit")
                return BadRequest("Parcel can only be Delivered after it is In Transit.");

            parcel.status = "Delivered";

            await _parcelRepository.UpdateParcelAsync(parcel);

            return NoContent();
        }
    }


}