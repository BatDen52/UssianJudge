using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _disclamerPanel;
    [SerializeField] private GameObject _logoPanel;
    [SerializeField] private GameObject _aboutPanel;

    private void Awake()
    {
        PlayerPrefs.SetInt("CountDay", 0);
        PlayerPrefs.SetInt("Results", 0);
        _disclamerPanel.SetActive(true);
        StartCoroutine(InMenu());
    }

    private IEnumerator InMenu()
    {
        yield return new WaitForSeconds(5f);
        _disclamerPanel.SetActive(false);
        yield return new WaitForSeconds(5f);
        _logoPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Newpaper");
    }

    public void About()
    {
        _disclamerPanel.SetActive(!_disclamerPanel.activeSelf);
        _aboutPanel.SetActive(!_aboutPanel.activeSelf);
    }
}
