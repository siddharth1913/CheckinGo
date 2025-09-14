using CheckinGo.Domain.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckinGo.Domain.Entities
{
    public class VillaNumber
    {
        //to make explicitly member as Primary key - we use [Key] Data Annotation


        // Be Dafault = Identity, but to disable identity use given below code 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Villa Number")]
        public int Villa_Number {  get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }

        // Navogation property to eastablish FK relation with parent - Villa table
        // To ignore this property in ModelState Validation we can use =>
        [ValidateNever]
        public Villa Villa { get; set; }

        public string? SpecialDetails { get; set; }


    }
}
