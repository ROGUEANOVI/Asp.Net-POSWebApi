using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.User.Request
{
    public class TokenRequestDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
