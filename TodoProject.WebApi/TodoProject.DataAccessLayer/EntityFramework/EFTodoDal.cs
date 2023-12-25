using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoProject.DataAccessLayer.Abstract;
using TodoProject.DataAccessLayer.Concrete;
using TodoProject.DataAccessLayer.Repositories;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.DataAccessLayer.EntityFramework
{
    public class EFTodoDal : GenericRepository<Todo>, ITodoDal
    {
        public EFTodoDal(Context context) : base(context)
        {
        }
    }
}
