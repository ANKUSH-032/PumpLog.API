using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Core.PumpLog
{
    public class PumpLogDto
    {
    }
    public class FuelFillingInsertDto
    {
        [Required]
        public string DispenserNo { get; set; } = default!;
        [Required]
        public decimal QuantityFilled { get; set; }           
        [Required]
        public string VehicleNumber { get; set; } = default!; 
        [Required]
        public string? PaymentMode { get; set; }          
        public IFormFile? PaymentProof { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? PaymentProofPath { get; set; }
    }   

    public class FuelFillingListDto
    {
        public Guid FuelFillingId { get; set; }                // uniqueidentifier / primary key
        public string DispenserNo { get; set; } = default!;
        public decimal QuantityFilled { get; set; }
        public string VehicleNumber { get; set; } = default!;
        public string? PaymentMode { get; set; }
        public string? PaymentProofPath { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
