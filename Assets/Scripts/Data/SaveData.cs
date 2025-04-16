using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// 돈
    /// </summary>
    public long m_money = 0;

    /// <summary>
    /// 선택한 캐릭터
    /// </summary>
    public int m_selectCharacterCode = 0;

    /// <summary>
    /// 점수 날짜
    /// </summary>
    public List<string> m_scoreDate = new List<string>();

    /// <summary>
    /// 점수
    /// </summary>
    public List<long> m_score = new List<long>();

    /// <summary>
    /// 캐릭터 코드
    /// </summary>
    public List<int> m_characterCode = new List<int>();

    /// <summary>
    /// 캐릭터 획득 여부
    /// </summary>
    public List<bool> m_isCharacter = new List<bool>();

    /// <summary>
    /// 생성자
    /// </summary>
    public SaveData()
    {

    }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="argData"></param>
    public SaveData(string argData)
    {
        SaveData _data = JsonUtility.FromJson<SaveData>(argData);

        m_money = _data.m_money;
        m_selectCharacterCode = _data.m_selectCharacterCode;
        m_scoreDate = _data.m_scoreDate;
        m_score = _data.m_score;
        m_characterCode = _data.m_characterCode;
        m_isCharacter = _data.m_isCharacter;
    }

    /// <summary>
    /// Json 문자열 데이터 변환
    /// </summary>
    /// <returns>문자열 데이터</returns>
    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }
}
