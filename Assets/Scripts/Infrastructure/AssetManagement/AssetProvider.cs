using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var gameObj = this.Instantiate(path);
            gameObj.transform.position = at;
            return gameObj;
        }

        public TComponent Instantiate<TComponent>(string path) where TComponent : MonoBehaviour
        {
            var prefab = Resources.Load<TComponent>(path);
            return Object.Instantiate(prefab);
        }
    }
}