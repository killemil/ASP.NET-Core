namespace CarDealer.Web.Models.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierFormModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Is Importer")]
        public bool IsImporter { get; set; }
    }
}
