using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCoursework.Models
{
    public class SalesDetail
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Sales")]
        public int SalesID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual Sales Sales { get; set; }
        public virtual Product Product { get; set; }
    }
}
