using UnityEngine;

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
            uiManager.YouWin();
        }
    }
}
