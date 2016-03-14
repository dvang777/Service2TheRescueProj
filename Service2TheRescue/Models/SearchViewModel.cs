using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service2TheRescue.Models
{
    public class SearchViewModel
    {
        [DisplayName("search query *")]
        [Required]
        public string Query { get; set; }
    }
}