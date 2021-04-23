using AutoMapper;
using ShopWatch.Model;
using ShopWatch.WebMvc.ViewModels;

namespace ShopWatch.WebMvc.App_Start
{
	public class EntityModelMapper : Profile
	{
		public EntityModelMapper()
		{
			CreateMap<Watch, WatchViewModel>();
			CreateMap<Category, CategoryViewModel>();
			CreateMap<Publisher, PublisherViewModel>();
			CreateMap<User, UserViewModel>();
			CreateMap<Promotion, PromotionViewModel>();
			CreateMap<Account, AccountViewModel>();

			CreateMap<WatchViewModel, Watch>();
			CreateMap<WatchEditViewModel, Watch>();
			CreateMap<PublisherEditViewModel, Publisher>();
			CreateMap<PublisherViewModel, Publisher>();
			CreateMap<CategoryViewModel, Category>();
			CreateMap<CategoryEditViewModel, Category>();
			CreateMap<UserViewModel, User>();
			CreateMap<UserEditViewModel, User>();
			CreateMap<PromotionViewModel, Promotion>();
			CreateMap<PromotionEditViewModel, Promotion>();
			CreateMap<LoginViewModel, Account>();
			CreateMap<AccountViewModel, Account>();
		} 
	}
}