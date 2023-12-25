using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.DataAccessLayer.Abstract
{
    public interface ITodoDal : IGenericDal<Todo>
    {
    }
}
