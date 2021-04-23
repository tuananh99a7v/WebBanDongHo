using System;
using System.Data.Entity;
using System.Linq;

namespace ShopWatch.Model.DataContext
{
	public class DbInitializer : CreateDatabaseIfNotExists<ShopWatchDataContext>
	{
		protected override void Seed(ShopWatchDataContext context)
		{
			if (context.AccountRoles.Any())
			{
				return;
			}
			#region Add AccountRoles
			var accountRoles = new AccountRole[]
			{
			new AccountRole()
			{
				RoleName="Customer",
				Description="Là khách hàng của website"
			},
			new AccountRole()
			{
				RoleName="Admin",
				Description="Là người quản trị trang web"
			}
			};
			context.AccountRoles.AddRange(accountRoles);
			context.SaveChanges();

			#endregion
			#region Add Accounts
			var accounts = new Account[]
			{
			new Account()
			{
				AccountName="tuananh99a7v",
				Password="123@abcd",
				CreateDate=DateTime.Now,
				ModifiedDate=DateTime.Now,
				AccountRole=accountRoles.Single(s=>s.RoleName=="Customer"),
			},
			new Account()
			{
				AccountName="anh99a7v",
				Password="123@abcd",
				CreateDate=DateTime.Now,
				ModifiedDate=DateTime.Now,
				AccountRole=accountRoles.Single(s=>s.RoleName=="Customer"),

			},
			new Account()
			{
				AccountName="admin",
				Password="123@abcd",
				CreateDate=DateTime.Now,
				ModifiedDate=DateTime.Now,
				AccountRole=accountRoles.Single(s=>s.RoleName=="Admin"),

			},
			};
			context.Accounts.AddRange(accounts);
			context.SaveChanges();

			#endregion
			#region AddUser
			var users = new User[]
			{
			new User()
			{

				Name="Nguyễn Tuấn Anh",
				City="Hải Dương",
				Address="Văn Tố - Tứ Kỳ",
				Photo="anh.jpg",
				BirthDate=new DateTime(1999,12,30),
				Email="tuananh99a7v@gmail.com",
				Phone="0327733312",
				Account=accounts.Single(s=>s.AccountName=="tuananh99a7v"),
			},
			new User()
			{
				Name="Phạm Hoàng Anh",
				City="Hải Dương",
				Address="Tứ Kỳ",
				Photo="hoang.jpg",
				BirthDate=new DateTime(1999,08,10),
				Email="hoanganh99@gmail.com",
				Phone="0927789312",
				Account=accounts.Single(s=>s.AccountName=="anh99a7v"),
			},
			};
			context.Users.AddRange(users);
			context.SaveChanges();

			#endregion
			#region Add Publishers
			var publishers = new Publisher[]
			{
				new Publisher()
				{
					PublisherName="Tissot",
					Image="p1.jpg",
					Description="Tissot là một trong số những thương hiệu đồng hồ sang trọng đến từ Thụy Sĩ." +
					"Thương hiệu được thành lập tại thị trấn Le Locle, Thụy Sĩ bởi nghệ nhân chế tác đồng hồ Charles-Felicien và con trai Charles-Emile Tissot vào năm 1853"
				},
				new Publisher()
				{
					PublisherName="Calvin Klein",
					Image="p2.jpg",
					Description="Calvin Klein là một nhãn hiệu thời trang được nhà thiết kế cùng tên Calvin Klein thành lập năm 1968. Công ty có trụ sở tại Midtown Manhattan, New York City[1] và hiện tại do Phillips-Van Heusen (PVH) sở hữu. Giống như các thương hiệu thời trang khác, Calvin Klein nổi tiếng với biểu tượng viết tắt của tên công ty là cK"
				},
				new Publisher()
				{
					PublisherName="Rolex",
					Image="p3.jpg",
					Description="Rolex với tên gọi chính xác là Rolex SA là một hãng sản xuất đồng hồ Thụy Sỹ với phân khúc từ cao cấp đến xa xỉ và sang trọng. Rolex SA bao gồm hai thương hiệu là Rolex và Montres Tudor SA chuyên thiết kế, sản xuất, phân phối đồng hồ đeo tay dưới thương hiệu Rolex và Tudor."
				}
			};
			context.Publishers.AddRange(publishers);
			context.SaveChanges();

			#endregion
			#region Add Categories
			var categories = new Category[]
			{
			new Category()
			{
				CategoryName="Đồng hồ nam",
				Description="Đồng hồ dành cho nam giới",
			},
			new Category()
			{
				CategoryName="Đồng hồ nữ",
				Description="Đồng hồ dành cho nữ giới",
			},
			new Category()
			{
				CategoryName="Đồng hồ trẻ em",
				Description="Đồng hồ dành cho trẻ em",
			},
			};
			context.Categories.AddRange(categories);
			context.SaveChanges();

			#endregion
			#region Add Watches
			var watches = new Watch[]
			{
			new Watch()
			{
				WatchName="Đồng hồ Tissot T106.417.16.031.00",
				Description="Đồng hồ nam, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 2 năm",
				ImageUrl="SP00001.jpg",
				SmallImage="SP00001",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=11930000,
				PricePromotion=11930000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Tissot")
			},
			new Watch()
			{
				WatchName="Đồng hồ Tissot T085.410.22.013.00",
				Description="Đồng hồ nam, Dây da, Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 2 năm, ",
				ImageUrl="SP00002.jpg",
				SmallImage="SP00002",
				Quantity=200,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				PricePromotion=10980000,
				Price=10980000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Tissot")
			},
			new Watch()
			{
				WatchName="Đồng hồ Tissot T063.617.36.037.00",
				Description="Đồng hồ nam, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 2 năm",
				ImageUrl="SP00003.jpg",
				SmallImage="SP00003",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=14060000,
				PricePromotion=14060000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Tissot")
			},
			new Watch()
			{
				WatchName="Đồng hồ Tissot Nữ T112.210.11.036.00",
				Description="Tissot T-Wave toát lên vẻ đẹp thanh lịch, nhẹ nhàng qua những chi tiết thiết kế vô cùng tinh tế. Bộ vỏ tròn kết hợp hài hoà với kiểu dây to bản cùng chất mang đến sự hài hoà và bền bỉ cho tạo tác. Đại diện là chiếc Tissot T112.210.11.036.00 với vẻ ngoài thanh lịch, đầy né nữ tính.",
				ImageUrl="SP00004.jpg",
				SmallImage="SP00004",
				Quantity=50,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=11725000,
				PricePromotion=11725000,

				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Tissot")
			},
			new Watch()
			{
				WatchName="Đồng hồ Rolex GMT-MASTER II",
				Description="Giống với tất cả các phiên bản đồng hồ Chuyên dụng của Rolex, GMT-Master II mang lại sự rõ nét đặc biệt trong bất kỳ tình huống nào, đặc biệt trong bóng đêm, nhờ vào hiển thị Chromalight của đồng hồ. Các kim giờ và điểm báo giờ lớn hình dạng đơn giản - hình tam giác, hình tròn, hình chữ chật - được phủ một loại chất liệu phát quang phát ra ánh sáng bền bỉ.",
				ImageUrl="SP00005.jpg",
				SmallImage="SP00005",
				Quantity=11,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=15000000,
				PricePromotion=15000000,

				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Rolex")
			},
			new Watch()
			{
				WatchName="Đồng hồ Rolex DATEJUST 41",
				Description="Đai kính rãnh Rolex là dấu ấn của sự khác biệt. Ban đầu, vành đồng hồ rãnh Oysterđược hình thành để xoắn chặt vành đồng hồ và vỏ đồng hồ một cách dễ dàng nhằm đảm bảo chống thấm nước tuyệt đối cho đồng hồ.",
				ImageUrl="SP00006.jpg",
				SmallImage="SP00006",
				Quantity=15,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=310000000,
				PricePromotion=310000000,
				IsActive=false,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Rolex")
			},
			new Watch()
			{
				WatchName="Đồng hồ Rolex Oyster Perpetual Datejust 31",
				Description="Đồng hồ Oyster Perpetual Datejust 31 bằng Oystersteel và vàng trắng đi kèm mặt số màu aubergine, nạm kim cương và dây đeo Oyster",
				ImageUrl="SP00007.jpg",
				SmallImage="SP00007",
				Quantity=25,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=5500000,
				PricePromotion=5500000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Rolex")

			},
			new Watch()
			{
				WatchName="Đồng hồ Rolex Oyster Perpetual Datejust 36",
				Description="Đồng hồ Oyster Perpetual Datejust 36 bằng Oystersteel và vàng vàng đi kèm mặt số màu champagne, nạm kim cương và dây đeo Oyster",
				ImageUrl="SP00008.jpg",
				SmallImage="SP00008",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=7800000,
				PricePromotion=7800000,

				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Rolex")
			},
			new Watch()
			{
				WatchName="ĐỒNG HỒ CALVIN KLEIN MINIMAL K3M23V26",
				Description="Đồng hồ nữ, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính cứng, Bảo hành toàn quốc 1 năm",
				ImageUrl="SP00009.jpg",
				SmallImage="SP00009",
				Quantity=32,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=8030000,
				PricePromotion=8030000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Calvin Klein")

			},
			new Watch()
			{
				WatchName="Đồng hồ Tissot T106.417.16.031.00",
				Description="Đồng hồ nữ, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 1 năm",
				ImageUrl="SP00010.jpg",
				SmallImage="SP00010",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=11930000,
				PricePromotion=11930000,
				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Calvin Klein")
			},
			new Watch()
			{
				WatchName="ĐỒNG HỒ CALVIN KLEIN STATELY K3G23626",
				Description="Đồng hồ nữ, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 1 năm",
				ImageUrl="SP00011.jpg",
				SmallImage="SP00011",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=6050000,
				PricePromotion=6050000,
				IsActive=false,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nữ"),
				Publisher=publishers.Single(s=>s.PublisherName=="Calvin Klein")
			},
			new Watch()
			{
				WatchName="ĐỒNG HỒ CALVIN KLEIN K5S341C6",
				Description="Đồng hồ nam, Dây da, Vỏ Thép không gỉ 316L/Mạ PVD, Kính sapphire, Bảo hành toàn quốc 1 năm",
				ImageUrl="SP00012.jpg",
				SmallImage="SP00012",
				Quantity=100,
				CreatedDate=new DateTime(2021,3,30,10,20,11),
				ModifiedDate=new DateTime(2021,3,30,10,20,11),
				Evaluate="Đồng hồ cao cấp, xứng đáng với giá tiền",
				Price=10930000,
				PricePromotion=10930000,

				IsActive=true,
				Category=categories.Single(s=>s.CategoryName=="Đồng hồ nam"),
				Publisher=publishers.Single(s=>s.PublisherName=="Calvin Klein")
			}
			};
			context.Watches.AddRange(watches);
			#endregion
			context.SaveChanges();
		}
	}
}