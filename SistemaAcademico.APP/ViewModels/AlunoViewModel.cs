using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademico.APP.ViewModels
{
    public class AlunoViewModel
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public string WhatsApp { get; set; }
        public string EmailPrimario { get; set; }
        public string EmailSecundario { get; set; }

        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
    }
}
