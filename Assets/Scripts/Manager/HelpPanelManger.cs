using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelManger : MonoBehaviour
{


    /// <summary>
    /// �̹��� ������Ʈ Ȯ��
    /// </summary>
    public GameObject m_Img1 = null;
    public GameObject m_Img2 = null;
    public GameObject m_Img3 = null;
    public GameObject m_Img4= null;
    public GameObject m_Img5 = null;
    public GameObject m_Img6 = null;
    public GameObject m_Img7 = null;

    /// <summary>
    /// �г� ������Ʈ
    /// </summary>
    public GameObject HelpPanel = null;

    /// <summary>
    /// �̹��� ������Ʈ Ȯ��
    /// </summary>
    public GameObject HelpPanelText = null;

    /// <summary>
    /// next(����) ��ư Ŭ��
    /// </summary>
    public void ClickNextBtn()
    {
        if(m_Img1.activeSelf == true)
        {
            m_Img1.SetActive(false);
            m_Img2.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
            
        }

        else if (m_Img2.activeSelf == true)
        {
            m_Img2.SetActive(false);
            m_Img3.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img3.activeSelf == true)
        {
            m_Img3.SetActive(false);
            m_Img4.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img4.activeSelf == true)
        {
            m_Img4.SetActive(false);
            m_Img5.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img5.activeSelf == true)
        {
            m_Img5.SetActive(false);
            m_Img6.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if(m_Img6.activeSelf == true)
        {
            m_Img6.SetActive(false);
            m_Img7.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }
        else if(m_Img7.activeSelf == true)
        {

        }
    }

    /// <summary>
    /// previous(����) ��ư Ŭ��
    /// </summary>
    public void ClickPreviousBtn()
    {
        if (m_Img7.activeSelf == true)
        {
            m_Img7.SetActive(false);
            m_Img6.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";

        }

        else if (m_Img6.activeSelf == true)
        {
            m_Img6.SetActive(false);
            m_Img5.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img5.activeSelf == true)
        {
            m_Img5.SetActive(false);
            m_Img4.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img4.activeSelf == true)
        {
            m_Img4.SetActive(false);
            m_Img3.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img3.activeSelf == true)
        {
            m_Img3.SetActive(false);
            m_Img2.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }

        else if (m_Img2.activeSelf == true)
        {
            m_Img2.SetActive(false);
            m_Img1.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "�ʹ����� �谡 ���Ŀ�.";
        }
        else if(m_Img1.activeSelf == true)
        {

        }
    }

    /// <summary>
    /// next��ư Ŭ��
    /// </summary>
    public void ClickMainMenBtn()
    {
        HelpPanel.SetActive(false);
    }
}
