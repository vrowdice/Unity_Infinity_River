using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// 캐릭터
    /// </summary>
    public List<CharacterData> m_character = new List<CharacterData>();

    /// <summary>
    /// 최대 저장할 스코어 정보
    /// </summary>
    public int m_maxSaveScore = 0;

    /// <summary>
    /// 리셋 하면 true
    /// </summary>
    public bool m_saveReset = false;

    /// <summary>
    /// 저장할 경로
    /// </summary>
    [TextArea]
    public string m_path = String.Empty;

    /// <summary>
    /// 세이브 중 플래그
    /// </summary>
    bool m_saveFlag = false;

    /// <summary>
    /// 캐릭터 코드
    /// </summary>
    int m_characterCode = 0;

    /// <summary>
    /// 플레이어 돈
    /// </summary>
    long m_money = 0;

    /// <summary>
    /// 스코어 문자열 리스트
    /// </summary>
    List<ScoreData> m_scoreData = new List<ScoreData>();

    /// <summary>
    /// 캐릭터 딕셔너리
    /// </summary>
    Dictionary<int, AllCharacterData> m_characterDic = new Dictionary<int, AllCharacterData>();

    /// <summary>
    /// 인스턴스
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
    /// 첫 셋팅
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
    /// 셋팅
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
    /// 기본값
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
    /// 아이템 관리 데이터 받아오기
    /// </summary>
    /// <param name="argCode">아이템 코드</param>
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
    /// 업데이트 스코어
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
    /// 스코어 추가
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
    /// 상점 업데이트
    /// </summary>
    public void SetStor()
    {
        foreach(KeyValuePair<int, AllCharacterData> item in m_characterDic)
        {
            GManager.Instance.m_uiManager.AddCharacterBtn(item.Key, item.Value);
        }
    }

    /// <summary>
    /// 세이브 리셋
    /// </summary>
    public void ResetSave()
    {
        DefultValue();

        Save();
        Load();
    }

    /// <summary>
    /// 세이브
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
    /// 로드
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
    /// 세이브
    /// </summary>
    /// <param name="argPath">저장할 위치</param>
    /// <param name="argData">저장할 데이터</param>
    public void WriteSave(string argData)
    {
        string _path = Application.persistentDataPath + "/" + m_path + ".json";

        StreamWriter _sw = new StreamWriter(_path, false, System.Text.Encoding.UTF8);
        _sw.WriteLine(argData);
        _sw.Close();
    }

    /// <summary>
    /// 로드
    /// </summary>
    /// <param name="argPath">경로</param>
    /// <returns>불러올 데이터</returns>
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
    /// 돈 변경
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
    /// 인스턴스
    /// </summary>
    public static GameDataManager Instance
    {
        get
        {
            return g_gameDataManager;
        }
    }
}
