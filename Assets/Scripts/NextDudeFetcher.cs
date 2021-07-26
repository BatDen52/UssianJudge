using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextDudeFetcher : MonoBehaviour
{
    [SerializeField] private Animator _avtozak;
    [SerializeField] private Animator _convoyBig;
    [SerializeField] private Animator _convoySmall;
    [SerializeField] private Animator _procurorText;
    [SerializeField] private GameObject _door;
    [SerializeField] private Passport _pass;

    [SerializeField] private Button _buttonNext;
    
    [SerializeField] private Case _case;
    [SerializeField] private GameObject _tutorial;

    private void Awake()//не здесь
    {
        if (PlayerPrefs.GetInt("CountDay") == 0)
        {
            _tutorial.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _buttonNext.onClick.AddListener(Fetch);
    }

    private void OnDisable()
    {
        _buttonNext.onClick.RemoveListener(Fetch);
    }

    public void Fetch()
    {
        _buttonNext.enabled = false;
        //_buttonNext.onClick.RemoveListener(Fetch);
        StartCoroutine(FetchAnimation());
    }

    public IEnumerator FetchAnimation()
    {
        _case.Generate();//не здесь?

        _avtozak.SetTrigger("CanArrive");
        yield return new WaitWhile(() => !_avtozak.GetBool("IsOpen"));

        _convoySmall.SetTrigger("CanGoIn");
        yield return new WaitWhile(() => !_convoySmall.GetBool("IsCame"));

        _convoyBig.SetTrigger("CanShow");
        _door.SetActive(true);
        _pass.Show();
        yield return new WaitWhile(() => !_convoyBig.GetBool("IsCame"));
        _door.SetActive(false);

        _procurorText.SetTrigger("CanShow");
    }
   
    public void TakeAway(int countOfSentence)
    {
        //_buttonNext.onClick.RemoveListener(Fetch);
        StartCoroutine(TakeAwayAnimation(countOfSentence));
    }

    public IEnumerator TakeAwayAnimation(int countOfSentence)
    {
        _procurorText.SetTrigger("CanHide");

        _convoyBig.SetTrigger("CanHide");
        _door.SetActive(true);
        _pass.Hide();
        yield return new WaitWhile(() => _convoyBig.GetBool("IsCame"));
        _door.SetActive(false);

        _convoySmall.SetTrigger("CanGoOut");
        yield return new WaitWhile(() => _convoySmall.GetBool("IsCame"));

        _avtozak.SetTrigger("CanClose");
        yield return new WaitWhile(() => _avtozak.GetBool("IsOpen"));

        if (countOfSentence < 5)//условие окончания заседания; не здесь
            _buttonNext.enabled = true;
        else
            Exit();
    }

    public void Exit()//+обед?
    {
        PlayerPrefs.SetInt("CountDay", PlayerPrefs.GetInt("CountDay",0)+1);
        SceneManager.LoadScene("Newpaper");
    }
}
