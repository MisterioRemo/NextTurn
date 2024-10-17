
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NextTurn
{
  public class MainMenu : MonoBehaviour
  {
    #region CONSTANTS
    private const float CENTER_POSITION = 0f;
    private const float MENU_OFFSET     = 800f;
    private const float CREDITS_OFFSET  = 850f;
    private const float OUT_DURATION    = 0.8f;
    private const float IN_DURATION     = 0.8f;
    #endregion

    #region PARAMETERS
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject creditsWindow;
    //[SerializeField] private GameObject settingsWindow;
    #endregion

    #region INTERFACE
    public void StartGame()
    {
      //GameState.Instance.Reset();
      //SceneManager.LoadScene("SceneName");
    }

    public void ShowCredits()
    {
      creditsWindow.SetActive(true);
      LeanTween.moveLocalY(menuWindow, MENU_OFFSET, OUT_DURATION).setEase(LeanTweenType.easeInOutBack).setOnComplete(() => menuWindow.SetActive(false));
      LeanTween.moveLocalY(creditsWindow, CENTER_POSITION, IN_DURATION).setEase(LeanTweenType.easeInOutBack);
      LeanTween.alphaCanvas(menuWindow.GetComponent<CanvasGroup>(), 0f, OUT_DURATION);
      LeanTween.alphaCanvas(creditsWindow.GetComponent<CanvasGroup>(), 1f, IN_DURATION / 2f);
    }

    public void ShowMainMenu()
    {
      menuWindow.SetActive(true);
      LeanTween.moveLocalY(creditsWindow, -CREDITS_OFFSET, OUT_DURATION).setEase(LeanTweenType.easeInOutBack).setOnComplete(() => creditsWindow.SetActive(false));
      LeanTween.moveLocalY(menuWindow, CENTER_POSITION, IN_DURATION).setEase(LeanTweenType.easeInOutBack);
      LeanTween.alphaCanvas(creditsWindow.GetComponent<CanvasGroup>(), 0f, OUT_DURATION);
      LeanTween.alphaCanvas(menuWindow.GetComponent<CanvasGroup>(), 1f, IN_DURATION / 2f);
    }

    public void QuitGame()
    {
      Application.Quit();
    }
    #endregion
  }
}
