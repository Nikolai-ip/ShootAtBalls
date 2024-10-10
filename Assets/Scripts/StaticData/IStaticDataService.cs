using Infrastructure.Services;

namespace StaticData
{
    public interface IStaticDataService:IService
    {
        BubbleFieldData BubbleFieldData { get; }
        void LoadGameFieldData();
    }
}