using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Smis.Models;

public partial class Useri : IdentityUser
{
    // public int UserId { get; set; }

    public string? Emri { get; set; }

    public string? Mbiemri { get; set; }


    // public string? Roli { get; set; }

    public DateOnly? Datelindja { get; set; }

    public string? NrLeternjoftimit { get; set; }

    public string? VendLindja { get; set; }

    public string? Gjinia { get; set; }

    public string? Shteti { get; set; }

    public string? Vendbanimi { get; set; }

    public string? Adresa { get; set; }

    public string? Zipkodi { get; set; }

    public string? Telefoni { get; set; }

    public string? Nenshtetesia { get; set; }

    public string? Foto { get; set; }

    // public int RoleId { get; set; }

    public virtual ICollection<Admin> Admin { get; set; } = new List<Admin>();
    public virtual Studenti Studenti { get; set; }
    public virtual StafiAkademik StafiAkademik { get; set; }





}
