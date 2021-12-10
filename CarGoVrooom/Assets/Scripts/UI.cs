using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private int _score = 0;

    [SerializeField]private TMP_Text scoreText;
    [SerializeField] private TMP_Text TitleText;
    [SerializeField]private TMP_Text messageText;
    [SerializeField] private TMP_Text enterCarText;
    // Start is called before the first frame update
    void Start()
    {
        Obstacle.destroyedEvent += AddScore;
        SetScoreText(_score);
        TitleText.text = null;
        messageText.text = null;
        enterCarText.text = null;
    }

    private void OnDisable()
    {
        Obstacle.destroyedEvent -= AddScore;
    }

    public void AddScore()
    {
        _score++;
        SetScoreText(_score);
    }

    public void SetScoreText(int points)
    {
        scoreText.text = "Score: " + points.ToString();
    }

    public void SetEnterText(bool canEnter)
    {
        if (canEnter)
        {
            enterCarText.text = "Press 'E' \n to enter";
        }
        else
        {
            enterCarText.text = null;
        }
    }

    public void ShowAchievment(string achievmentTitle, string achievmentDescription)
    {
        StartCoroutine(ShowMessage(achievmentTitle, achievmentDescription));
    }

    private IEnumerator ShowMessage(string title, string message)
    {
        TitleText.text = title;
        messageText.text = message;
        yield return new WaitForSeconds(3);
        TitleText.text = null;
        messageText.text = null;

    }
}
