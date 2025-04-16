using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class GManager : MonoBehaviour
{
    [Header("Script")]
    /// <summary>
    /// 플레이어 매니저
    /// </summary>
    public PlayerManager m_playerManager = null;

    /// <summary>
    /// 오브젝트 매니저
    /// </summary>
    public ObjManager m_objManager = null;

    /// <summary>
    /// UI매니저
    /// </summary>
    public UIManager m_uiManager = null;

    /// <summary>
    /// 사운드 매니저
    /// </summary>
    public SoundManager m_soundManager = null;

    [Header("Common")]
    /// <summary>
    /// 게임이 시작을 했는지 안했는지 판별
    /// </summary>
    public bool m_isGameStart = false;

    /// <summary>
    /// 최소 게임 속도
    /// </summary>
    public float m_firstGameSpeed = 0.0f;

    /// <summary>
    /// 최대 게임 속도
    /// </summary>
    public float m_maxGameSpeed = 0.0f;

    /// <summary>
    /// 더해지는 게임 속도
    /// </summary>
    public float m_plusGameSpeed = 0.0f;

    /// <summary>
    /// 게임 속도
    /// </summary>
    public float m_gameSpeed = 0.0f;

    /// <summary>
    /// 밸런스 플래그
    /// </summary>
    bool m_balanceFlag = false;

    /// <summary>
    /// 저장할 게임 속도
    /// </summary>
    float m_tmpGameSpeed = 0.0f;

    /// <summary>
    /// 게임 안의 골드
    /// </summary>
    long m_gameGold = 0;

    [Header("Item")]
    /// <summary>
    /// 플레이어 아이템 데이터
    /// </summary>
    public ItemData[] m_itemData = null;

    /// <summary>
    /// 플레이어 아이템 딕셔너리
    /// </summary>
    Dictionary<int, PlayerItem> m_playerItemDic = new Dictionary<int, PlayerItem>();

    [Header("Laft")]
    /// <summary>
    /// 땟목 블럭 게임오브젝트
    /// </summary>
    public GameObject m_raftBlock = null;

    /// <summary>
    /// 땟목 위치 기본값
    /// </summary>
    public Vector3 m_raftBasePos = new Vector3();

    /// <summary>
    /// 최대 땟목 길이
    /// </summary>
    public int m_maxRaftLength = 0;

    /// <summary>
    /// 최소 땟목 넓이
    /// </summary>
    public int m_minRaftWidth = 0;

    /// <summary>
    /// 최대 땟목 넓이
    /// </summary>
    public int m_maxRaftWidth = 0;

    /// <summary>
    /// 첫 땟목 길이
    /// </summary>
    public int m_firstRaftLength = 0;

    /// <summary>
    /// 첫 땟목 너비
    /// </summary>
    public int m_firstRaftWidth = 0;

    /// <summary>
    /// 블럭이 만들어질 플래이어 블럭 오브젝트
    /// </summary>
    public GameObject m_playerBlock = null;

    /// <summary>
    /// 선택되지 않은 쉐이더
    /// </summary>
    public Shader m_noSelectShader = null;

    /// <summary>
    /// 선택된 블럭의 쉐이더
    /// </summary>
    public Shader m_selectShader = null;

    /// <summary>
    /// 선택된 컬러
    /// </summary>
    public Color m_raftColor;

    /// <summary>
    /// 선택되지 않은 컬러
    /// </summary>
    public Color m_selectRaftColor;

    [Header("Effect")]
    /// <summary>
    /// 파괴 이펙트
    /// </summary>
    public GameObject m_destroyEffect = null;

    /// <summary>
    /// 이펙트 부모 오브젝트
    /// </summary>
    public GameObject m_effectObj = null;

    /// <summary>
    /// 이펙트 리스트
    /// </summary>
    public List<DestroyEffect> m_effect = new List<DestroyEffect>();

    /// <summary>
    /// 스톱워치
    /// </summary>
    Stopwatch m_stopwatch = new Stopwatch();

    /// <summary>
    /// 땟목 블럭 데이터
    /// </summary>
    Raft[,] m_raftBlockData = null;

    /// <summary>
    /// 인스턴스
    /// </summary>
    static GManager g_gManager = null;

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
        Balance();
    }

    void Setting()
    {
        if (g_gManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            g_gManager = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (ItemData item in m_itemData)
        {
            m_playerItemDic.Add(item.m_code, new PlayerItem());
            m_playerItemDic[item.m_code].m_itemUseType = item.m_itemType;
            m_playerItemDic[item.m_code].m_viewObj = item.m_viewObj;
            m_playerItemDic[item.m_code].m_sampleSprite = item.m_sampleSprite;
            m_playerItemDic[item.m_code].m_name = item.m_name;

            if(item.m_itemType == ItemType.itemUseType.Raft)
            {
                m_playerItemDic[item.m_code].m_needMatCode = item.m_needMatCode;
                m_playerItemDic[item.m_code].m_needMatAmount = item.m_needMatAmount;
            }
        }
        m_itemData = null;

        m_raftBlockData = new Raft[m_maxRaftWidth, m_maxRaftLength];
        for (int i = 0; i < m_maxRaftWidth; i++)
        {
            for (int o = 0; o < m_maxRaftLength; o++)
            {
                m_raftBlockData[i, o] = null;
                SetSelect(i, o);
            }
        }
        
        ResetItem();
        ResetRaft();
        UpdateItemImfo();

        for (int i = 0; i < 10; i++)
        {
            GameObject _effect = Instantiate(m_destroyEffect.gameObject, m_effectObj.transform);
            _effect.transform.position = new Vector3(0.0f, 200.0f);
            m_effect.Add(_effect.GetComponent<DestroyEffect>());
        }

        m_playerManager.m_inputFlag = false;
        m_gameSpeed = 0.0f;
    }

    /// <summary>
    /// 게임 벨런스
    /// </summary>
    void Balance()
    {
        if (m_isGameStart)
        {
            if (m_gameSpeed <= m_maxGameSpeed)
            {
                m_gameSpeed += m_plusGameSpeed * Time.deltaTime;
            }

            if (m_balanceFlag) return;

            if (m_stopwatch.ElapsedMilliseconds / 1000 % 20 == 0)
            {
                if (m_objManager.m_rockAmount < m_objManager.m_maxRock)
                {
                    m_objManager.m_rockAmount++;
                }
                if (m_objManager.m_stickAmount < m_objManager.m_maxStick || m_objManager.m_stickAmount >= 1)
                {
                    m_objManager.m_stickAmount--;
                }

                if (m_stopwatch.ElapsedMilliseconds / 1000 % 40 == 0)
                {
                    if (m_objManager.m_boomAmount < m_objManager.m_maxBoom)
                    {
                        m_objManager.m_boomAmount++;
                    }
                    if (m_objManager.m_ironAmount < m_objManager.m_maxIron || m_objManager.m_ironAmount >= 1)
                    {
                        m_objManager.m_ironAmount--;
                    }
                    if (m_objManager.m_coinAmount < m_objManager.m_maxCoin)
                    {
                        m_objManager.m_coinAmount++;
                    }
                }

                m_balanceFlag = true;
                Invoke("BalanceDelay", 5.0f);
                return;
            }
        }
        else
        {
            m_objManager.m_rockAmount = 5;
            m_objManager.m_boomAmount = 1;
            m_objManager.m_stickAmount = m_objManager.m_maxStick;
            m_objManager.m_ironAmount = m_objManager.m_maxIron;

            m_gameSpeed = 0;
        }
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    public void GameStart()
    {
        m_gameSpeed = m_firstGameSpeed;

        m_playerManager.gameObject.SetActive(true);
        m_uiManager.m_playUIAni.SetBool("Active", true);
        m_uiManager.m_menuUIAni.SetBool("Active", false);
        m_uiManager.m_BackGroundPanel.SetActive(false);

        m_uiManager.UpdateHealth();
        m_playerManager.GameStart();
        UpdateItemImfo();
    }

    /// <summary>
    /// 게임 오버
    /// </summary>
    public void GameOver()
    {
        GameDataManager.Instance.AddScore(m_stopwatch.ElapsedMilliseconds);
        m_stopwatch.Reset();
        ResetItem();

        GameDataManager.Instance.IsMoney += m_gameGold;

        ExitGame();
        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 게임 나가기
    /// </summary>
    public void ExitGame()
    {
        if (!m_isGameStart) return;

        IsGameGold = 0;
        m_playerManager.OverAni();
        Destroy(m_playerManager.m_viewObj);
        m_playerManager.m_inputFlag = false;

        m_playerManager.IsPlayerHealth = m_playerManager.m_playerMaxHealth;
        
        ResetRaft();
        m_objManager.ResetMap();
        m_stopwatch.Reset();

        m_playerManager.gameObject.SetActive(false);
        m_uiManager.m_optionPanel.SetActive(false);
        m_uiManager.m_playUIAni.SetBool("Active", false);
        m_uiManager.m_menuUIAni.SetBool("Active", true);
        m_uiManager.m_BackGroundPanel.SetActive(true);

        m_isGameStart = false;
        m_gameSpeed = 0.0f;
    }

    /// <summary>
    /// 게임 속도 일시중지
    /// </summary>
    /// <param name="argIsStop">중지 여부</param>
    public void GameStop(bool argIsStop)
    {
        if (argIsStop)
        {
            m_playerManager.m_inputFlag = false;
            m_stopwatch.Stop();
            m_tmpGameSpeed = m_gameSpeed;
            m_gameSpeed = 0.0f;
        }
        else
        {
            if (m_tmpGameSpeed <= 0.0f)
            {
                m_tmpGameSpeed = 0.0f;
                m_gameSpeed = 20.0f;
                return;
            }

            m_gameSpeed = m_tmpGameSpeed;
            m_stopwatch.Start();
            m_playerManager.m_inputFlag = true;
        }
    }

    /// <summary>
    /// 아이템 리셋
    /// </summary>
    void ResetItem()
    {
        m_playerItemDic[20000].m_amount = 50;
        m_playerItemDic[20001].m_amount = 50;
    }

    /// <summary>
    /// 아이템 청구
    /// </summary>
    /// <param name="argCode">아이템 코드</param>
    /// <param name="argAmount">아이템 갯수</param>
    public void ManageItem(int argCode, int argAmount)
    {
        PlayerItem _item;
        if (!m_playerItemDic.TryGetValue(argCode, out _item))
        {
            return;
        }
        
        m_playerItemDic[argCode].m_amount += argAmount;
        if (m_playerItemDic[argCode].m_amount <= 0)
        {
            m_playerItemDic[argCode].m_amount = 0;
        }
        
        UpdateItemImfo();
    }

    /// <summary>
    /// 아이템 관리 데이터 받아오기
    /// </summary>
    /// <param name="argCode">아이템 코드</param>
    /// <returns></returns>
    public PlayerItem ManageItem(int argCode)
    {
        PlayerItem _item;
        if (!m_playerItemDic.TryGetValue(argCode, out _item))
        {
            return null;
        }

        return m_playerItemDic[argCode];
    }

    /// <summary>
    /// 아이템 관리 데이터 존재 여부 확인
    /// </summary>
    /// <param name="argCode">아이템 코드</param>
    /// <returns></returns>
    public bool ExistItem(int argCode)
    {
        PlayerItem _item;
        if (m_playerItemDic.TryGetValue(argCode, out _item))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 아이템 정보 업데이트
    /// </summary>
    void UpdateItemImfo()
    {
     
        for(int i = 0; i < m_uiManager.m_itemUIContent.transform.childCount; i++)
        {
            Destroy(m_uiManager.m_itemUIContent.transform.GetChild(i).gameObject);
        }

        foreach(KeyValuePair<int, PlayerItem> item in m_playerItemDic)
        {
            if(item.Value.m_itemUseType == ItemType.itemUseType.Material || item.Value.m_itemUseType == ItemType.itemUseType.Raft)
            {


                if (item.Value.m_amount > 0)
                {

                    GameObject _panel = Instantiate(m_uiManager.m_itemUIPanel, m_uiManager.m_itemUIContent.transform);
                    _panel.transform.localScale = new Vector3(1, 1, 1);
          
                    _panel.transform.Find("Image").GetComponent<Image>().sprite = item.Value.m_sampleSprite;
                    _panel.transform.Find("NameText").GetComponent<Text>().text = item.Value.m_name;
                    _panel.transform.Find("AmountText").GetComponent<Text>().text = item.Value.m_amount.ToString();

                }


            }
        }
    }

    /// <summary>
    /// 땟목 블럭 체력 관리
    /// </summary>
    public void ManageRaftHp(int argX, int argZ, int argHp)
    {
        if(ManageRaft(argX, argZ) == null)
        {
            return;
        }

        m_raftBlockData[argX, argZ].m_hp += argHp;

        if (m_raftBlockData[argX, argZ].m_hp <= 0)
        {
            if (m_raftBlockData[argX, argZ].m_code <= 10000)
            {
                ManageRaft(argX, argZ, -1);
                RaftDestroyEffect(argX, argZ);
                m_soundManager.m_destroySound.Play();
                return;
            }
            ManageRaft(argX, argZ, m_raftBlockData[argX, argZ].m_code - 1);
        }
    }

    /// <summary>
    /// 뗏목 리셋
    /// </summary>
    void ResetRaft()
    {
        for (int i = 0; i < m_maxRaftWidth + 1; i++)
        {
            for (int o = 0; o < m_maxRaftLength + 1; o++)
            {
                ManageRaft(i, o, -1);
            }
        }

        int[] _widPos = new int[m_firstRaftWidth];
        for (int i = 0; i < m_firstRaftWidth; i++)
        {
            _widPos[i] = i + (int)m_raftBasePos.x;
        }

        int[] _lenPos = new int[m_firstRaftLength];
        for (int i = 0; i < m_firstRaftLength; i++)
        {
            _lenPos[i] = i + (int)m_raftBasePos.z;
        }

        for (int i = 0; i < _widPos.Length; i++)
        {
            for (int o = 0; o < _lenPos.Length; o++)
            {
                ManageRaft(_widPos[i], _lenPos[o], 10000);
            }
        }
    }

    /// <summary>
    /// 땟목 접근
    /// </summary>
    /// <param name="argX">x</param>
    /// <param name="argZ">z</param>
    public Raft ManageRaft(int argX, int argZ)
    {
        if (m_maxRaftLength <= argZ || m_maxRaftWidth <= argX || m_minRaftWidth >= argX || argZ < 0)
        {
            return null;
        }

        if (m_raftBlockData[argX, argZ] == null)
        {
            return null;
        }

        return m_raftBlockData[argX, argZ];
    }

    /// <summary>
    /// 땟목 블럭 관리
    /// </summary>
    /// <param name="argX">x</param>
    /// <param name="argZ">z</param>
    /// <param name="argBlockCode">코드</param>
    public void ManageRaft(int argX, int argZ, int argBlockCode)
    {
        if (m_maxRaftLength <= argZ || m_maxRaftWidth <= argX || m_minRaftWidth >= argX || argZ < 0)
        {
            return;
        }

        GameObject _block = m_raftBlockData[argX, argZ].gameObject;
        for (int i = 1; i < _block.transform.childCount; i++)
        {
            Destroy(_block.transform.GetChild(i).gameObject);
        }

        if (argBlockCode == -1)
        {
            _block.layer = 7;
            _block.GetComponent<Collider>().isTrigger = true;

            Raft _selectRaft = m_raftBlockData[argX, argZ];
            GameObject _selectViewObj = Instantiate(m_playerItemDic[10000].m_viewObj);

            _selectViewObj.transform.SetParent(_block.transform);
            _selectViewObj.transform.localPosition = Vector3.zero;

            _selectRaft.ResetMatStack();
            _selectRaft.m_viewObj = _selectViewObj;
            _selectRaft.m_meshRenderer = _selectRaft.m_viewObj.transform.GetChild(0).GetComponent<MeshRenderer>();
            SetShader(argX, argZ, m_selectShader, false);
            return;
        }
        else if(ExistItem(argBlockCode))
        {
            _block.layer = 6;
            _block.GetComponent<Collider>().isTrigger = false;

            Raft _raft = m_raftBlockData[argX, argZ];
            GameObject _viewObj = Instantiate(m_playerItemDic[argBlockCode].m_viewObj);

            _raft.m_code = argBlockCode;
            _raft.m_hp = 1;
            
            _viewObj.transform.SetParent(_block.transform);
            _viewObj.transform.localPosition = Vector3.zero;

            _raft.ResetMatStack();
            _raft.m_viewObj = _viewObj;
            _raft.m_meshRenderer = _raft.m_viewObj.transform.GetChild(0).GetComponent<MeshRenderer>();
            SetShader(argX, argZ, m_noSelectShader, true);
        }
    }

    /// <summary>
    /// 선택 블록 생성
    /// </summary>
    /// <param name="argX">x</param>
    /// <param name="argZ">z</param>
    void SetSelect(int argX, int argZ)
    {
        if (m_maxRaftLength <= argZ || m_maxRaftWidth <= argX || m_minRaftWidth >= argX || argZ < 0)
        {
            return;
        }

        if (m_raftBlockData[argX, argZ] != null)
        {
            return;
        }
        
        GameObject _block = null;

        _block = Instantiate(m_raftBlock);
        _block.layer = 6;
        _block.GetComponent<Collider>().isTrigger = true;
        _block.transform.SetParent(m_playerBlock.transform);
        _block.transform.localPosition = new Vector3(argX, m_raftBasePos.y, argZ);

        Raft _raft = _block.GetComponent<Raft>();
        GameObject _viewObj = Instantiate(m_playerItemDic[10000].m_viewObj);

        m_raftBlockData[argX, argZ] = _raft;
        _raft.m_code = 0;

        _viewObj.transform.SetParent(_block.transform);
        _viewObj.transform.localPosition = Vector3.zero;

        _raft.ResetMatStack();
        _raft.m_viewObj = _viewObj;
        _raft.m_meshRenderer = _raft.m_viewObj.transform.GetChild(0).GetComponent<MeshRenderer>();
        SetShader(argX, argZ, m_selectShader, false);
    }

    /// <summary>
    /// 가장 가까운 뗏목
    /// </summary>
    /// <returns>트렌스폼</returns>
    public Vector3 CloseRaft(int argX, int argZ)
    {
        Vector3 _vector = new Vector3();

        float _x = -1;
        float _z = -1;
        int _mul = 10000;
        int _tmpDisX = -1;
        int _tmpDisZ = -1;

        for (int i = m_minRaftWidth + 1; i < m_maxRaftWidth; i++)
        {
            for(int o = 0; o < m_maxRaftLength; o++)
            {
                if (m_raftBlockData[i, o].gameObject.layer == 6)
                {
                    _tmpDisX = Mathf.Abs(i - argX);
                    _tmpDisZ = Mathf.Abs(i - argZ);
                    
                    if (_tmpDisX * _tmpDisZ < _mul)
                    {
                        _mul = _tmpDisX * _tmpDisZ;
                        _x = i;
                        _z = o;
                    }
                }
            }
        }

        if(_x == -1 || _z == -1)
        {
            return m_raftBasePos;
        }

        _vector = new Vector3(_x, 0.0f, _z);
        return _vector;
    }

    /// <summary>
    /// 파괴 이펙트 실행
    /// </summary>
    /// <param name="argX">x</param>
    /// <param name="argZ">z</param>
    public void RaftDestroyEffect(int argX, int argZ)
    {
        if (m_raftBlockData[argX, argZ] == null) return;
        if (m_effect.Count <= 0) return;
        m_effect[0].gameObject.transform.localPosition = new Vector3(argX, 0.0f, argZ);
        m_effect[0].PlayEffect();
        m_effect.RemoveAt(0);
    }
    
    /// <summary>
    /// 쉐이더 설정
    /// </summary>
    /// <param name="argSet">설정</param>
    public void SetShader(int argX, int argZ, Shader argShader, bool argSet)
    {
        if (m_maxRaftLength <= argZ || m_maxRaftWidth <= argX || m_minRaftWidth >= argX || argZ < 0)
        {
            return;
        }

        m_raftBlockData[argX, argZ].m_meshRenderer.material.shader = argShader;
        if (argSet)
        {
            m_raftBlockData[argX, argZ].m_meshRenderer.material.SetColor("_Color", m_raftColor);
        }
        else
        {
            m_raftBlockData[argX, argZ].m_meshRenderer.material.SetColor("_Color", m_selectRaftColor);
        }
    }

    /// <summary>
    /// 쉐이더 설정
    /// </summary>
    /// <param name="argSet">설정</param>
    public void SetShader(int argX, int argZ, bool argSet)
    {
        if (argSet)
        {
            m_raftBlockData[argX, argZ].m_meshRenderer.material.SetColor("_Color", m_raftColor);
        }
        else
        {
            m_raftBlockData[argX, argZ].m_meshRenderer.material.SetColor("_Color", m_selectRaftColor);
        }
    }

    void BalanceDelay()
    {
        m_balanceFlag = false;
    }

    public Stopwatch IsStopWatch
    {
        get
        {
            return m_stopwatch;
        }
    }

    /// <summary>
    /// 게임 속도 변경
    /// </summary>
    public float IsGameSpeed
    {
        get
        {
            return m_gameSpeed;
        }
        set
        {
            m_gameSpeed = value;
        }
    }

    /// <summary>
    /// 게임 골드 변경
    /// </summary>
    public long IsGameGold
    {
        get
        {
            return m_gameGold;
        }
        set
        {
            m_gameGold = value;
            m_uiManager.UpdateGameGold(m_gameGold);
        }
    }

    /// <summary>
    /// 인스턴스
    /// </summary>
    public static GManager Instance
    {
        get
        {
            return g_gManager;
        }
    }
}
