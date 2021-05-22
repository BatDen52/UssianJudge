using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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
    [SerializeField] private File _file;

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
        _buttonNext.onClick.RemoveListener(Fetch);
        StartCoroutine(FetchAnimation());
    }

    public IEnumerator FetchAnimation()
    {
        _file.Generate();

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
}
