using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBtn : MonoBehaviour
{
    /// <summary>
    /// ĳ���� �ڵ�
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// ��� �г�
    /// </summary>
    GameObject m_useImage = null;

    /// <summary>
    /// Ŭ�� ��
    /// </summary>
    public void Click()
    {
        GManager.Instance.m_uiManager.SelectCharacter(m_code);
    }

    /// <summary>
    /// ĳ���� ���
    /// </summary>
    public void UseChar()
    {
        m_useImage = Instantiate(GManager.Instance.m_uiManager.m_usingPanel, transform);
    }

    /// <summary>
    /// ��� ���� ĳ����
    /// </summary>
    public void DisuseChar()
    {
        Destroy(m_useImage);
        m_useImage = null;
    }
}
