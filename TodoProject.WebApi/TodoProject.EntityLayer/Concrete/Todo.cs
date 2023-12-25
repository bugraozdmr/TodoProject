using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoProject.EntityLayer.Concrete
{
    public class Todo
    {
        // null gelebilecek bir değer ile çalışıyosan string? gibi eklicen

        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

    }
}
