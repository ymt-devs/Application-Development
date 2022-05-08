using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCoursework.Models
{
    public class Purchase
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int BillNo { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
    }
}
