using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.BL.VM.Department;
public class DepartmentUpdateVM
{
    [Required(ErrorMessage = "Name is required!"), MaxLength(128, ErrorMessage = "Name length must be 128 charachers or less")]

    public string Name { get; set; }
}
