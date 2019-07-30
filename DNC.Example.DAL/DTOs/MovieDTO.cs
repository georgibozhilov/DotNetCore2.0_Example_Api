using System;
using System.Collections.Generic;
using System.Text;

namespace DNC.Example.DAL.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public float ScoreRating { get; set; }
        public string ImageAsBase64 { get; set; }
    }
}
