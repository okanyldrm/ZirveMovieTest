using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirve.Entity.Concrete
{
    public class Note
    {

        public int Id { get; set; }
        public string NoteText { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }


    }
}
