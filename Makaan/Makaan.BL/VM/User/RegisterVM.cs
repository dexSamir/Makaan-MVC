using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.BL.VM.User;
public class RegisterVM
{
    [MaxLength(64), Required]
    public string Fullname { get; set; }

    [MaxLength(64), Required]
    public string Username { get; set; }

    [MaxLength(64), Required, EmailAddress]
    public string Email { get; set; }

    [MaxLength(32), Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [MaxLength(32), Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string RePassword { get; set; }
}
