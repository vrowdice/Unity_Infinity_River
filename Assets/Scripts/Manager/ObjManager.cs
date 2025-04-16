using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [Header("Common")]
    /// <summary>
    /// �� �ִ� ����
    /// </summary>
    public int m_maxBlock = 0;

    /// <summary>
    /// �� ��ġ �⺻��
    /// </summary>
    public Vector3 m_blockBasePos = new Vector3();

    /// <summary>
    /// �� ����
    /// </summary>
    public int m_blockLength = 0;

    /// <summary>
    /// ��� �ʺ�
    /// </summary>
    public int m_blockWight = 0;

    /// <summary>
    /// �� ��Ұ� ������ �θ�
    /// </summary>
    public GameObject m_mapParent = null;

    [Header("Map")]
    /// <summary>
    /// �� �� ���ӿ�����Ʈ
    /// </summary>
    public GameObject m_mapBlock = null;

    /// <summary>
    /// ������
    /// </summary>
    public float m_finalPosZ = 0.0f;

    /// <summary>
    /// �� �� ����Ʈ
    /// </summary>
    public List<GameObject> m_mapBlockList = new List<GameObject>();

    /// <summary>
    /// ó�� �� ����
    /// </summary>
    bool m_firstMapCreFlag = false;

    [Header("Float")]
    /// <summary>
    /// ��ֹ� ������
    /// </summary>
    public GameObject m_obs = null;

    /// <summary>
    /// ������ �ִ� ������Ʈ
    /// </summary>
    public int m_maxRock = 0;

    /// <summary>
    /// �ִ� ��ź
    /// </summary>
    public int m_maxBoom = 0;

    /// <summary>
    /// �ִ� ��������
    /// </summary>
    public int m_maxStick = 0;

    /// <summary>
    /// �ִ� ö
    /// </summary>
    public int m_maxIron = 0;

    /// <summary>
    /// �ִ� ����
    /// </summary>
    public int m_maxCoin = 0;

    /// <summary>
    /// �� ��ֹ� ����
    /// </summary>
    public int m_rockAmount = 0;

    /// <summary>
    /// ��� ��ֹ� ����
    /// </summary>
    public int m_boomAmount = 0;

    /// <summary>
    /// �������� ��� ����
    /// </summary>
    public int m_stickAmount = 0;

    /// <summary>
    /// ö�� ��� ����
    /// </summary>
    public int m_ironAmount = 0;

    /// <summary>
    /// ������ ����
    /// </summary>
    public int m_logAmount = 0;

    /// <summary>
    /// ���� ����
    /// </summary>
    public int m_coinAmount = 0;

    // Update is called once per frame
    void Update()
    {
        CreateMap();
    }

    /// <summary>
    /// �� ����
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
    /// �� ����
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
    /// �ش� ��ġ�� ���� ��ü ����
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
    /// ���ٴϴ� �� ���� ����
    /// </summary>
    /// <param name="_float">float ��ũ��Ʈ</param>
    public void SetFloat(Float _float, int _code)
    {
        _float.m_code = _code;
        GameObject _viewObj = Instantiate(GManager.Instance.ManageItem(_code).m_viewObj);
        _viewObj.transform.SetParent(_float.transform);
        _viewObj.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// �� ����
    /// </summary>
    /// <param name="argIndex">�� �ε���</param>
    public void DestroyMap()
    {
        Destroy(m_mapBlockList[0]);
        m_mapBlockList.RemoveAt(0);
    }
}
