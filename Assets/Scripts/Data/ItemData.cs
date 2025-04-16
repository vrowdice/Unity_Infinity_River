using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "New Item Data", order = 1)]
public class ItemData : ScriptableObject
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
    /// ������ ��� Ÿ��
    /// </summary>
    public ItemType.itemUseType m_itemType = new ItemType.itemUseType();

    /// <summary>
    /// ���̴� ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// UI�� ǥ�õ� ���� ��������Ʈ
    /// </summary>
    public Sprite m_sampleSprite = null;

    /// <summary>
    /// �ʿ��� ��� �ڵ�
    /// </summary>
    public int m_needMatCode = 0;

    /// <summary>
    /// �ʿ��� ��� ����
    /// </summary>
    public int m_needMatAmount = 0;
}
