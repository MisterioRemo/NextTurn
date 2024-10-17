using UnityEngine;
using Zenject;

namespace jam
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