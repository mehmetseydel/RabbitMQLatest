using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreateExcelMessage 
    {

        public int UserId { get; set; }
        public int FileId { get; set; }
        //istersen exceli byte arraye cevir gelsun!
        //büyük data worker service'den yapcez!
    }
}
