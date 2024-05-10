using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace libreria.Models.dbModels
{
	public class ApplicationUser : IdentityUser<int>
	{
        [InverseProperty("User")]
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}

