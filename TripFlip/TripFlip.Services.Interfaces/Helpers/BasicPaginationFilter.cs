namespace TripFlip.Services.Interfaces.Helpers
{
    public class BasicPaginationFilter
    {
		private static readonly int _maxPageSize = 50;

		private static readonly int _minPageSize = 5;

		private int _pageNumber;

		private int _pageSize;

		public int PageNumber 
		{ 
			get
			{
				return _pageNumber;
			}
			set
			{
				_pageNumber = (value > 0) ? value : 1;
			}
		}

		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				if (value < _minPageSize)
				{
					_pageSize = _minPageSize;
				}
				else if (value > _maxPageSize)
				{
					_pageSize = _maxPageSize;
				}
				else
				{
					_pageSize = value;
				}
			}
		}
	}
}
