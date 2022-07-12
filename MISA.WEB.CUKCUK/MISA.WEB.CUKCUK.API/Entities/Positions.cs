using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB.CUKCUK.API.Entities
{
    public class Positions
    {

        [Required(ErrorMessage = "e004")]
        public Guid PositionID { get; set; }

        [Required(ErrorMessage = "e004")]
        public string PositionCode { get; set; }

        [Required(ErrorMessage = "e004")]
        public string? PositionName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

    }
}
