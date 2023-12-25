using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoProject.BusinessLayer.Abstract;
using TodoProject.DataAccessLayer.Abstract;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.BusinessLayer.Concrete
{
    public class TodoManager : ITodoService
    {
        private readonly ITodoDal _TodoDal;

        public TodoManager(ITodoDal todoDal)
        {
            _TodoDal = todoDal;
        }

        public void TInsert(Todo entity)
        {
            _TodoDal.Insert(entity);
        }

        public void TUpdate(Todo entity)
        {
            _TodoDal.Update(entity);
        }

        public void TDelete(Todo entity)
        {
            _TodoDal.Delete(entity);
        }

        public List<Todo> TgetList()
        {
            return _TodoDal.getList();
        }

        public Todo TGetById(int id)
        {
            return _TodoDal.GetById(id);
        }
    }
}
