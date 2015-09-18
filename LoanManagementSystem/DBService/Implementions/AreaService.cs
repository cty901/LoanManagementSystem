using LoanManagementSystem.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBService.Implementions
{
    class AreaService:GenericService<area>
    {
        public static int InsertArea(area _area)
        {
            try
            {
                db.areas.Add(_area);
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

        public static int UpdateSMS(area _area)
        {
            try
            {
                var area = db.areas.Single(ar => ar.ID == _area.ID);
                db.Entry(area).CurrentValues.SetValues(_area);

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

        public static int DeleteSMS(area _area)
        {
            try
            {
                var area = db.areas.Single(ar => ar.ID == _area.ID);
                db.areas.Remove(area);
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

        internal static System.Collections.IEnumerable getAreaCodes()
        {
            var areas = db.areas.Where(e => e.STATUS == true).ToList();
            return areas;
        }

        internal static area GetAreaByID(string p)
        {
            area area = db.areas.Where(e => e.ID.Trim() == p.Trim()).SingleOrDefault();
            return area;
        }
    }
}
