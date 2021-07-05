using UnityEngine;
using UnityEngine.UI;

public class MenuSceneLoader : MonoBehaviour
{
    [SerializeField] Canvas MainUI;

    public void LoadUniqueComponent (GameObject panel)
    {
        Transform[] panels = MainUI.GetComponentsInChildren<Transform>();
        foreach (Transform gamePanel in panels)
        {
            if (gamePanel.gameObject.name == MainUI.gameObject.name) continue; // holy crap im dumb
            
            gamePanel.gameObject.SetActive(false);
        }

        panel.SetActiveRecursively(true);
    }
}
