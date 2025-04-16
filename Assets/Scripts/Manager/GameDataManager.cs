using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// ĳ����
    /// </summary>
    public List<CharacterData> m_character = new List<CharacterData>();

    /// <summary>
    /// �ִ� ������ ���ھ� ����
    /// </summary>
    public int m_maxSaveScore = 0;

    /// <summary>
    /// ���� �ϸ� true
    /// </summary>
    public bool m_saveReset = false;

    /// <summary>
    /// ������ ���
    /// </summary>
    [TextArea]
    public string m_path = String.Empty;

    /// <summary>
    /// ���̺� �� �÷���
    /// </summary>
    bool m_saveFlag = false;

    /// <summary>
    /// ĳ���� �ڵ�
    /// </summary>
    int m_characterCode = 0;

    /// <summary>
    /// �÷��̾� ��
    /// </summary>
    long m_money = 0;

    /// <summary>
    /// ���ھ� ���ڿ� ����Ʈ
    /// </summary>
    List<ScoreData> m_scoreData = new List<ScoreData>();

    /// <summary>
    /// ĳ���� ��ųʸ�
    /// </summary>
    Dictionary<int, AllCharacterData> m_characterDic = new Dictionary<int, AllCharacterData>();

    /// <summary>
    /// �ν��Ͻ�
    /// </summary>
    static GameDataManager g_gameDataManager = null;

    private void Awake()
    {
        FirstSetting();
        Load();
    }

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        Setting();
    }

    /// <summary>
    /// ù ����
    /// </summary>
    void FirstSetting()
    {
        if (g_gameDataManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            g_gameDataManager = this;
            DontDestroyOnLoad(gameObject);
        }

        for(int i = 0; i < m_character.Count; i++)
        {
            AllCharacterData _data = new AllCharacterData();
            _data.m_name = m_character[i].m_name;
            _data.m_sprite = m_character[i].m_sprite;
            _data.m_viewObj = m_character[i].m_viewObj;
            _data.m_price = m_character[i].m_price;
            _data.m_isGet = false;

            m_characterDic.Add(m_character[i].m_code, _data);
        }

        m_character.Clear();
        m_character = null;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Setting()
    {
        SetStor();
        UpdateScore();

        m_characterDic[0].m_isGet = true;

        if (m_saveReset)
        {
            ResetSave();
        }
        else
        {
            Save();
        }
    }

    /// <summary>
    /// �⺻��
    /// </summary>
    void DefultValue()
    {
        foreach (KeyValuePair<int, AllCharacterData> item in m_characterDic)
        {
            item.Value.m_isGet = false;
        }

        m_scoreData.Clear();

        m_characterDic[0].m_isGet = true;
        m_money = 1000000;
    }

    /// <summary>
    /// ������ ���� ������ �޾ƿ���
    /// </summary>
    /// <param name="argCode">������ �ڵ�</param>
    /// <returns></returns>
    public AllCharacterData ManageCharacter(int argCode)
    {
        AllCharacterData _item;
        if (!m_characterDic.TryGetValue(argCode, out _item))
        {
            return null;
        }

        return _item;
    }

    /// <summary>
    /// ������Ʈ ���ھ�
    /// </summary>
    public void UpdateScore()
    {
        GManager.Instance.m_uiManager.ResetScorePanel();

        for (int i = 0; i < m_scoreData.Count; i++)
        {
            GManager.Instance.m_uiManager.AddSetScorePanel(i + 1, m_scoreData[i].m_date, m_scoreData[i].m_score);
        }
    }

    /// <summary>
    /// ���ھ� �߰�
    /// </summary>
    /// <param name="argScore"></param>
    public void AddScore(long argScore)
    {
        ScoreData _data = new ScoreData();

        _data.m_date = DateTime.Now.ToString();
        _data.m_score = argScore;
        m_scoreData.Add(_data);

        m_scoreData.Sort((a, b) => {
            return b.m_score.CompareTo(a.m_score);
        });

        if (m_maxSaveScore < m_scoreData.Count)
        {
            m_scoreData.RemoveAt(m_maxSaveScore);
        }

        UpdateScore();
        Save();
    }
    
    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    public void SetStor()
    {
        foreach(KeyValuePair<int, AllCharacterData> item in m_characterDic)
        {
            GManager.Instance.m_uiManager.AddCharacterBtn(item.Key, item.Value);
        }
    }

    /// <summary>
    /// ���̺� ����
    /// </summary>
    public void ResetSave()
    {
        DefultValue();

        Save();
        Load();
    }

    /// <summary>
    /// ���̺�
    /// </summary>
    public void Save()
    {
        if (m_saveFlag)
        {
            GManager.Instance.m_uiManager.Warning("Wait Please");
            return;
        }
        m_saveFlag = true;
        SaveData _data = new SaveData();

        _data.m_money = m_money;
        _data.m_selectCharacterCode = m_characterCode;

        foreach(KeyValuePair<int, AllCharacterData> item in m_characterDic)
        {
            _data.m_characterCode.Add(item.Key);
            _data.m_isCharacter.Add(item.Value.m_isGet);
        }

        for(int i = 0; i < m_scoreData.Count; i++)
        {
            _data.m_scoreDate.Add(m_scoreData[i].m_date);
            _data.m_score.Add(m_scoreData[i].m_score);
        }

        WriteSave(_data.ToJsonString());
        m_saveFlag = false;
    }

    /// <summary>
    /// �ε�
    /// </summary>
    public void Load()
    {
        SaveData _data = new SaveData(ReadLoad());

        if (_data == null) ResetSave();

        m_money = _data.m_money;
        m_characterCode = _data.m_selectCharacterCode;

        for (int i = 0; i < _data.m_characterCode.Count; i++)
        {
            m_characterDic[_data.m_characterCode[i]].m_isGet = _data.m_isCharacter[i];
        }

        m_scoreData.Clear();
        for (int i = 0; i < _data.m_scoreDate.Count; i++)
        {
            ScoreData _score = new ScoreData();
            _score.m_date = _data.m_scoreDate[i];
            _score.m_score = _data.m_score[i];
            m_scoreData.Add(_score);
        }
    }

    /// <summary>
    /// ���̺�
    /// </summary>
    /// <param name="argPath">������ ��ġ</param>
    /// <param name="argData">������ ������</param>
    public void WriteSave(string argData)
    {
        string _path = Application.persistentDataPath + "/" + m_path + ".json";

        StreamWriter _sw = new StreamWriter(_path, false, System.Text.Encoding.UTF8);
        _sw.WriteLine(argData);
        _sw.Close();
    }

    /// <summary>
    /// �ε�
    /// </summary>
    /// <param name="argPath">���</param>
    /// <returns>�ҷ��� ������</returns>
    public string ReadLoad()
    {
        string _data = string.Empty;

        string _path = Application.persistentDataPath + "/" + m_path + ".json";

        try
        {
            StreamReader _srT = new StreamReader(_path, System.Text.Encoding.UTF8);
        }
        catch
        {
            ResetSave();
        }

        StreamReader _sr = new StreamReader(_path, System.Text.Encoding.UTF8);
        _data = _sr.ReadToEnd();
        _sr.Close();

        if(_data == null)
        {
            ResetSave();
            return null;
        }

        return _data;
    }

    public int IsCharacter
    {
        get
        {
            return m_characterCode;
        }
        set
        {
            m_characterCode = value;
        }
    }

    /// <summary>
    /// �� ����
    /// </summary>
    public long IsMoney
    {
        get
        {
            return m_money;
        }
        set
        {
            m_money = value;
            if(m_money < 0)
            {
                m_money = 0;
            }

            GManager.Instance.m_uiManager.UpdateMoney(m_money);
        }
    }
    
    /// <summary>
    /// �ν��Ͻ�
    /// </summary>
    public static GameDataManager Instance
    {
        get
        {
            return g_gameDataManager;
        }
    }
}
