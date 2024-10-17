using UnityEngine;
using Zenject;

namespace NextTurn
{
  public class AudioInstaller : MonoInstaller
  {
    [SerializeField] private GameObject audioManagerPrefab;

    public override void InstallBindings()
    {
      Container
        .Bind<AudioManager>()
        .FromComponentInNewPrefab(audioManagerPrefab)
        .AsSingle()
        .NonLazy();
    }
  }
}