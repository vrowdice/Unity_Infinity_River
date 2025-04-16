using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacterData
{
    /// <summary>
    /// �̸�
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// ��������Ʈ
    /// </summary>
    public Sprite m_sprite = null;

    /// <summary>
    /// ������ ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// ĳ���� ��ư
    /// </summary>
    public CharacterBtn m_btn = null;

    /// <summary>
    /// ȹ�� ����
    /// </summary>
    public bool m_isGet = false;

    /// <summary>
    /// ����
    /// </summary>
    public long m_price = 0;
}
