using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider argCol)
    {
        if (argCol.gameObject.layer == 8)
        {
            GManager.Instance.ManageItem(argCol.GetComponent<Float>().m_code, 1);
            Destroy(argCol.gameObject);
            GManager.Instance.m_soundManager.m_getSound.Play();
        }

        if (argCol.gameObject.layer == 6)
        {
            Raft _raft = argCol.gameObject.GetComponent<Raft>();

            if(_raft.m_matList != null)
            {
                for (int i = 0; i < _raft.m_matList.Count; i++)
                {
                    GManager.Instance.ManageItem(_raft.m_matList[i], 1);
                }
                _raft.ResetMatStack();
                GManager.Instance.m_soundManager.m_getSound.Play();
            }
        }

        if(argCol.gameObject.layer == 11)
        {
            GManager.Instance.IsGameGold += 100;
            Destroy(argCol.gameObject);
            GManager.Instance.m_soundManager.m_coinGetSound.Play();
        }
    }
}
