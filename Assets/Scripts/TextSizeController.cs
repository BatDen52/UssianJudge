using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class TextSizeController : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;

    public void TextSizeChange(TMP_Text[] texts)
    {
        _texts = texts;
        StartCoroutine(TextSizeChange());
    }

    private IEnumerator TextSizeChange()
    {
        for (int i = 0; i < _texts.Length; i++)
            _texts[i].fontSizeMax = 100;

        yield return new WaitWhile(delegate
        {
            return _texts.All(item => item.fontSize == _texts[0].fontSize);
        });
        yield return new WaitUntil(delegate
        {
            float minTextSize = float.MaxValue;

            foreach (var item in _texts)
                if (item.fontSize < minTextSize)
                    minTextSize = item.fontSize;

            foreach (var item in _texts)
                item.fontSizeMax = minTextSize;

            return _texts.All(item => item.fontSize == minTextSize);
        });
    }
}
