using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacterData
{
    /// <summary>
    /// 이름
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// 스프라이트
    /// </summary>
    public Sprite m_sprite = null;

    /// <summary>
    /// 보여질 오브젝트
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// 캐릭터 버튼
    /// </summary>
    public CharacterBtn m_btn = null;

    /// <summary>
    /// 획득 여부
    /// </summary>
    public bool m_isGet = false;

    /// <summary>
    /// 가격
    /// </summary>
    public long m_price = 0;
}
