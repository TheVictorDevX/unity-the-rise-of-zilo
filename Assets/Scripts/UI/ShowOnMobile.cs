using UnityEngine;

public class ShowOnMobile : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform); //Works for WebGL too! :)
    }
    private void Update()
    {
        ////If pause screen already active unpause and vice versa
        //if (pauseScreen.activeInHierarchy)
        //{
        //    PauseGame(false);
        //}
        //else
        //{
        //    PauseGame(true);
        //}
    }
}
