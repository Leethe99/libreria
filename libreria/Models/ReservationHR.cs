using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria.Models
{
	public class ReservationHR
	{
        public int Id { get; set; }

        public int UserId { get; set; }

        public int StoreId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

