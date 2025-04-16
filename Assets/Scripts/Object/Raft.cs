using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
    /// <summary>
    /// �� �������ڵ�
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// �¸� ü��
    /// </summary>
    public int m_hp = 0;

    /// <summary>
    /// ��� ����
    /// </summary>
    public List<int> m_matList = null;

    /// <summary>
    /// ���̴� ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// ���� ������Ʈ�� �Ž� ������
    /// </summary>
    public MeshRenderer m_meshRenderer = null;

    /// <summary>
    /// �¸� ���̴� ����
    /// </summary>
    public GameObject m_viewMat = null;

    /// <summary>
    /// ó�� ����
    /// </summary>
    bool m_firstAdd = false;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ResetMatStack()
    {
        if(m_matList != null)
        {
            m_matList.Clear();
            m_matList = null;
        }

        m_firstAdd = false;

        for (int i = 0; i < m_viewMat.transform.childCount; i++)
        {
            Destroy(m_viewMat.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// ��� ���� �߰�
    /// </summary>
    /// <param name="argCode">��� �ڵ�</param>
    public void AddMatStack(int argCode)
    {
        if (!m_firstAdd)
        {
            m_matList = new List<int>();
            m_firstAdd = true;
        }

        m_matList.Add(argCode);

        if(m_matList.Count <= 5)
        {
            GameObject _viewObj = Instantiate(GManager.Instance.ManageItem(argCode).m_viewObj);

            _viewObj.transform.SetParent(m_viewMat.transform);
            _viewObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _viewObj.transform.localPosition = new Vector3(0.0f, 0.2f, 0.6f - (m_matList.Count * 0.2f));
            _viewObj.transform.localRotation = Quaternion.Euler(new Vector3(-45.0f, 0.0f, 0.0f));
        }
    }
}
