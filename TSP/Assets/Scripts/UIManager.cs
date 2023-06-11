using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if( _instance == null)
            {
                throw new Exception("UIManager is null");
            }
            else
            {
                return _instance;
            }
        }
    }

    private TextMeshProUGUI _titleText;
    private TextMeshProUGUI _descriptionText;

    private void Awake()
    {
        if (_instance != null) Destroy(this.gameObject);
        else _instance = this;

        _titleText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void SetInfo(string info)
    {
        _descriptionText.text = info;
    }
}
