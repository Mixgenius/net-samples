using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class A
    {
        [Required(ErrorMessage = "Enter")]
        public string hey { get; set; }
    }
}