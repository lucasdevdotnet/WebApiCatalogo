using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Dtos
{
    public class UsuarioDto
    {
        public string Email { get; set; }
        public string Passwprd { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
