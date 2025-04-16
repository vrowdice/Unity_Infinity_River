using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "New Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    /// <summary>
    /// 아이템 코드
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string m_name = string.Empty;

    /// <summary>
    /// 아이템 사용 타입
    /// </summary>
    public ItemType.itemUseType m_itemType = new ItemType.itemUseType();

    /// <summary>
    /// 보이는 오브젝트
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// UI에 표시될 샘플 스프라이트
    /// </summary>
    public Sprite m_sampleSprite = null;

    /// <summary>
    /// 필요한 재료 코드
    /// </summary>
    public int m_needMatCode = 0;

    /// <summary>
    /// 필요한 재료 갯수
    /// </summary>
    public int m_needMatAmount = 0;
}
