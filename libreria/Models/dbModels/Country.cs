using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("country")]
    [StringLength(100)]
    public string Country1 { get; set; } = null!;

    [InverseProperty("CountryNavigation")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
