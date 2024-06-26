﻿using AutoMapper;

namespace ShopWatch.WebMvc.App_Start
{
	public static class AutoMapperConfiguration
	{
        public static void Config()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile(new EntityModelMapper());
            });
        }
    }
}