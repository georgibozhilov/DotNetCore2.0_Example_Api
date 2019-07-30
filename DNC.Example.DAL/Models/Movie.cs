namespace DNC.Example.DAL.Models
{
    using System;

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public float ScoreRating { get; set; }
        public byte[] Image { get; set; }
    }
}
