using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRot : MonoBehaviour
{
    /// <summary>
    /// 최대값
    /// </summary>
    float m_max = 180.0f;

    /// <summary>
    /// 회전 값
    /// </summary>
    Vector3 m_rot = new Vector3(0.0f, 180.0f);

    /// <summary>
    /// 속도
    /// </summary>
    float m_speed = 22.5f;

    /// <summary>
    /// 방향 플래그
    /// </summary>
    bool m_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_flag)
        {
            m_rot.y += m_speed * 3.0f * Time.deltaTime;
            if (m_rot.y > m_max) m_flag = true;
        }
        else
        {
            m_rot.y -= m_speed * 3.0f * Time.deltaTime;
            if (m_rot.y < 0.0f) m_flag = false;
        }

        transform.rotation = Quaternion.Euler(m_rot);
    }
}
