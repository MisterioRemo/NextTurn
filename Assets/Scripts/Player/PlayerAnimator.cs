using UnityEngine;
using Zenject;

namespace NextTurn
{
  public class PlayerAnimator : MonoBehaviour
  {
    #region PARAMETERS
    [SerializeField] private float maxTilt;
    [SerializeField] private float tiltSpeed;

    [SerializeField] private ParticleSystem jumpFX;
    [SerializeField] private ParticleSystem landFX;
    [SerializeField] private ParticleSystem turnFX;
    [SerializeField] private ParticleSystem hitFX;

    private Animator       anim;
    private SpriteRenderer sprite;
    private bool           justLanded;
    private bool           startedJumping;

    [Inject] private PlayerMovement  movement;
    #endregion

    #region PROPERTIES
    public bool JustLanded     { get => justLanded;
                                 set { justLanded = value;
                                       if (justLanded) OnLanded();
                                     }
                               }
    public bool StartedJumping { get => startedJumping;
                                 set { startedJumping = value;
                                       if (startedJumping) OnStartedJumping();
                                     }
                               }
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
      anim   = GetComponentInChildren<Animator>();
      sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void LateUpdate()
    {
      Tilt();
    }
    #endregion

    #region METHODS
    private void Tilt()
    {
      float tiltProgress;
      int   mult = -1;

      if (movement.IsSliding)
      {
        tiltProgress = 0.25f;
      }
      else
      {
        tiltProgress = Mathf.InverseLerp(-movement.Data.runMaxSpeed, movement.Data.runMaxSpeed, movement.RB.velocity.x);
        mult         = movement.IsFacingRight ? 1 : -1;
      }

      float rot = Mathf.LerpAngle(sprite.transform.localRotation.eulerAngles.z * mult,
                                  tiltProgress * maxTilt * 2 - maxTilt,
                                  tiltSpeed);
      sprite.transform.localRotation = Quaternion.Euler(0, 0, rot * mult);
    }
    #endregion

    #region INTERFACE
    public void OnTurned()
    {
      turnFX.Play();
    }

    public void OnLanded()
    {
      anim.SetTrigger("Land");
      landFX.Play();
      JustLanded = false;
    }

    public void OnStartedJumping()
    {
      anim.SetTrigger("Jump");
      jumpFX.Play();
      StartedJumping = false;
    }

    public void OnHitted(Vector3 _hitPosition)
    {
      hitFX.gameObject.transform.position = _hitPosition;
      hitFX.Play();
    }
    #endregion
  }
}
