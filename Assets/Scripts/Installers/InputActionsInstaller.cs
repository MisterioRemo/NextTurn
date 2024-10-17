using UnityEngine;
using Zenject;

namespace NextTurn
{
  public class InputActionsInstaller : MonoInstaller
  {
    [SerializeField] private bool isUI = false;

    public override void InstallBindings()
    {
      Container
        .Bind<JamInputActions>()
        .FromNew()
        .AsSingle()
        .OnInstantiated<JamInputActions>
          ((context, inputActions) => { if (isUI) inputActions.UI.Enable();
                                        else inputActions.Player.Enable();
                                        // InputSystem.DisableDevice(Mouse.current);
          })
        .NonLazy();
    }
  }

}