using System.Data.Entity;

namespace ShopWatch.Model.DataContext
{
	public class ShopWatchDataContext:DbContext
	{
		public ShopWatchDataContext():base("name=ShopWatchDb")
		{
			Database.SetInitializer(new DbInitializer());
		}
		public DbSet<AccountRole> AccountRoles { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Watch> Watches { get; set; }
		public DbSet<User> Users{ get; set; }
		public DbSet<Promotion> Promotions { get; set; }
		public DbSet<Order> Orders{ get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Watch>().HasMany<Promotion>(s => s.Promotions)
				.WithMany(d => d.Watches)
				.Map(cs =>
				{
					cs.MapLeftKey("WatchId");
					cs.MapRightKey("PromotionId");
					cs.ToTable("WatchPromotion");
				});
		}
	}
}
