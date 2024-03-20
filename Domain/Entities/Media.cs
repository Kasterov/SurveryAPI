using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Media : BaseAuditableEntity
{
    public string Name { get; set; }
    public byte[] Bytes { get; set; }
    public string ContentType { get; set; }
    public string Expression { get;set; }
    public User User { get; set; }
}
