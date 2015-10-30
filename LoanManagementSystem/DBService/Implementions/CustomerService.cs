using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoanManagementSystem.DBModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using LoanManagementSystem.Util;
using System.Windows;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public static int UpdateCustomer(customer _customer)
        {
            try
            {
                var cus = db.customers.Single(c => c.ID == Session.SelectedCustomer.ID);
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

        public static int DeleteCustomer(customer _customer)
        {
            var query = db.customers.Single(c => c.ID == _customer.ID);
            try
            {
                db.customers.Remove(query);
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
            catch (Exception ex)
            {
                db.Entry(query).Reload();
                return 0;
            }
        }

        internal static PagingCollection<customer> GetPaginatedQuickSearchedCustomerListByPage(string _searchText, int page,string areaName)
        {
            PagingCollection<customer> pager = new PagingCollection<customer>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<customer> customers=null;
            if (areaName == "ALL")
            {
                customers = db.customers.Where(e => (e.ISACTIVE == true)).ToList();
            }
            else
            {
                customers = db.customers.Where(e => (e.ISACTIVE == true && e.area.AREA_NAME == areaName)).ToList();
            }

           if (_searchText != "")
            {
                _searchText = _searchText.ToLower();
                    customers = customers.Where
                    (e =>
                        (   
                            e.ISACTIVE == true &&
                            (e.FIRST_NAME.ToLower() + e.LAST_NAME.ToLower()).Contains(_searchText.Replace(" ", "")) ||
                            e.FIRST_NAME.ToLower().Contains(_searchText) ||
                            e.LAST_NAME.ToLower().Contains(_searchText) ||
                            e.PHONE_HP1.ToLower().Contains(_searchText) ||
                            e.ID_NUM.ToLower().Contains(_searchText)
                        )).ToList();
            }
            else
            {
                customers = customers.Where(e => (e.ISACTIVE == true)).ToList();
            }
            customers = customers.OrderByDescending(cus => cus.FK_AREA_ID).ThenBy(cus=>cus.CUSTOMER_ID).ToList();

            pager.Collection = customers.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = customers.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static PagingCollection<customer> GetPaginatedCustomerListByPage(int page,string areaName)
        {
            PagingCollection<customer> pager = new PagingCollection<customer>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);
            List<customer> customers;
            if (areaName == "ALL")
            {
                customers = db.customers.Where(e => (e.ISACTIVE == true)).ToList();
            }
            else
            {
                customers = db.customers.Where(e => (e.ISACTIVE == true && e.area.AREA_NAME == areaName)).ToList();
            }

            customers = customers.OrderByDescending(cus => cus.FK_AREA_ID).ThenBy(cus => cus.CUSTOMER_ID).ToList();

            pager.Collection = customers.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = customers.Count();
            pager.CurrentPage = page;

            return pager;
        }


        internal static List<customer> GetCustomerListByArea(string p)
        {
            List<customer> _cusList = db.customers.Where(cus => cus.FK_AREA_ID == p).ToList();
            return _cusList;
        }

        internal static customer RefreshCustomerByID(customer _customer)
        {
            db.Entry(_customer).Reload();
            _customer.NeedToSave = false;
            return _customer;
        }

        internal static customer GetCustomerByID(string p)
        {
            customer _cus = db.customers.Where(cus => cus.ID == p).SingleOrDefault();
            return _cus;
        }
    }
}
