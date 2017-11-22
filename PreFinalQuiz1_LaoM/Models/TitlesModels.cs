using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinalQuiz1_LaoM.Models
{
    public class TitlesModels
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Publisher")]
        [Required]
        public int pubID { get; set; }
        public List<SelectListItem> Publishers { get; set; }

        public string Publisher { get; set; }

        [Display(Name = "Author")]
        [Required]
        public int authorID { get; set; }
        public List<SelectListItem> Authors { get; set; }
        public string Author { get; set; }
        public string AuthorFN { get; set; }

        [Display(Name = "Title Name")]
        [Required]
        [MaxLength(200)]
        public string TitleName { get; set; }

        [Display(Name = "Price")]
        [Required]
        [DataType(DataType.Currency)]
        [Range(1.00, 10000.00, ErrorMessage = "Invalid range.")]
        public double Price { get; set; }

        [Display(Name = "Publication Date")]
        [Required]
        public DateTime Date { get; set; }
        [DisplayFormat(NullDisplayText = "")]

        [Display(Name = "Notes")]
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Notes { get; set; }
    }
}