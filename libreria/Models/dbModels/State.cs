using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("states")]
public partial class State
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("state")]
    [StringLength(100)]
    public string State1 { get; set; } = null!;

    [InverseProperty("State")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
