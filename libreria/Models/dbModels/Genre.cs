using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libreria.Models.dbModels;

[Table("genres")]
public partial class Genre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("genre")]
    [StringLength(50)]
    public string Genre1 { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Genres")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
