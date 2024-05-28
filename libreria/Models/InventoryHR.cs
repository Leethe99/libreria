using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria.Models
{
	public class InventoryHR
	{
        public int StoreId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}

