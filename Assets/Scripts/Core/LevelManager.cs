using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Button[] levelButtons;
    // Change this to 'static' so PlayerWin can see it easily
    public static int TotalLevels;
    [SerializeField] private int levelsCount; // Set this in the Inspector

    void Awake()
    {
        TotalLevels = levelsCount;
    }
    void Start()
    {
        // Automatically gets all buttons that are children of this object
        levelButtons = GetComponentsInChildren<Button>();

        // Get the progress from memory (Default to 1 if first time playing)
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // If the level number (i + 1) is greater than our progress, lock it
            if (i + 1 > reachedIndex)
            {
                levelButtons[i].interactable = false;
            }

            // Optional: Add a listener so you don't have to manually 
            // set OnClick for every single button.
            //int levelNum = i + 1;
            //levelButtons[i].onClick.AddListener(() => LoadLevel(levelNum));
        }
    }

    //void LoadLevel(int levelId)
    //{
    //    SceneManager.LoadScene("Level " + levelId);
    //}
}
