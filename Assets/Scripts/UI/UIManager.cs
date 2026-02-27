using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("You Win!")]
    [SerializeField] private GameObject youWinScreen;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private Button nextLevelButton;

    [Header("Mobile UI")]
    [SerializeField] private GameObject mobileUI;

    public static bool pause;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        youWinScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Health.dead)
        {
            //If pause screen already active unpause and vice versa
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else {
                PauseGame(true);
            }
        }
    }

    public void MobileUI(bool value)
    {
        mobileUI.SetActive(value);
    }

    #region Game Over

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        MobileUI(false);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        Time.timeScale = 1; // <--- Add this line!
        SceneManager.LoadScene(0);
        pause = false;
    }
    
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor) 
        #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        pause = status;

        MobileUI(!status);

        //When pause status is true change timescale to 0 (time stops)
        //When it's false change it back to 1 (time goes by normally)
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region You Win!
    public void YouWin(bool allow)
    {
        youWinScreen.SetActive(true);
        SoundManager.instance.PlaySound(winSound);
        Time.timeScale = 0;
        if (allow)
        {
            nextLevelButton.interactable = true;
        }
        else 
        {
            nextLevelButton.interactable = false;
        }
    }
    public void NextLevel()
    {
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);
        SceneManager.LoadScene("Level"+reachedIndex);
        Time.timeScale = 1;
    }
    #endregion
}
