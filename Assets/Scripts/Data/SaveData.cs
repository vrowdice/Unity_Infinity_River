using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// ��
    /// </summary>
    public long m_money = 0;

    /// <summary>
    /// ������ ĳ����
    /// </summary>
    public int m_selectCharacterCode = 0;

    /// <summary>
    /// ���� ��¥
    /// </summary>
    public List<string> m_scoreDate = new List<string>();

    /// <summary>
    /// ����
    /// </summary>
    public List<long> m_score = new List<long>();

    /// <summary>
    /// ĳ���� �ڵ�
    /// </summary>
    public List<int> m_characterCode = new List<int>();

    /// <summary>
    /// ĳ���� ȹ�� ����
    /// </summary>
    public List<bool> m_isCharacter = new List<bool>();

    /// <summary>
    /// ������
    /// </summary>
    public SaveData()
    {

    }

    /// <summary>
    /// ������
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
    /// Json ���ڿ� ������ ��ȯ
    /// </summary>
    /// <returns>���ڿ� ������</returns>
    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }
}
