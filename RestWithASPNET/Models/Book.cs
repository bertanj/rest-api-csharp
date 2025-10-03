using System;
using System.Collections.Generic;

namespace RestWithASPNET.Models;

public partial class Book
{
    public long Id { get; set; }

    public string Author { get; set; } = null!;

    public DateTime LaunchDate { get; set; }

    public decimal Price { get; set; }

    public string Title { get; set; } = null!;
}
