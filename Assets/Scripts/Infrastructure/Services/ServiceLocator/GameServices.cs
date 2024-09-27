using System;
using System.Collections.Generic;

namespace Infrastructure.Services.ServiceLocator
{
    public class GameServices
    {
        private static GameServices _instance;
        public static GameServices Container => _instance ??= new GameServices();
        public void RegisterSingle<TService>(TService implementation)where TService:IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }
        public TService Single<TService>()where TService:IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService:IService
        {
            public static TService ServiceInstance;
        }
    }
}
