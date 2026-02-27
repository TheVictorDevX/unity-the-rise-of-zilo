using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "GoalPost")
        {
            uiManager.MobileUI(false);
            // 1. Get the full name (e.g., "Level10")
            string fullSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("fullSceneName: " + fullSceneName);

            // 2. Remove "Level" to get just the number string (e.g., "10")
            string levelNumberString = fullSceneName.Replace("Level", "");

            // 3. Optional: Convert it to an actual integer if you need to do math
            int levelIndex = int.Parse(levelNumberString);
            Debug.Log("levelIndex: " + levelIndex);

            if (levelIndex< LevelManager.TotalLevels)
            {
                // Update the value to the next level
                PlayerPrefs.SetInt("ReachedIndex", levelIndex + 1);
                Debug.Log("ReachedIndex: " + (levelIndex + 1));

                // 4. Force Unity to write the data to the disk immediately
                PlayerPrefs.Save();

                uiManager.YouWin(true);
            }
            else
            {
                uiManager.YouWin(false);

            }
        }
    }
}
