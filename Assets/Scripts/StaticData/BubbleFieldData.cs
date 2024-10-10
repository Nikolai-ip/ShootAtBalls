using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Data/BubbleField", fileName = "BubbleField")]
    public class BubbleFieldData:ScriptableObject
    {
        [TextArea(10, 20)] public string Field;
        
    }
}