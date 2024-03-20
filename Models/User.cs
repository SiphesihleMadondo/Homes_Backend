using System;
using System.Collections.Generic;

namespace Homes;

public partial class User
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Province { get; set; }

    public string? Code { get; set; }

    public string? City { get; set; }
}
