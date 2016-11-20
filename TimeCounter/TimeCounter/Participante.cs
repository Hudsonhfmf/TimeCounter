using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCounter
{
    public class Participante
    {
        public string Idade { get; set; }
        public string Sexo { get; set; }
        public string Profissao { get; set; }
        public string Formacao { get; set; }
        public string TempoGasto { get; set; }
        public int QtdErros { get; set; }
        public string Tarefa { get; set; }
        public IList<SaveTime> listaTempo { get; set; }
    }
}
