using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem
{
    /// <summary>
    /// ������ �ڵ�
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// ������ �̸�
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// ������ ����
    /// </summary>
    public int m_amount = 0;

    /// <summary>
    /// ������ ��� Ÿ��
    /// </summary>
    public ItemType.itemUseType m_itemUseType = new ItemType.itemUseType();

    /// <summary>
    /// ���̴� ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// ���� ��������Ʈ
    /// </summary>
    public Sprite m_sampleSprite = null;

    /// <summary>
    /// �ʿ��� ���
    /// </summary>
    public int m_needMatCode = 0;

    /// <summary>
    /// �ʿ��� ��� ����
    /// </summary>
    public int m_needMatAmount = 0;
}
