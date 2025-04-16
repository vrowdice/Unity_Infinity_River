using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBtn : MonoBehaviour
{
    /// <summary>
    /// 캐릭터 코드
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// 사용 패널
    /// </summary>
    GameObject m_useImage = null;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        GManager.Instance.m_uiManager.SelectCharacter(m_code);
    }

    /// <summary>
    /// 캐릭터 사용
    /// </summary>
    public void UseChar()
    {
        m_useImage = Instantiate(GManager.Instance.m_uiManager.m_usingPanel, transform);
    }

    /// <summary>
    /// 사용 해재 캐릭터
    /// </summary>
    public void DisuseChar()
    {
        Destroy(m_useImage);
        m_useImage = null;
    }
}
