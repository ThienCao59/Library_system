using System.Collections.Generic;

namespace IdentityReportService.DTOs
{
    public class DashboardReportDto
    {
        public UserStatsDto UserStats { get; set; } = null!;
        public CardStatsDto CardStats { get; set; } = null!;
        public List<MonthlyBorrowDto> MonthlyBorrowStats { get; set; } = null!;
        public List<PopularBookDto> TopPopularBooks { get; set; } = null!;
        public List<WeeklyCardDto> WeeklyCardStats { get; set; } = null!;
    }

    public class UserStatsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
    }

    public class CardStatsDto
    {
        public int TotalCards { get; set; }
        public int ActiveCards { get; set; }
        public int LockedCards { get; set; }
        public int ExpiredCards { get; set; }
    }

    public class MonthlyBorrowDto
    {
        public string Month { get; set; } = null!;
        public int BorrowCount { get; set; }
        public int ReturnCount { get; set; }
    }

    public class PopularBookDto
    {
        public string BookTitle { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int BorrowTimes { get; set; }
    }

    public class WeeklyCardDto
    {
        public string Week { get; set; } = null!;
        public int ActiveCards { get; set; }
        public int LockedCards { get; set; }
    }
}
