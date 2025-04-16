using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    /// <summary>
    /// 각 씬의 캔버스
    /// </summary>
    public GameObject m_canvas = null;

    /// <summary>
    /// 로딩 패널
    /// </summary>
    public GameObject m_loadingPanel = null;

    /// <summary>
    /// 로딩 슬라이더
    /// </summary>
    public Slider m_slider = null;

    /// <summary>
    /// 로딩 텍스트
    /// </summary>
    public Text m_progressText = null;

    /// <summary>
    /// static
    /// </summary>
    static SManager g_sManager = null;

    /// <summary>
    /// awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            g_sManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        FindObj();

        SceneManager.sceneLoaded += LoadSceneEvent;
        Screen.SetResolution(1920, 1080, true);
    }

    /// <summary>
    /// 오브젝트 찾기
    /// </summary>
    void FindObj()
    {
        m_canvas = GameObject.Find("Canvas");
        m_loadingPanel = m_canvas.transform.Find("LoadingPanel").gameObject;
        m_slider = m_loadingPanel.transform.Find("LoadingBar").GetComponent<Slider>();
        m_progressText = m_slider.transform.Find("ProgressText").GetComponent<Text>();
    }

    /// <summary>
    /// 씬이 로딩됬을 때
    /// </summary>
    /// <param name="scene">씬</param>
    /// <param name="mode">모드</param>
    void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            LoadScene("Main");
        }
        FindObj();
        if (SceneManager.GetActiveScene().name == "Main")
        {
            
        }
    }

    /// <summary>
    /// 메인이면 true 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool CheckMain()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            return true;
        }
        return false;
    }

    //씬 이동 함수
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchrounously(sceneName));
    }

    IEnumerator LoadAsynchrounously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        m_loadingPanel.SetActive(true);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            m_slider.value = progress;
            m_progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public static SManager Instance
    {
        get
        {
            return g_sManager;
        }
    }
}
