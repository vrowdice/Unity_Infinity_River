using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "New Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    /// <summary>
    /// ĳ���� �ڵ�
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// ����
    /// </summary>
    public long m_price = 0;

    /// <summary>
    /// ĳ���� �̸�
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// ĳ���� ������ �̹���
    /// </summary>
    public Sprite m_sprite = null;

    /// <summary>
    /// ���̴� ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;
}
