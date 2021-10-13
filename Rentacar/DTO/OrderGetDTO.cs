using System;

namespace Rentacar.DTO
{
    public class OrderGetDTO
    {
        public Guid OrderId { get; set; }
        
        public Guid CarId { get; set; }
        
        public string Name { get; set; }
        
        public string SurName { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Telephone { get; set; }
        
        public string IdentificationNumber { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public DateTime ReceiptDate { get; set; }
        
        public DateTime IssuanceDate { get; set; }
        
        public string DeliveryAddress { get; set; }
        
        public string Note { get; set; }
    }
}