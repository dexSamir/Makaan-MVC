using Makaan.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.Core.Entities;
public class Department : BaseEntity
{
    public string Name { get; set; }    
    public ICollection<Agent> Agents { get; set; } = new HashSet<Agent>();
}
