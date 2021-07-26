using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Newspaper : MonoBehaviour
{
    [SerializeField] private Sprite[] _newspapers;
    [SerializeField] private Image _newspaperPanel;
    [SerializeField] private Image _btnExit;
    [SerializeField] private Sprite _btnExitSprite;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("CountDay") == 0)
        {
            _newspaperPanel.sprite = _newspapers[0];
        }
        else
        {
            if (PlayerPrefs.GetInt("CountDay") == 1)
            {
                if (PlayerPrefs.GetInt("Results") > 0)
                {
                    _newspaperPanel.sprite = _newspapers[2];
                }
                else
                {
                    _newspaperPanel.sprite = _newspapers[1];
                }
            }
            else if (PlayerPrefs.GetInt("CountDay") == 2)
            {
                if (PlayerPrefs.GetInt("Results") > 0)
                {
                    _newspaperPanel.sprite = _newspapers[4];
                }
                else
                {
                    _newspaperPanel.sprite = _newspapers[3];
                }
            }
            else if (PlayerPrefs.GetInt("CountDay") == 3)
            {
                if (PlayerPrefs.GetInt("Results") > 0)
                {
                    _newspaperPanel.sprite = _newspapers[6];
                }
                else
                {
                    _newspaperPanel.sprite = _newspapers[5];
                }
                _btnExit.sprite = _btnExitSprite;
            }
        }
    }

    public void GoNext()
    {
        if (PlayerPrefs.GetInt("CountDay") == 3)
            SceneManager.LoadScene("Menu");
        else
            SceneManager.LoadScene("Game");
    }
}
