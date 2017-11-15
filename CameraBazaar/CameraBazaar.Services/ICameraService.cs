namespace CameraBazaar.Services
{
    using CameraBazaar.Data.Models.Enums;
    using System.Collections.Generic;

    public interface ICameraService
    {
        void Create(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxISO,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightmeterings,
            string description,
            string imageUrl,
            string userId);
    }
}