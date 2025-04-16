using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotCam : MonoBehaviour
{
    /// <summary>
    /// 따라다닐 것
    /// </summary>
    public PlayerManager m_player = null;

    /// <summary>
    /// y축 회전 보정용
    /// </summary>
    public float m_fixY = 0.0f;

    /// <summary>
    /// 애니메이션
    /// </summary>
    Animation m_animation = null;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    Camera m_mainCamera = null;

    /// <summary>
    /// 마우스 입력 보존
    /// </summary>
    Vector2 m_mouse = Vector2.zero;

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        Setting();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (m_player.m_inputFlag)
        {
            CamLot();
            RdPerson3();
        }
    }

    /// <summary>
    /// 셋팅
    /// </summary>
    void Setting()
    {
        m_mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        m_animation = GetComponent<Animation>();
    }

    /// <summary>
    /// 3인칭
    /// </summary>
    void RdPerson3()
    {
        transform.position = m_player.transform.position;

        if (!m_player.m_buildFlag)
        {
            m_player.transform.localEulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        }

        /*
        if (Input.touchCount == 1)
        {
            Touch _touch = Input.GetTouch(0);
            if (_touch.position.y <= 450.0f && _touch.position.x <= 500.0f)
            {
                return;
            }
            else
            {
                CamLot();
            }
        }
        else
        {
            Touch _touch = Input.GetTouch(1);
            if (_touch.position.y <= 450.0f && _touch.position.x <= 500.0f)
            {
                return;
            }
            else
            {
                CamLot();
            }
        }
        */
    }

    /// <summary>
    /// 카메라 로테이션
    /// </summary>
    void CamLot()
    {
        m_mouse.y += Input.GetAxisRaw("Mouse X");
        m_mouse.x -= Input.GetAxisRaw("Mouse Y");

        if (Mathf.Abs(m_mouse.x) > m_fixY)
        {
            if (m_mouse.x > 0.0f)
            {
                m_mouse.x = m_fixY;
            }
            else
            {
                m_mouse.x = -m_fixY;
            }
        }

        transform.localRotation = Quaternion.Euler(m_mouse.x * m_player.m_sensitivity, m_mouse.y * m_player.m_sensitivity, 0.0f);
    }
}
