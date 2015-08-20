using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Util
{
    public class PagingCollection<T>
    {
        private const int DEFAULTPAGESIZE = 10;
        private const int TOTALITEMS = 0;

        private int _pageSize = DEFAULTPAGESIZE;
        private int _totalCount = TOTALITEMS;
        private List<PageData> _pagesList = new List<PageData>();
        private int _pagesCount
        {
            get
            {
                return (int)Math.Ceiling(TotalCount / (decimal)PageSize);
            }
        }

        public List<T> Collection { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                _pageSize = value;
            }
        }
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                _totalCount = value;
            }
        }
        public List<PageData> PagesList
        {
            get
            {
                for (int i = 1; i <= _pagesCount; i++)
                {
                    PageData pd = new PageData();
                    pd.Page = i;
                    if (i == CurrentPage)
                    {
                        pd.IsSelected = true;
                    }
                    else pd.IsSelected = false;
                    _pagesList.Add(pd);
                }

                return _pagesList;
            }

        }

    }
    
}
