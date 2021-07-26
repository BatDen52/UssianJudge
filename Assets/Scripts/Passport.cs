using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Passport : MonoBehaviour
{
    [SerializeField] private GameObject _openPass;
    [SerializeField] private GameObject _closePass;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _surname;
    [SerializeField] private TMP_Text _birthday;
    [SerializeField] private SpriteRenderer _photo;
    [SerializeField] private TextSizeController _textController;

    public void Init(string name, string surname, string birthday, Sprite photo)
    {
        _name.text = name;
        _surname.text = surname;
        _birthday.text = birthday;
        _photo.sprite = photo;

        _textController.TextSizeChange(new TMP_Text[] { _name, _surname,_birthday});
    }

    public void Hide()
    {
        _closePass.SetActive(false);
        _openPass.SetActive(false);
    }
    public void Show()
    {
        _closePass.SetActive(true);
    }
}
