
namespace HRM.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext() : base("name=DefaultConnection")
    //    {
    //    }

    //    public DbSet<User> Users { get; set; }
    //}
}
