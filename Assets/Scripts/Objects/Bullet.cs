using System;
using UnityEngine;

namespace NextTurn
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class Bullet : MonoBehaviour
  {
    #region PARAMETERS
    [SerializeField] private float speed;
    [SerializeField] private int   maxHitCount = 4;

    private Rigidbody2D rb;
    private int         hitCount = 0;
    #endregion

    #region PROPERTIES
    public Vector2 Direction { get; set; }
    #endregion

    #region EVENTS
    public Action OnLifespanEnded;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
      rb.velocity = speed * Direction;
    }
    #endregion

    #region COLLISIONS
    private void OnCollisionEnter2D(Collision2D _collision)
    {
      if (_collision.otherCollider.CompareTag("Player"))
      {
        // todo: player die, play anim, restart
      }

      if (hitCount++ >= maxHitCount)
        EndLifespan();
    }
    #endregion

    #region METHODS
    private void Reset()
    {
      rb.velocity = Vector2.zero;
      hitCount    = 0;
      gameObject.SetActive(false);
    }
    #endregion

    #region INTERFACE
    public void EndLifespan()
    {
      OnLifespanEnded?.Invoke();
      Reset();
    }
    #endregion
  }
}
