using UnityEngine;
using Zenject;

namespace NextTurn
{
  public class PlayerInstaller : MonoInstaller
  {
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAnimator animator;

    public override void InstallBindings()
    {
      Container
        .Bind<PlayerMovement>()
        .FromInstance(movement)
        .AsSingle()
        .NonLazy();

      Container
        .Bind<PlayerAnimator>()
        .FromInstance(animator)
        .AsSingle()
        .NonLazy();
    }
  }
}