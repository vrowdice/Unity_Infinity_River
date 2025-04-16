using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    /// <summary>
    /// 코드
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// 지금 상태
    /// </summary>
    public ItemType.itemUseType m_itemType = new ItemType.itemUseType();

    private void OnTriggerEnter(Collider argCol)
    {
        if (argCol.gameObject.layer == 6)
        {
            if (m_itemType == ItemType.itemUseType.Material)
            {
                GManager.Instance.ManageRaft((int)transform.position.x, (int)transform.position.z).AddMatStack(m_code);
                Destroy(gameObject);
                return;
            }

            switch (m_code)
            {
                case 30000:
                    GManager.Instance.ManageRaftHp((int)transform.position.x, (int)transform.position.z, -1);
                    Destroy(gameObject);
                    break;
                case 30001:
                    GManager.Instance.ManageRaftHp((int)transform.position.x, (int)transform.position.z, -1);
                    GManager.Instance.ManageRaftHp((int)transform.position.x - 1, (int)transform.position.z, -1);
                    GManager.Instance.ManageRaftHp((int)transform.position.x, (int)transform.position.z - 1, -1);
                    GManager.Instance.ManageRaftHp((int)transform.position.x + 1, (int)transform.position.z, -1);
                    GManager.Instance.ManageRaftHp((int)transform.position.x, (int)transform.position.z + 1, -1);
                    Destroy(gameObject);
                    break;
            }
        }
        else if (argCol.gameObject.layer == 8 || argCol.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider argCol)
    {
        if (argCol.gameObject.layer == 10)
        {
            if (m_code == 30002)
            {
                argCol.transform.Translate(new Vector3(0.0f, 0.0f, -GManager.Instance.IsGameSpeed) * Time.deltaTime);
            }
        }
    }
}
