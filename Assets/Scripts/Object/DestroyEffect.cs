using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    /// <summary>
    /// ��ƼŬ �ý���
    /// </summary>
    public ParticleSystem m_effect = null;

    public void PlayEffect()
    {
        m_effect.time = 0.0f;
        m_effect.Play();
        Invoke("Stack", 1.0f);
    }

    void Stack()
    {
        GManager.Instance.m_effect.Add(this);
    }
}
