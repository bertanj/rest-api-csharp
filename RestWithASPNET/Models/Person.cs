using System;
using System.Collections.Generic;

namespace RestWithASPNET.Models;

public partial class Person
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
