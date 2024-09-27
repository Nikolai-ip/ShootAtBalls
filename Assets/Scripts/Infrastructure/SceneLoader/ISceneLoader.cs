using System;

namespace Infrastructure.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
        string CurrentScene { get; }
    }
}