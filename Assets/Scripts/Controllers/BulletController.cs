using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace NextTurn
{
  public class BulletController : MonoBehaviour
  {
    #region PARAMETERS
    [SerializeField] private Camera currentCamera;
    [SerializeField] private Bullet bulletPrefab;

    private SpriteRenderer sprite;
    private Bullet         bullet;
    private Vector2        mouseDirection;
    private bool           canFire = true;

    [Inject] private JamInputActions inputActions;
    #endregion

    #region PROPERTIES
    public bool CanFire { private set {
                            canFire = value;
                            sprite.gameObject.SetActive(canFire);
                          }
                          get => canFire;
                        }
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
      sprite = GetComponentInChildren<SpriteRenderer>();
      bullet = Instantiate(bulletPrefab, sprite.transform.position, sprite.transform.rotation);
      bullet.gameObject.SetActive(false);
    }

    private void Start()
    {
      inputActions.Player.Fire.performed += Fire;
      bullet.OnLifespanEnded             += ShowAim;
    }

    private void LateUpdate()
    {
      RotateTowardsMouse();
    }

    private void OnDestroy()
    {
      inputActions.Player.Fire.performed -= Fire;
      bullet.OnLifespanEnded             -= ShowAim;
    }
    #endregion

    #region INPUT ACTIONS CALLBACKS
    private void Fire(CallbackContext _context)
    {
      if (!CanFire)
        return;

      bullet.transform.position = sprite.transform.position;
      bullet.Direction          = mouseDirection.normalized;
      CanFire                   = false;
      bullet.gameObject.SetActive(true);
    }
    #endregion

    #region METHODS
    private void RotateTowardsMouse()
    {
      Vector3 mousePos   = Mouse.current.position.ReadValue();
      Vector3 objectPos  = currentCamera.WorldToScreenPoint(transform.position);
      mouseDirection     = new Vector2(mousePos.x - objectPos.x, mousePos.y - objectPos.y);
      transform.rotation = Quaternion.Euler(new Vector3(0f,
                                                        0f,
                                                        Mathf.Rad2Deg * Mathf.Atan2(mouseDirection.y, mouseDirection.x) - 90f));
    }

    private void ShowAim()
    {
      CanFire = true;
    }
    #endregion
  }
}
