using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodexController : MonoBehaviour
{
    [SerializeField] private TMP_Text _leftPage;
    [SerializeField] private InfoStorage _storage;
    [SerializeField] private TextSizeController _textController;
    [SerializeField] private TMP_Text _rightPage;

    private ArticlePage _leftPageArticle;
    private ArticlePage _rightPageArticle;

    private int _index;

    private void Start()
    {
        _leftPageArticle = _leftPage.GetComponent<ArticlePage>();
        _rightPageArticle = _rightPage.GetComponent<ArticlePage>();

        for (int i = 0; i < _storage.ArticlesCount; i += 2)
        {
            PrintPagesByIndex(i, i + 1);
            _textController.TextSizeChange(new TMP_Text[] { _leftPage, _rightPage });
        }
        _index = -1;
        NextPage();
    }

    public void NextPage()
    {
        if (_storage.ArticlesCount > _index + 1)
        {
            _index += 2;
            PrintPagesByIndex(_index - 1, _index);
            //_textController.TextSizeChange(new TMP_Text[] { _leftPage, _rightPage });
        }
    }

    public void PreviousPage()
    {
        if (_index - 1 > 0)
        {
            _index -= 2;
            PrintPagesByIndex(_index - 1, _index);
            //_textController.TextSizeChange(new TMP_Text[] { _leftPage, _rightPage });
        }
    }

    private void PrintPagesByIndex(int leftPageIndex, int rightPageIndex)
    {
        _leftPageArticle.Article = _storage.TryGetArticle(leftPageIndex);
        _leftPage.text = _leftPageArticle.Article?.Text ?? " ";
        _rightPageArticle.Article = _storage.TryGetArticle(rightPageIndex);
        _rightPage.text = _rightPageArticle.Article.Text ?? " ";
    }
}
