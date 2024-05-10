using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("reservation")]
public partial class Reservation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("book_id")]
    public int BookId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Reservations")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Reservations")]
    public virtual ReservationStatus Status { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Reservations")]
    public virtual Store Store { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reservations")]
    public virtual ApplicationUser User { get; set; } = null!;
}
