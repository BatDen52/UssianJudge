using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private Dude _dude;
    [SerializeField] private TMP_Text _procurorText;
    [SerializeField] private InfoStorage _storage;

    public void Generate()
    {
        string name = _storage.GetRandomName();
        string surname = _storage.GetRandomSurname();
        string birthday = _storage.GetRandomBirthday();
        Sprite skin = _storage.GetRandomSkin(out Sprite photo);
        _dude.Init(name, surname, birthday, skin, photo);

        _procurorText.text = _storage.GetRandomCase().Replace("ИМЯ ФАМИЛИЯ", $"{name} {surname}");
    }
}
