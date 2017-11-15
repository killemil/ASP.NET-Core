namespace CameraBazaar.Services.Implementations
{
    using System.Collections.Generic;
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Web.Data;
    using CameraBazaar.Data.Models;
    using System.Linq;
    using CameraBazaar.Services.Models.Cameras;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraListingModel> AllListing()
            => this.db.Cameras
                .Select(c => new CameraListingModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    ImageUrl = c.ImageUrl
                })
                .ToList();

        public void Create(
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
            IEnumerable<LightMetering> lightMeterings, 
            string description, 
            string imageUrl, 
            string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }
    }
}
