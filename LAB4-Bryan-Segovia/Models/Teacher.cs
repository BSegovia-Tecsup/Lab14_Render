using System;
using System.Collections.Generic;

namespace LAB4_Bryan_Segovia.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? Email { get; set; }
}
