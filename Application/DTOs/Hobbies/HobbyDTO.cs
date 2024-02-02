using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Hobbies;

public class HobbyDTO : BaseDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}
