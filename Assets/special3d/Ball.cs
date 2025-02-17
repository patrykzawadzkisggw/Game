using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public int maxMoney=10;
    UIManager uIManager;
    bool isWin= false;
    int Money;
    [SerializeField] private Image currentmoneyBar;
    [SerializeField] private Text moneytxt;
    private bool ismoving=true;
    private void Start()
    {
        Money = maxMoney;
        uIManager = GameObject.FindObjectOfType<UIManager>();
        InvokeRepeating("DecreaseMoney", 1.0f, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if(ismoving) {
                ismoving = false;
                isWin = true;
            uIManager.Win(SceneManager.GetActiveScene().buildIndex,Money,0,Vector3.one,true);
            }
            
        }
    }
    private void DecreaseMoney()
    {
        if (isWin) return;

        
        
        if(Money<=0)
        {
            uIManager.GameOver();
            return;
        }
        Money--;
    }

    private void Update()
    {
        currentmoneyBar.fillAmount = Money / (float)maxMoney;
        moneytxt.text = Money.ToString();
    }
}
