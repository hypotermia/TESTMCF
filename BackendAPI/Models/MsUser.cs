﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackendAPI.Models;

public partial class MsUser
{
    public Guid Id { get; set; }

    public string UserNames { get; set; }

    public string Passwords { get; set; }

    public bool Isactive { get; set; }
}