using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[PrimaryKey("StoreId", "BookId")]
[Table("inventory")]
public partial class Inventory
{
    [Key]
    [Column("store_id")]
    public int StoreId { get; set; }

    [Key]
    [Column("book_id")]
    public int BookId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Inventories")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Inventories")]
    public virtual Store Store { get; set; } = null!;
}
