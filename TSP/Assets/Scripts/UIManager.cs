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

    private TMP_Text _titleText;
    private TMP_Text _descriptionText;
    private TMP_Text _debugLog;

    private void Awake()
    {
        if (_instance != null) Destroy(this.gameObject);
        else _instance = this;

        _titleText = transform.GetChild(0).GetComponent<TMP_Text>();
        _descriptionText = transform.GetChild(1).GetComponent<TMP_Text>();
        _debugLog = transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void SetInfo(string info)
    {
        _descriptionText.text = info;
    }

    public void SetDebug(string text)
    {
        Debug.Log("count");
        _debugLog.text += text;
    }
}
