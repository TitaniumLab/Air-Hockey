using AirHockey;
using Unity.Netcode;
using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField] private NetworkObject _playerController;
    [SerializeField] private NetworkObject _malletPrefab;


    public override void InstallBindings()
    {
        Container.Bind<MovableFactory>().AsSingle().WithArguments(_malletPrefab);
        Container.Bind<PlayerControllerFactory>().AsSingle().WithArguments(_playerController);
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();
    }
}