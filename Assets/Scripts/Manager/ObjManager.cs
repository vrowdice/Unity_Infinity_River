using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [Header("Common")]
    /// <summary>
    /// 블럭 최대 갯수
    /// </summary>
    public int m_maxBlock = 0;

    /// <summary>
    /// 블럭 위치 기본값
    /// </summary>
    public Vector3 m_blockBasePos = new Vector3();

    /// <summary>
    /// 맵 길이
    /// </summary>
    public int m_blockLength = 0;

    /// <summary>
    /// 블록 너비
    /// </summary>
    public int m_blockWight = 0;

    /// <summary>
    /// 맵 요소가 생성될 부모
    /// </summary>
    public GameObject m_mapParent = null;

    [Header("Map")]
    /// <summary>
    /// 맵 블럭 게임오브젝트
    /// </summary>
    public GameObject m_mapBlock = null;

    /// <summary>
    /// 종착점
    /// </summary>
    public float m_finalPosZ = 0.0f;

    /// <summary>
    /// 맵 블럭 리스트
    /// </summary>
    public List<GameObject> m_mapBlockList = new List<GameObject>();

    /// <summary>
    /// 처음 맵 생성
    /// </summary>
    bool m_firstMapCreFlag = false;

    [Header("Float")]
    /// <summary>
    /// 장애물 프리펍
    /// </summary>
    public GameObject m_obs = null;

    /// <summary>
    /// 각각의 최대 오브젝트
    /// </summary>
    public int m_maxRock = 0;

    /// <summary>
    /// 최대 폭탄
    /// </summary>
    public int m_maxBoom = 0;

    /// <summary>
    /// 최대 나뭇가지
    /// </summary>
    public int m_maxStick = 0;

    /// <summary>
    /// 최대 철
    /// </summary>
    public int m_maxIron = 0;

    /// <summary>
    /// 최대 코인
    /// </summary>
    public int m_maxCoin = 0;

    /// <summary>
    /// 돌 장애물 갯수
    /// </summary>
    public int m_rockAmount = 0;

    /// <summary>
    /// 기뢰 장애물 갯수
    /// </summary>
    public int m_boomAmount = 0;

    /// <summary>
    /// 나뭇가지 재료 갯수
    /// </summary>
    public int m_stickAmount = 0;

    /// <summary>
    /// 철판 재료 갯수
    /// </summary>
    public int m_ironAmount = 0;

    /// <summary>
    /// 나무통 갯수
    /// </summary>
    public int m_logAmount = 0;

    /// <summary>
    /// 코인 갯수
    /// </summary>
    public int m_coinAmount = 0;

    // Update is called once per frame
    void Update()
    {
        CreateMap();
    }

    /// <summary>
    /// 맵 생성
    /// </summary>
    void CreateMap()
    {
        if (m_mapBlockList.Count >= m_maxBlock)
        {
            return;
        }

        if (!m_firstMapCreFlag)
        {
            ResetMap();

            m_firstMapCreFlag = true;
        }
        else if (m_mapBlockList.Count <= m_maxBlock)
        {
            GameObject _map = Instantiate(m_mapBlock);
            _map.transform.localPosition = new Vector3(m_blockBasePos.x, m_blockBasePos.y, m_blockBasePos.z + 240);
            m_mapBlockList.Add(_map);
            _map.transform.SetParent(m_mapParent.transform);

            Map _mapMap = _map.GetComponent<Map>();
            _mapMap.m_finalPosZ = m_finalPosZ;

            StartCoroutine(CreateFloat(_map.transform));
        }
    }

    /// <summary>
    /// 맵 리셋
    /// </summary>
    public void ResetMap()
    {
        for (int i = 0; i <= m_maxBlock; i++)
        {
            GameObject _map = Instantiate(m_mapBlock);
            _map.transform.localPosition = new Vector3(m_blockBasePos.x, m_blockBasePos.y, m_blockBasePos.z * i - 240);
            m_mapBlockList.Add(_map);
            _map.transform.SetParent(m_mapParent.transform);

            Map _mapMap = _map.GetComponent<Map>();
            _mapMap.m_finalPosZ = m_finalPosZ;
        }
    }

    /// <summary>
    /// 해당 위치에 수중 물체 생성
    /// </summary>
    IEnumerator CreateFloat(Transform argTrans)
    {
        if(argTrans == null)
        {
            yield return null;
        }

        for(int i = 0; i < m_stickAmount + m_ironAmount + m_rockAmount + m_boomAmount + m_coinAmount; i++)
        {
            int _x = Random.Range(0, m_blockWight + 1);
            int _y = Random.Range(0, m_blockLength + 1);
            
            GameObject _float = Instantiate(m_obs, argTrans);
            _float.transform.localPosition = new Vector3(_x - m_blockWight / 2, 0.0f, _y - m_blockLength / 2);
            Float _floatScript = _float.GetComponent<Float>();

            if (i < m_stickAmount)
            {
                _floatScript.m_itemType = ItemType.itemUseType.Material;
                _float.layer = 8;
                SetFloat(_floatScript, 20000);
                yield return null;
            }
            else if (i < m_stickAmount + m_ironAmount)
            {
                _floatScript.m_itemType = ItemType.itemUseType.Material;
                _float.layer = 8;
                SetFloat(_floatScript, 20001);
                yield return null;
            }
            else if (i < m_stickAmount + m_ironAmount + m_rockAmount)
            {
                _floatScript.m_itemType = ItemType.itemUseType.Obstacle;
                _float.layer = 9;
                SetFloat(_floatScript, 30000);
                yield return null;
            }
            else if(i < m_stickAmount + m_ironAmount + m_rockAmount + m_boomAmount)
            {
                _floatScript.m_itemType = ItemType.itemUseType.Obstacle;
                _float.layer = 9;
                SetFloat(_floatScript, 30001);
                yield return null;
            }
            else if (i < m_stickAmount + m_ironAmount + m_rockAmount + m_boomAmount + m_logAmount)
            {
                _floatScript.m_itemType = ItemType.itemUseType.Obstacle;
                _float.layer = 9;
                SetFloat(_floatScript, 30002);
                yield return null;
            }
            else
            {
                _floatScript.m_itemType = ItemType.itemUseType.Coin;
                _float.layer = 11;
                SetFloat(_floatScript, 40000);
                yield return null;
            }
        }
    }

    /// <summary>
    /// 떠다니는 것 정보 생성
    /// </summary>
    /// <param name="_float">float 스크립트</param>
    public void SetFloat(Float _float, int _code)
    {
        _float.m_code = _code;
        GameObject _viewObj = Instantiate(GManager.Instance.ManageItem(_code).m_viewObj);
        _viewObj.transform.SetParent(_float.transform);
        _viewObj.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 맵 삭제
    /// </summary>
    /// <param name="argIndex">맵 인덱스</param>
    public void DestroyMap()
    {
        Destroy(m_mapBlockList[0]);
        m_mapBlockList.RemoveAt(0);
    }
}
