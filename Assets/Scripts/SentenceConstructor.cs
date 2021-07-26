using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentenceConstructor : MonoBehaviour
{
    [SerializeField] private TMP_Text _procurorText;
    [SerializeField] private TMP_Text _passportInfo;
    [SerializeField] private TMP_Text _codexText;
    [SerializeField] private TMP_Text[] _toggles;
    [SerializeField] private TextSizeController _textController;
    [SerializeField] private Button _buttonOk;
    [SerializeField] private NextDudeFetcher _dudeNext;

    private GameObject _movableItem;
    private Transform _startPosition;
    private MovableItemTypes _types;

    private bool _ptIsUse;
    private bool _piIsUse;
    private bool _ctIsUse;

    private bool _forPeople;

    public int CountOfSentence { get; set; }

    enum MovableItemTypes
    {
        ProcurorText,
        PassportInfo,
        CodexText
    }

    private void Start()
    {
        CountOfSentence = 0;
    }

    private void FixedUpdate()
    {
     }

    public void Drop(GameObject text)
    {
        switch (_types)
        {
            case MovableItemTypes.ProcurorText:
                _procurorText.text = "";
                foreach (var item in _movableItem.GetComponentsInChildren<TMP_Text>())
                {
                    _procurorText.text += item.text;
                    _ptIsUse = true;
                }
                break;
            case MovableItemTypes.PassportInfo:
                _passportInfo.text = " ";
                foreach (var item in _movableItem.GetComponentsInChildren<TMP_Text>())
                {
                    _passportInfo.text += item.text + " ";
                    _piIsUse = true;
                }
                break;
            case MovableItemTypes.CodexText:
                text.TryGetComponent<ArticlePage>(out ArticlePage ap);
                if (ap.Article.Panishment != null && ap.Article.Panishment.Count > 0)
                {
                    _forPeople = ap.Article.Number == "Статья 2 АУК АР";

                    _codexText.text = ap.Article.Number;

                    for (int j = 0; j < _toggles.Length; j++)
                    {
                        _toggles[j].transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
                        _toggles[j].gameObject.transform.parent.gameObject.SetActive(false);
                    }

                    int i = 0;

                    foreach (var item in ap.Article.Panishment.Keys)
                    {
                        _toggles[i].gameObject.transform.parent.gameObject.SetActive(true);
                        _toggles[i++].text = item;
                    }
                    _ctIsUse = true;
                }
                break;
        }
        _textController.TextSizeChange(new TMP_Text[] { _passportInfo, _procurorText, _codexText, _toggles[0] });

        if (_movableItem != null)
            _movableItem.transform.position = _startPosition.position;
        _movableItem = null;
    }

    public void CheckToggle()
    {
        if (_ctIsUse && _piIsUse && _ptIsUse && (_toggles[0].transform.parent.gameObject.GetComponent<Toggle>().isOn || _toggles[1].transform.parent.gameObject.GetComponent<Toggle>().isOn))
        {
            _buttonOk.enabled = true;
            _buttonOk.onClick.AddListener(PanishmentOk);
        }
    }

    public void PanishmentOk()
    {
        _buttonOk.onClick.RemoveListener(PanishmentOk);

        if (_forPeople)
            PlayerPrefs.SetInt("Results", PlayerPrefs.GetInt("Results") + 1);
        else
            PlayerPrefs.SetInt("Results", PlayerPrefs.GetInt("Results") - 1);

        Debug.Log($"Results {PlayerPrefs.GetInt("Results")}");

        _buttonOk.enabled = false;
        _ctIsUse = _piIsUse = _ptIsUse = false;
        _procurorText.text = "Обвинение";
        _passportInfo.text = "ФИО";
        _codexText.text = "Статья";
        _toggles[0].text = "Мера пресечения";
        _toggles[1].gameObject.transform.parent.gameObject.SetActive(false);
        _dudeNext.TakeAway(CountOfSentence++);
    }

    public void Drag()
    {
        // _movableItem.transform.Translate(Input.mousePosition);
    }

    public void StartDrag(GameObject text)
    {
        if (text.TryGetComponent<ProcurorText>(out ProcurorText pt))
            _types = MovableItemTypes.ProcurorText;
        else if (text.TryGetComponent<PassportText>(out PassportText pass))
            _types = MovableItemTypes.PassportInfo;
        else if (text.TryGetComponent<ArticlePage>(out ArticlePage ap))
            _types = MovableItemTypes.CodexText;
         
        _startPosition = text.transform;
        _movableItem = text.gameObject;
    }
}
