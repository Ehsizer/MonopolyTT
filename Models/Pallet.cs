using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_test_task.Models
{
    public class Pallet //Public Только для Тестов ориг. Internal
    {
        public int Id { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Depth { get; set; }

        public List<Box> Boxes { get; set; } = new();
    }
}
