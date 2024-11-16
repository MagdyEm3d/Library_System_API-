﻿using Library_System_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Library_System_API.DTOS
{
    public class AddGenreDTO
    {
        [Required(ErrorMessage = "Please Enter GenreName")]
        public string Name { get; set; }

        public List<int> BooksIds { get; set; }= new List<int>();
    }
}
