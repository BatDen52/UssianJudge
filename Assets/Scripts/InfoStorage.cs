using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InfoStorage : MonoBehaviour
{
    [SerializeField] private List<Sprite> _skins;
    [SerializeField] private List<Sprite> _photos;
    [SerializeField] private Button _buttonNext;

    private List<string> _names;
    private List<string> _surnames;
    private List<string> _cases;
    private List<string> _articles;

    private string _namesFileTitle = "names";
    private string _surnamesFileTitle = "surnames";
    private string _casesFileTitle = "cases";
    private string _articlesFileTitle = "articles";

    private void Awake()
    {
        TextAsset mytxtData = (TextAsset)Resources.Load(_namesFileTitle);
        _names = JsonConvert.DeserializeObject<List<string>>(mytxtData.text);
        mytxtData = (TextAsset)Resources.Load(_surnamesFileTitle);
        _surnames = JsonConvert.DeserializeObject<List<string>>(mytxtData.text);
        mytxtData = (TextAsset)Resources.Load(_casesFileTitle);
        _cases = JsonConvert.DeserializeObject<List<string>>(mytxtData.text);
        mytxtData = (TextAsset)Resources.Load(_articlesFileTitle);
        _articles = JsonConvert.DeserializeObject<List<string>>(mytxtData.text);

        _buttonNext.enabled = true;
    }

    public string GetRandomName() => _names[Random.Range(0, _names.Count)];

    public string GetRandomSurname() => _surnames[Random.Range(0, _surnames.Count)];

    public string GetRandomCase() => _cases[Random.Range(0, _cases.Count)];

    public string GetRandomArticle() => _articles[Random.Range(0, _articles.Count)];

    public string GetRandomBirthday() => $"{Random.Range(1, 29)}.{Random.Range(1, 13)}.{Random.Range(80, 22)}";

    public Sprite GetRandomSkin(out Sprite photo)
    {
        int dudeIndex = Random.Range(0, _skins.Count);
        photo = _photos[dudeIndex];
        return _skins[dudeIndex];
    }
}
