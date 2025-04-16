using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public float m_finalPosZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z <= m_finalPosZ)
        {
            GManager.Instance.m_objManager.DestroyMap();
        }

        MoveController();
    }

    /// <summary>
    /// ������
    /// </summary>
    void MoveController()
    {
        transform.Translate(new Vector3(0.0f, 0.0f, -GManager.Instance.IsGameSpeed) * Time.deltaTime);
    }
}
