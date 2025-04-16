using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSky : MonoBehaviour
{
    /// <summary>
    /// 스카이 박스 속도
    /// </summary>
    public float m_skyBoxSpeed = 0.0f;

    /// <summary>
    /// 스카이박스 회전
    /// </summary>
    float m_boxRot = 0.0f;

    // Update is called once per frame
    void Update()
    {
        IsSkyRot += m_skyBoxSpeed * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", m_boxRot);
    }

    float IsSkyRot
    {
        get
        {
            return m_boxRot;
        }
        set
        {
            m_boxRot += value;
            if(m_boxRot >= 720.0f)
            {
                m_boxRot = 0.0f;
            }
        }
    }
}
