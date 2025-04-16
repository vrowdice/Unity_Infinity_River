using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "New Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    /// <summary>
    /// 캐릭터 코드
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// 가격
    /// </summary>
    public long m_price = 0;

    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// 캐릭터 아이콘 이미지
    /// </summary>
    public Sprite m_sprite = null;

    /// <summary>
    /// 보이는 오브젝트
    /// </summary>
    public GameObject m_viewObj = null;
}
