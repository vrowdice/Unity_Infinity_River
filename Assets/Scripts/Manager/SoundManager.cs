using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Sound")]
    /// <summary>
    /// �������
    /// </summary>
    public AudioSource m_backgroundSound = null;

    [Header("Effect Sound")]
    /// <summary>
    /// �ͻ� ����
    /// </summary>
    public AudioSource m_drownSound = null;

    /// <summary>
    /// �ı� ����
    /// </summary>
    public AudioSource m_destroySound = null;

    /// <summary>
    /// ȹ�� ����
    /// </summary>
    public AudioSource m_getSound = null;

    /// <summary>
    /// ���� ȹ�� ����
    /// </summary>
    public AudioSource m_coinGetSound = null;

    /// <summary>
    /// ���� ����
    /// </summary>
    public AudioSource m_buildSound = null;

    /// <summary>
    /// ���� ����
    /// </summary>
    public AudioSource m_woodSound = null;

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        m_backgroundSound.volume = 0.5f;
        m_drownSound.volume = 0.5f;
        m_destroySound.volume = 0.5f;
        m_getSound.volume = 0.5f;
        m_coinGetSound.volume = 0.5f;
        m_buildSound.volume = 0.5f;
        m_woodSound.volume = 0.5f;
    }
}
