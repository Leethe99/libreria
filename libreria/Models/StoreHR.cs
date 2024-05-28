using System;
namespace libreria.Models
{
	public class StoreHR
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Street { get; set; }

        public int? City { get; set; }

        public int StateId { get; set; }

        public int Country { get; set; }

        public string Zipcode { get; set; } = null!;
    }
}

