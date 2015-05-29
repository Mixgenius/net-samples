using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class Wizard
    {
        [Required(ErrorMessage = "Text1 is required") ]
        [StringLength(5, ErrorMessage="Max string length of 5")]
        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }
    
    
    }

    

}