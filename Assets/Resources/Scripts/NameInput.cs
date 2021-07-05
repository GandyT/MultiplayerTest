using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField] private Button NextButton;
    private TMP_InputField InputText;
    private Color defaultButtonColor;

    void Start()
    {
        defaultButtonColor = NextButton.image.color;
        InputText = GetComponent<TMP_InputField>();
        
        if (GameManager.INSTANCE.PlayerSettings.ContainsKey("DISPLAY_NAME"))
        {
            InputText.text = GameManager.INSTANCE.PlayerSettings["DISPLAY_NAME"];
        } else
        {
            InputText.text = GameManager.INSTANCE.GetRandomName();
        }
    }

    void FixedUpdate ()
    {
        if (InputText.text.Length <= 0)
        {
            NextButton.interactable = false;
            NextButton.image.color = Color.red;
        } else
        {
            NextButton.interactable = true;
            NextButton.image.color = defaultButtonColor;
        }
    }
}
