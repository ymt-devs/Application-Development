using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCoursework.Models
{
    public class Sales
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        [Required]
        public DateTime SalesDate { get; set; }
        [Required]
        public int BillNo { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
