using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseYourLevelsUIManager : MonoBehaviour
{
    [Header("Level Menu")]
    [SerializeField] private AudioClip selectSound;

    public void Back()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.PlaySound(selectSound);
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
        SoundManager.instance.PlaySound(selectSound);
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
        SoundManager.instance.PlaySound(selectSound);
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
        SoundManager.instance.PlaySound(selectSound);
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
        SoundManager.instance.PlaySound(selectSound);
    }
    public void Level5()
    {
        SceneManager.LoadScene("Level5");
        SoundManager.instance.PlaySound(selectSound);
    }
}
