using System.Collections.Generic;

namespace Infrastructure.AssetManagement
{
    public static class AssetPath
    {
        public static string GunPrefab = "Prefabs/GameEntities/Gun";
        public static string MouseInputPrefab = "Prefabs/Infrastructure/MouseInputController";
        public static string BubbleContainerPrefab = "Prefabs/GameEntities/BubbleContainer";
        public static string BubbleFieldPrefab = "Prefabs/GameEntities/BubbleField";
        public static string BubbleFieldData = "Data/BubbleField";
        public static string BubbleGunIndicatorPrefab = "Prefabs/UI/BubbleGunIndicator";
        private static string BubbleIndicatorFolder = "Prefabs/GameEntities/Bubble/Indicators/";
        public static List<string> BubbleIndicators = new()
        {
            BubbleIndicatorFolder+"Red",
            BubbleIndicatorFolder+"Green",
            BubbleIndicatorFolder+"Blue"

        };

        public static string Canvas = "Prefabs/UI/Canvas";
        public static string MagazineIndicatorPrefab = "Prefabs/UI/MagazineIndicator";
        public static string TrajectoryDrawerPrefab = "Prefabs/UI/TrajectoryDrawer";
    }
}