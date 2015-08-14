using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoanManagementSystem.DBModel;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace LoanManagementSystem.DBService.Implementions
{
    class CustomerService:GenericService<customer>
    {
        public static int InsertCustomer(customer _customer)
        {
            try
            {
                db.customers.Add(_customer);
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public static int UpdateEmployee(customer _customer)
        {
            try
            {
                var cus = db.customers.Single(c => c.ID == _customer.ID);
                db.Entry(cus).CurrentValues.SetValues(_customer);

                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

    }
}
