using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelManger : MonoBehaviour
{


    /// <summary>
    /// 이미지 오브젝트 확인
    /// </summary>
    public GameObject m_Img1 = null;
    public GameObject m_Img2 = null;
    public GameObject m_Img3 = null;
    public GameObject m_Img4= null;
    public GameObject m_Img5 = null;
    public GameObject m_Img6 = null;
    public GameObject m_Img7 = null;

    /// <summary>
    /// 패널 오브젝트
    /// </summary>
    public GameObject HelpPanel = null;

    /// <summary>
    /// 이미지 오브젝트 확인
    /// </summary>
    public GameObject HelpPanelText = null;

    /// <summary>
    /// next(다음) 버튼 클릭
    /// </summary>
    public void ClickNextBtn()
    {
        if(m_Img1.activeSelf == true)
        {
            m_Img1.SetActive(false);
            m_Img2.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
            
        }

        else if (m_Img2.activeSelf == true)
        {
            m_Img2.SetActive(false);
            m_Img3.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img3.activeSelf == true)
        {
            m_Img3.SetActive(false);
            m_Img4.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img4.activeSelf == true)
        {
            m_Img4.SetActive(false);
            m_Img5.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img5.activeSelf == true)
        {
            m_Img5.SetActive(false);
            m_Img6.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if(m_Img6.activeSelf == true)
        {
            m_Img6.SetActive(false);
            m_Img7.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }
        else if(m_Img7.activeSelf == true)
        {

        }
    }

    /// <summary>
    /// previous(이전) 버튼 클릭
    /// </summary>
    public void ClickPreviousBtn()
    {
        if (m_Img7.activeSelf == true)
        {
            m_Img7.SetActive(false);
            m_Img6.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";

        }

        else if (m_Img6.activeSelf == true)
        {
            m_Img6.SetActive(false);
            m_Img5.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img5.activeSelf == true)
        {
            m_Img5.SetActive(false);
            m_Img4.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img4.activeSelf == true)
        {
            m_Img4.SetActive(false);
            m_Img3.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img3.activeSelf == true)
        {
            m_Img3.SetActive(false);
            m_Img2.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }

        else if (m_Img2.activeSelf == true)
        {
            m_Img2.SetActive(false);
            m_Img1.SetActive(true);
            HelpPanelText.GetComponent<Text>().text = "너무나도 배가 고파요.";
        }
        else if(m_Img1.activeSelf == true)
        {

        }
    }

    /// <summary>
    /// next버튼 클릭
    /// </summary>
    public void ClickMainMenBtn()
    {
        HelpPanel.SetActive(false);
    }
}
