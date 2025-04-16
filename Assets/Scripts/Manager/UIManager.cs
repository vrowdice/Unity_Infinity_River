using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [Header("Prefeb")]
    /// <summary>
    /// 아이템 UI패널 프리펍
    /// </summary>
    public GameObject m_itemUIPanel = null;

    /// <summary>
    /// 스코어 UI패널
    /// </summary>
    public GameObject m_scorePanel = null;

    /// <summary>
    /// 캐릭터 버튼
    /// </summary>
    public GameObject m_characterBtn = null;

    /// <summary>
    /// 사용중을 알리는 패널
    /// </summary>
    public GameObject m_usingPanel = null;

    /// <summary>
    /// 도움말 패널
    /// </summary>
    public GameObject m_helpUiPanel = null;


    [Header("Play")]
    /// <summary>
    /// 플레이 UI패널
    /// </summary>
    public Animator m_playUIAni = null;

    /// <summary>
    /// 아이템 UI 콘텐츠
    /// </summary>
    public Transform m_itemUIContent = null;

    /// <summary>
    /// 게임 골드
    /// </summary>
    public Text m_gameGold = null;

    /// <summary>
    /// 시간 카운트
    /// </summary>
    public Text m_timeText = null;

    /// <summary>
    /// 플레이 카운트
    /// </summary>
    public Text m_countText = null;

    /// <summary>
    /// 건설 슬라이더
    /// </summary>
    public Slider m_buildSlider = null;

    /// <summary>
    /// 체력 UI이미지
    /// </summary>
    public GameObject m_healthImage = null;

    /// <summary>
    /// 체력 콘텐츠
    /// </summary>
    public GameObject m_healthContent = null;

    /// <summary>
    /// 일시정지 패널
    /// </summary>
    public GameObject m_optionPanel = null;

    /// <summary>
    /// 컨트롤 패널
    /// </summary>
    public GameObject m_controllPanel = null;

    [Header("Menu")]
    /// <summary>
    /// 매뉴 UI패널
    /// </summary>
    public Animator m_menuUIAni = null;

    /// <summary>
    /// 스코어 콘텐츠
    /// </summary>
    public Transform m_scoreContent = null;

    /// <summary>
    /// 돈 텍스트
    /// </summary>
    public Text m_moneyText = null;

    /// <summary>
    /// 메뉴 화면 배경패널
    /// </summary>
    public GameObject m_BackGroundPanel = null;

    [Header("Store")]
    /// <summary>
    /// 돈 텍스트
    /// </summary>
    public Text m_storeMoneyText = null;

    /// <summary>
    /// 상점 패널
    /// </summary>
    public Animator m_storeAni = null;

    /// <summary>
    /// 상점 컨텐츠
    /// </summary>
    public Transform m_storeContent = null;

    /// <summary>
    /// 캐릭터 빈 오브젝트
    /// </summary>
    public Transform m_character = null;

    /// <summary>
    /// 캐릭터 패널
    /// </summary>
    public GameObject m_characterPanel = null;

    /// <summary>
    /// 패널의 캐릭터 이름 텍스트
    /// </summary>
    Text m_characterNameTxt = null;

    /// <summary>
    /// 패널의 캐릭터 가격 텍스트
    /// </summary>
    Text m_characterPriceTxt = null;

    /// <summary>
    /// 캐릭터 상호작용 버튼의 텍스트
    /// </summary>
    Text m_characterManageBtnText = null;

    /// <summary>
    /// 현재 캐릭터 코드
    /// </summary>
    int m_nowCharacterCode = 0;

    [Header("Option")]
    /// <summary>
    /// 옵션 버튼
    /// </summary>
    public GameObject m_optionBtn = null;

    /// <summary>
    /// 감도 오브젝트
    /// </summary>
    public GameObject m_sensitivity = null;

    /// <summary>
    /// 감도 오브젝트
    /// </summary>
    public GameObject m_backgroundSound = null;

    /// <summary>
    /// 감도 오브젝트
    /// </summary>
    public GameObject m_effectSound = null;

    /// <summary>
    /// 감도 텍스트
    /// </summary>
    Text m_sensitivityText = null;

    /// <summary>
    /// 감도 슬라이더
    /// </summary>
    Slider m_sensitivitySlider = null;

    /// <summary>
    /// 배경음악 텍스트
    /// </summary>
    Text m_backgroundSoundText = null;

    /// <summary>
    /// 배경음악 슬라이더
    /// </summary>
    Slider m_backgroundSoundSlider = null;

    /// <summary>
    /// 이펙트 소리 텍스트
    /// </summary>
    Text m_effectSoundText = null;

    /// <summary>
    /// 이펙트 소리 슬라이더
    /// </summary>
    Slider m_effectSoundSlider = null;

    [Header("Warning")]
    /// <summary>
    /// 경고 애니매이션
    /// </summary>
    public GameObject m_warningPanel = null;

 

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        Setting();
    }

    /// <summary>
    /// 셋팅
    /// </summary>
    void Setting()
    {
        m_optionBtn.SetActive(true);
        m_playUIAni.gameObject.SetActive(true);
        m_menuUIAni.gameObject.SetActive(true);
        m_storeAni.gameObject.SetActive(true);

        m_menuUIAni.SetBool("Active", true);
        m_playUIAni.SetBool("Active", false);
        m_storeAni.SetBool("Active", false);

        m_characterNameTxt = m_characterPanel.transform.GetChild(0).Find("NameText").GetComponent<Text>();
        m_characterPriceTxt = m_characterPanel.transform.GetChild(0).Find("PriceText").GetComponent<Text>();
        m_characterManageBtnText = m_characterPanel.transform.GetChild(0).Find("ManageBtn").Find("Text").GetComponent<Text>();

        m_sensitivityText = m_sensitivity.transform.Find("ValueText").GetComponent<Text>();
        m_sensitivitySlider = m_sensitivity.transform.Find("Slider").GetComponent<Slider>();

        m_backgroundSoundText = m_backgroundSound.transform.Find("ValueText").GetComponent<Text>();
        m_backgroundSoundSlider = m_backgroundSound.transform.Find("Slider").GetComponent<Slider>();

        m_effectSoundText = m_effectSound.transform.Find("ValueText").GetComponent<Text>();
        m_effectSoundSlider = m_effectSound.transform.Find("Slider").GetComponent<Slider>();

        m_sensitivitySlider.value = GManager.Instance.m_playerManager.m_sensitivity;
        m_backgroundSoundSlider.value = GManager.Instance.m_soundManager.m_backgroundSound.volume * 100;
        m_effectSoundSlider.value = GManager.Instance.m_soundManager.m_buildSound.volume * 100;

        GameDataManager.Instance.IsMoney += 0;

/*#if !UNITY_EDITOR
        m_controllPanel.SetActive(true);
#else
        m_controllPanel.SetActive(false);
#endif*/
    }

    /// <summary>
    /// update
    /// </summary>
    private void Update()
    {
        UpdateStopWatch();
    }

    /// <summary>
    /// 게임시작 버튼 클릭
    /// </summary>
    public void StartBtn()
    {
        GManager.Instance.GameStart();
    }

    /// <summary>
    /// 돈 UI 업데이트
    /// </summary>
    public void UpdateMoney(long argMoney)
    {
        m_moneyText.text = argMoney.ToString();
    }

    /// <summary>
    /// 스코어 패널 리셋
    /// </summary>
    public void ResetScorePanel()
    {
        for (int i = 0; i < m_scoreContent.transform.childCount; i++)
        {
            Destroy(m_scoreContent.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 스코어 패널 추가
    /// </summary>
    /// <param name="argOrder">순서</param>
    /// <param name="argData">시간 정보</param>
    /// <param name="argScore">점수</param>
    public void AddSetScorePanel(int argOrder, string argData, long argScore)
    {
        GameObject _ui = Instantiate(m_scorePanel, m_scoreContent);
        _ui.transform.Find("OrderText").gameObject.GetComponent<Text>().text = argOrder.ToString();
        _ui.transform.Find("TimeText").gameObject.GetComponent<Text>().text = argData;
        _ui.transform.Find("ScroeText").gameObject.GetComponent<Text>().text = argScore.ToString();
        
        _ui.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// 캐릭터 버튼 생성
    /// </summary>
    /// <param name="argCode">코드</param>
    /// <param name="argData">데이터</param>
    public void AddCharacterBtn(int argCode, AllCharacterData argData)
    {
        GameObject _btn = Instantiate(m_characterBtn, m_storeContent);
        _btn.transform.Find("MoneyText").GetComponent<Text>().text = argData.m_price.ToString();
        _btn.transform.Find("CharacterNameText").GetComponent<Text>().text = argData.m_name;

        CharacterBtn _btnS = _btn.GetComponent<CharacterBtn>();
        _btnS.m_code = argCode;

        argData.m_btn = _btnS;
        if (GameDataManager.Instance.IsCharacter == argCode)
        {
            _btnS.UseChar();
        }

        CreateChaacterObj(argCode, _btn.transform, new Vector3(0.0f, -200.0f, -90.0f), new Vector3(130.0f, 130.0f, 130.0f));
    }

    /// <summary>
    /// 보여주는 캐릭터 오브젝트 생성
    /// </summary>
    /// <param name="argCode">코드</param>
    /// <param name="argParent">부모</param>
    GameObject CreateChaacterObj(int argCode, Transform argParent, Vector3 argPosition, Vector3 argScale)
    {
        GameObject _char = Instantiate(GameDataManager.Instance.ManageCharacter(argCode).m_viewObj, argParent);
        _char.layer = 5;
        for (int i = 0; i < _char.transform.childCount; i++)
        {
            _char.transform.GetChild(i).gameObject.layer = 5;
        }
        _char.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f));
        _char.transform.localPosition = argPosition;
        _char.transform.localScale = argScale;

        return _char;
    }

    /// <summary>
    /// 캐릭터 선택
    /// </summary>
    /// <param name="argCode">코드</param>
    public void SelectCharacter(int argCode)
    {
        AllCharacterData _data = GameDataManager.Instance.ManageCharacter(argCode);

        m_nowCharacterCode = argCode;
        for (int i = 0; i < m_character.childCount; i++)
        {
            Destroy(m_character.transform.GetChild(i).gameObject);
        }

        GameObject _char = CreateChaacterObj(argCode, m_character, new Vector3(0.0f, -110.0f, -90.0f), new Vector3(150.0f, 150.0f, 150.0f));
        _char.AddComponent<UIRot>();

        m_characterNameTxt.text = _data.m_name;
        m_characterPriceTxt.text = _data.m_price.ToString();
        if (_data.m_isGet)
        {
            m_characterManageBtnText.text = "Select";
        }
        else
        {
            m_characterManageBtnText.text = "Buy";
        }

        m_characterPanel.SetActive(true);
    }

    /// <summary>
    /// 캐릭터 상호작용
    /// </summary>
    public void ManageBtn()
    {
        if (GameDataManager.Instance.ManageCharacter(m_nowCharacterCode).m_isGet)
        {
            GameDataManager.Instance.ManageCharacter(GameDataManager.Instance.IsCharacter).m_btn.DisuseChar();
            GameDataManager.Instance.ManageCharacter(m_nowCharacterCode).m_btn.UseChar();
            GameDataManager.Instance.IsCharacter = m_nowCharacterCode;

            for (int i = 0; i < m_character.childCount; i++)
            {
                Destroy(m_character.transform.GetChild(i).gameObject);
            }

            m_characterPanel.SetActive(false);
        }
        else
        {
            if(GameDataManager.Instance.IsMoney < GameDataManager.Instance.ManageCharacter(m_nowCharacterCode).m_price)
            {
                Warning("Not Enough Money!");
                return;
            }
            else
            {
                GameDataManager.Instance.IsMoney -= GameDataManager.Instance.ManageCharacter(m_nowCharacterCode).m_price;
                m_storeMoneyText.text = GameDataManager.Instance.IsMoney.ToString();
            }

            GameDataManager.Instance.ManageCharacter(m_nowCharacterCode).m_isGet = true;
            SelectCharacter(m_nowCharacterCode);
        }

        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 게임 골드 업데이트
    /// </summary>
    public void UpdateGameGold(long argGold)
    {
        m_gameGold.text = string.Format(argGold.ToString());
    }

    /// <summary>
    /// 스톱워치 UI 업데이트
    /// </summary>
    public void UpdateStopWatch()
    {
        m_timeText.text = string.Format(GManager.Instance.IsStopWatch.ElapsedMilliseconds.ToString());
    }

    /// <summary>
    /// 체력 UI 업데이트
    /// </summary>
    public void UpdateHealth()
    {
        for (int i = 0; i < m_healthContent.transform.childCount; i++)
        {
            Destroy(m_healthContent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GManager.Instance.m_playerManager.IsPlayerHealth; i++)
        {
            GameObject _obj = Instantiate(m_healthImage, m_healthContent.transform);
            _obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    /// <summary>
    /// 상점 패널
    /// </summary>
    public void StorPanel(bool argActive)
    {
        if (argActive)
        {
            m_storeMoneyText.text = GameDataManager.Instance.IsMoney.ToString();

            m_optionBtn.SetActive(false);
            m_storeAni.SetBool("Active", true);
            m_menuUIAni.SetBool("Active", false);
            m_playUIAni.SetBool("Active", false);
        }
        else
        {
            m_optionBtn.SetActive(true);
            m_storeAni.SetBool("Active", false);
            m_menuUIAni.SetBool("Active", true);
            m_playUIAni.SetBool("Active", false);
        }
    }

    /// <summary>
    /// 옵션 버튼 클릭
    /// </summary>
    public void OptionBtn(bool argActive)
    {
        if (argActive)
        {
            if (GManager.Instance.m_isGameStart)
            {
                GManager.Instance.GameStop(true);
            }

            m_optionPanel.SetActive(true);
        }
        else
        {
            if (GManager.Instance.m_isGameStart)
            {
                GManager.Instance.GameStop(false);
            }

            m_optionPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 도움말 패널
    /// </summary>
    public void HelpPanel()
    {
        m_helpUiPanel.SetActive(true);

    }

    public void ExitGame()
    {
        if (GManager.Instance.m_isGameStart)
        {
            GManager.Instance.ExitGame();
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

    /// <summary>
    /// 감도 변경
    /// </summary>
    public void SensitivityValueChanged()
    {
        GManager.Instance.m_playerManager.m_sensitivity = m_sensitivitySlider.value;
        m_sensitivityText.text = m_sensitivitySlider.value.ToString();
    }

    /// <summary>
    /// 배경음악 크기 변경
    /// </summary>
    public void BackgroundValueChanged()
    {
        GManager.Instance.m_soundManager.m_backgroundSound.volume = m_backgroundSoundSlider.value / 100;
        m_backgroundSoundText.text = m_backgroundSoundSlider.value.ToString();
    }

    public void EffectValueChanged()
    {
        m_effectSoundText.text = m_effectSoundSlider.value.ToString();
        GManager.Instance.m_soundManager.m_buildSound.volume = m_effectSoundSlider.value / 100;
        GManager.Instance.m_soundManager.m_coinGetSound.volume = m_effectSoundSlider.value / 100;
        GManager.Instance.m_soundManager.m_destroySound.volume = m_effectSoundSlider.value / 100;
        GManager.Instance.m_soundManager.m_drownSound.volume = m_effectSoundSlider.value / 100;
        GManager.Instance.m_soundManager.m_getSound.volume = m_effectSoundSlider.value / 100;
    }

    /// <summary>
    /// 경고
    /// </summary>
    /// <param name="argWarnStr">경고 문자열</param>
    public void Warning(string argWarnStr)
    {
        GameObject _obj = Instantiate(m_warningPanel, gameObject.transform);
        _obj.transform.Find("Text").gameObject.GetComponent<Text>().text = argWarnStr;
    }
}
