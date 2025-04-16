using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour
{
    /// <summary>
    /// 체크된 블럭
    /// </summary>
    public GameObject m_checkBlock = null;

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnTriggerStay(Collider argCol)
    {
        if (m_checkBlock != null)
        {
            return;
        }

        if (argCol.gameObject.layer == 7)
        {
            int _x = (int)argCol.gameObject.transform.position.x;
            int _z = (int)argCol.gameObject.transform.position.z;
            if (GManager.Instance.ManageRaft(_x - 1, _z).m_code == 0 &&
                GManager.Instance.ManageRaft(_x + 1, _z).m_code == 0 &&
                GManager.Instance.ManageRaft(_x, _z - 1).m_code == 0 &&
                GManager.Instance.ManageRaft(_x, _z + 1).m_code == 0) return;

            m_checkBlock = argCol.gameObject;
            GManager.Instance.SetShader((int)argCol.transform.position.x, (int)argCol.transform.position.z, GManager.Instance.m_selectShader, true);
        }
        else if(argCol.gameObject.layer == 6)
        {
            m_checkBlock = argCol.gameObject;
        }
    }

    private void OnTriggerExit(Collider argCol)
    {
        if (argCol.gameObject.layer == 7)
        {
            GManager.Instance.SetShader((int)argCol.transform.position.x, (int)argCol.transform.position.z, false);
            m_checkBlock = null;
        }
        else if (argCol.gameObject.layer == 6)
        {
            m_checkBlock = null;
        }
    }
}
