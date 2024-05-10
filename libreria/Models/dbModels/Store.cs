using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("stores")]
public partial class Store
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("street")]
    [StringLength(100)]
    public string? Street { get; set; }

    [Column("city")]
    public int? City { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("country")]
    public int Country { get; set; }

    [Column("zipcode")]
    [StringLength(20)]
    public string Zipcode { get; set; } = null!;

    [ForeignKey("City")]
    [InverseProperty("Stores")]
    public virtual City? CityNavigation { get; set; }

    [ForeignKey("Country")]
    [InverseProperty("Stores")]
    public virtual Country CountryNavigation { get; set; } = null!;

    [InverseProperty("Store")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [InverseProperty("Store")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [ForeignKey("StateId")]
    [InverseProperty("Stores")]
    public virtual State State { get; set; } = null!;
}
