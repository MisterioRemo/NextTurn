using System.Collections;
using UnityEngine;

namespace NextTurn
{
  public class Rotator : MonoBehaviour
  {
    #region PARAMETERS
    private GameObject parent;
    private bool       isRotating = false;

    [SerializeField] private float speed;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
      parent = transform.root.gameObject;
    }
    #endregion

    #region COLLISIONS
    private void OnTriggerEnter2D(Collider2D _collision)
    {
      if (_collision.CompareTag("Bullet") && !isRotating)
      {
        StartCoroutine(Rotate(parent, -90f));
        if (_collision.transform.root.GetComponent<Bullet>() is Bullet bullet)
          bullet.EndLifespan();
      }
    }
    #endregion

    #region METHODS
    private IEnumerator Rotate(GameObject _object, float _angleZ)
    {
      isRotating = true;

      Quaternion from = _object.transform.rotation;
      Quaternion to   = Quaternion.Euler(0f, 0f, _object.transform.eulerAngles.z + _angleZ);
      float      time = 0f;

      while (time <= 1f)
      {
        _object.transform.rotation = Quaternion.Lerp(from, to, time);
        time                      += speed * Time.deltaTime;

        yield return new WaitForEndOfFrame();
      }

      _object.transform.rotation = to; // to be sure
      isRotating                 = false;
    }
    #endregion
  }
}
