using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    public TextMeshProUGUI problem;
    public TMP_InputField userInputField;
    public AudioSource audioSource;
    
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;
    

    public int n1;
    public int n2;
    public int answer;

    public string whereQ;

    public int numberCorrect = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        userInputField.onEndEdit.AddListener(OnEndEdit);
        GenerateProblem();
        
    }


    void GenerateProblem()
    {
        // Generate random numbers form a given range
        n1 = Random.Range(2, 18);

        if (n1 > 11)
        {
            n2 = n1;
        }
        else
        {
            n2 = Random.Range(2, 11);
        }
        
        answer = n1 * n2;

        if (n1 > 11)
        {
            whereQ = "Third";
            problem.text = $"{n1} x {n2} = ?";
            return;
        }
        
        // Randomize where to put question mark
        int randomIndex = UnityEngine.Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                whereQ = "First";
                problem.text = $"? x {n2} = {answer}";
                break;
            case 1:
                whereQ = "Second";
                problem.text = $"{n1} x ? = {answer}";
                break;
            case 2:
                whereQ = "Third";
                problem.text = $"{n1} x {n2} = ?";
                break;
        }
        

    }

    public void OnEndEdit(string userStringInput)
    {
        int userInput = 0;
        if (!int.TryParse(userStringInput, out userInput))
        {
            // incorrect
            IncorrectAnswer();
        }
        else
        {
            if (CheckAnswer(userInput))
            {
                CorrectAnswer();
                if (numberCorrect == 40)
                {
                    // What to do after 40 correct
                    SceneManager.LoadScene("CongratulationScreen");
                }
            }
            else
            {
                IncorrectAnswer();
            }
        }
        userInputField.Select();
        userInputField.ActivateInputField();        
    }
    

    public bool CheckAnswer(int userInput)
    {
        if (whereQ == "First")
        {
            return n1 == userInput;
        }
        else if (whereQ == "Second")
        {
            return n2 == userInput;
        }
        else if (whereQ == "Third")
        {
            return answer == userInput;
        }
        return false;
    }
    
    private void CorrectAnswer()
    {
        audioSource.PlayOneShot(correctAnswerSound);
        userInputField.text = "";
        numberCorrect++;
        GenerateProblem();
    }

    public void IncorrectAnswer()
    {
        audioSource.PlayOneShot(wrongAnswerSound);
        userInputField.text = "";
    }
    
    public void SaveJson()
    {
        string jsonContent = "{\"done\": true}";
        string path = Application.persistentDataPath + "/log.json";
        
        File.WriteAllText(path, jsonContent);
    }
}



