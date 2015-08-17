﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoanManagementSystem.DBModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using LoanManagementSystem.Util;

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

        public static int UpdateCustomer(customer _customer)
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

        internal static PagingCollection<customer> GetPaginatedQuickSearchedCustomerListByPage(string _searchText, int page)
        {
            PagingCollection<customer> pager = new PagingCollection<customer>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var customers = db.customers.Where
                (e =>
                    (   e.FIRST_NAME == _searchText ||
                        e.LAST_NAME == _searchText ||
                        e.CUSTOMER_ID == _searchText||
                        e.PHONE_HP1==_searchText||
                        e.PHONE_HP2==_searchText||
                        e.PHONE_RECIDENCE==_searchText                        
                    )).ToList();

            pager.Collection = customers.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = customers.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static PagingCollection<customer> GetPaginatedCustomerListByPage(int page)
        {
            PagingCollection<customer> pager = new PagingCollection<customer>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var customers = db.customers.Where(e => e.ISACTIVE == true).ToList();

            pager.Collection = customers.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = customers.Count();
            pager.CurrentPage = page;

            return pager;
        }

    }
}
