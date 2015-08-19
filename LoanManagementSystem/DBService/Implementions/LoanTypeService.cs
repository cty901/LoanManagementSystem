using LoanManagementSystem.DBModel;
using LoanManagementSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBService.Implementions
{
    class LoanTypeService:GenericService<loan_type>
    {
        internal static PagingCollection<loan_type> GetPaginatedQuickSearchedLoanTypeListByPage(string _searchText, int page)
        {
            PagingCollection<loan_type> pager = new PagingCollection<loan_type>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<loan_type> loan_types = null;

            if (_searchText != "")
            {
                loan_types = db.loan_type.Where
                    (ln =>
                        (
                            ln.LOAN_TYPE_ID == _searchText
                            //|| e.hrm_contacts.Where(c => c.HOME == searche.hrm_contacts.Single().HOME)
                        )).ToList();
            }
            else
            {
                loan_types = db.loan_type.ToList();
            }
            pager.Collection = loan_types.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = loan_types.Count();
            pager.CurrentPage = page;

            return pager;
        }
    }
}
