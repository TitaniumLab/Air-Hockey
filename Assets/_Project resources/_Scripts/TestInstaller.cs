using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CheckTick>().AsSingle().NonLazy();
        //Container.Bind<ITickable>();
    }


    public class CheckTick : ITickable
    {
        public void Tick()
        {
            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }
}