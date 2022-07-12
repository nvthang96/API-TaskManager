using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB.CUKCUK.API.Entities
{
    public class Department
    {
        [Required(ErrorMessage = "e004")]
        public Guid DepartmentID { get; set; }

        [Required(ErrorMessage = "e004")]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "e004")]

        public string DepartmentName { get; set; }
      public DateTime CreatedDate { get; set; }
      public string CreatedBy { get; set; }
      public DateTime ModifiedDate { get; set; }

      public string ModifiedBy { get; set; }
        
    }
}
