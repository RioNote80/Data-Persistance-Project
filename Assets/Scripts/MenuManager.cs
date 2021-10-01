using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text hiScoreText;
    int hiScore = 0;
    string hiScoreName = "No Name";

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        DataManager.instance.playerName = inputField.text;
    }

    private void Start()
    {
        hiScore = DataManager.instance.hiScore;
        hiScoreName = DataManager.instance.hiScoreName;
        hiScoreText.text = "Best Score : " + hiScoreName + " : " + hiScore;
    }
}
