using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour
{
    [SerializeField] private Passport _pass;
    [SerializeField] private SpriteRenderer _skin;

    private string _name;
    private string _surname;
    private string _birthday;
    private Sprite _photo;

    public void Init(string name, string surname, string birthday, Sprite skin, Sprite photo)
    {
        _name = name;
        _surname = surname;
        _birthday = birthday;
        _photo = photo;

        _pass.Init(_name, _surname, _birthday, _photo);
        _skin.sprite = skin;
    }
}
