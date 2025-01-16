using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.BL.VM.Agent;
public class AgentCreateVM
{
    public string Fullname { get; set; }
    public int? DepartmentId { get; set; }
    public IFormFile? Image { get; set; }
}
