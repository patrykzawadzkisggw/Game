using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader1 : MonoBehaviour
{
    public int levelNumber;
    [SerializeField] private Image unlocked;
    [SerializeField] private Image locked;
    private Button targetButton;
    public Text moneyText;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelNumber);
    }
    private void Start()
    {
        targetButton = GetComponent<Button>();
        GameLogger g1 = new GameLogger();
        if (levelNumber != 1 && targetButton != null)
        {
            int lastlevel = g1.getMaxCompletedScene();
            if(levelNumber<= lastlevel+1)
            {
                targetButton.image.sprite = unlocked.sprite;
                return;
            }
            Text buttonText = targetButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.gameObject.SetActive(false);
            }


            targetButton.interactable = false;


            targetButton.transition = Selectable.Transition.None;
            
        }
        if (moneyText != null)
        {
            moneyText.text = g1.getTotalMoney().ToString();
        }

    }

    
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
