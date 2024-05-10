using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("cities")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("city")]
    [StringLength(100)]
    public string City1 { get; set; } = null!;

    [InverseProperty("CityNavigation")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
