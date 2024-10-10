using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private BubbleFieldData _bubbleFieldData;
        public BubbleFieldData BubbleFieldData => _bubbleFieldData;

        public void LoadGameFieldData()
        {
            _bubbleFieldData = Resources.Load<BubbleFieldData>(AssetPath.BubbleFieldData);
        }

    }
}