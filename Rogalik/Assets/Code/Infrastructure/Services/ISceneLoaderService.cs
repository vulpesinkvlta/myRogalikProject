using System;

namespace Core
{
    public interface ISceneLoaderService
    {
        void Load(string name, Action OnLoaded = null);
    }
}