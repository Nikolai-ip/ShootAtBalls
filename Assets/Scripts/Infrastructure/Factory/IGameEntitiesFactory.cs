using Infrastructure.Services;

namespace Infrastructure.Factory
{
    public interface IGameEntitiesFactory:IService
    {
        void CreateGameEntities();
    }
}