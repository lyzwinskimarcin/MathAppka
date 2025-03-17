using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;



public class CongratulationLogic : MonoBehaviour
{
    public TextMeshProUGUI congratualtionsTextField;
    public Button nextButton;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowCongratsMessage();
        nextButton.onClick.AddListener(nextScene);
    }
    
    
    void ShowCongratsMessage()
    {
        string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        congratualtionsTextField.text = $"Gratulacje! Zrobiłeś 40 przykładów.\nData: {dateTime}";
    }

    public void nextScene()
    {
        SceneManager.LoadScene("MathGame");
    }
}
