using LoanManagementSystem.DBModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBService.Implementions
{
    class GenericService<T>
    {
        protected static loandbEntities  db = new loandbEntities();      

        public static void Insert(T entity)
        {
            // entity.
        }

        public static void Update(T entity)
        {
            // entity.
        }

        public static void Delete(T entity)
        {
            // entity.
        }
    }
}
