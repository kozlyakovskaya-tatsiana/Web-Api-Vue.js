﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_server.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(40, ErrorMessage = "Max length of book name = 40")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(0,3000)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(80, ErrorMessage ="Max length of author's name = 80")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(700, ErrorMessage = "Max length of book description = 700")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageSrc { get; set; }



        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Book()
        {
            Users = new List<ApplicationUser>();
        }
    }
}