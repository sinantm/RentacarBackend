using System;
using System.ComponentModel.DataAnnotations;

namespace Rentacar.Models
{
    public class Order
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [Required]
        public Guid CarId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string SurName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string Telephone { get; set; }
        
        [Required]
        [StringLength(11)]
        public string IdentificationNumber { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        [Required]
        public DateTime ReceiptDate { get; set; }
        
        [Required]
        public DateTime IssuanceDate { get; set; }
        
        [Required]
        public string DeliveryAddress { get; set; }
        
        public string Note { get; set; }
        
        public virtual Car Car { get; set; }
    }
}