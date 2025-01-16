using Makaan.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.Core.Entities;
public class Agent : BaseEntity
{
    public string Fullname { get; set; }    
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; } 
    public string? ProfileImageUrl { get; set; } 
}
