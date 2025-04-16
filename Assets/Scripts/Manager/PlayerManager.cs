using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Script")]
    /// <summary>
    /// �� üĿ
    /// </summary>
    public BlockChecker m_blockChecker = null;

    /// <summary>
    /// ���̽�ƽ
    /// </summary>
    public Joystick m_joystick = null;

    [Header("Player Effect")]
    /// <summary>
    /// ����Ʈ
    /// </summary>
    public GameObject m_effect = null;

    [Header("Player")]
    /// <summary>
    /// ȸ�� ����
    /// </summary>
    public float m_sensitivity = 0.0f;

    /// <summary>
    /// �ӵ�
    /// </summary>
    public float m_speed = 0.0f;

    /// <summary>
    /// ���� ��
    /// </summary>
    public float m_jumpForce = 0.0f;

    /// <summary>
    /// ���� �ӵ�
    /// </summary>
    public float m_dieSpeed = 0.0f;

    /// <summary>
    /// �Ǽ� �ӵ�
    /// </summary>
    public float m_buildSpeed = 0.0f;

    /// <summary>
    /// �÷��̾� �ִ� ü��
    /// </summary>
    public int m_playerMaxHealth = 0;

    /// <summary>
    /// �÷��̾� ǥ�� ��ġ
    /// </summary>
    public Vector3 m_playerBasePos = new Vector3();

    /// <summary>
    /// ��ǲ �÷���
    /// </summary>
    public bool m_inputFlag = false;

    /// <summary>
    /// ���� ��
    /// </summary>
    public bool m_buildFlag = false;

    /// <summary>
    /// Ŭ�� ������
    /// </summary>
    public float m_clickDelay = 0.0f;

    /// <summary>
    /// ���̴� ������Ʈ
    /// </summary>
    public GameObject m_viewObj = null;

    /// <summary>
    /// cam ani animator
    /// </summary>
    public Animation m_camAni = null;

    /// <summary>
    /// �÷��̾� ü��
    /// </summary>
    int m_playerHealth = 0;

    /// <summary>
    /// ���̾� ����
    /// </summary>
    int m_layer = 0;

    /// <summary>
    /// Ŭ�� ������ �÷���
    /// </summary>
    bool m_clickDelayFlag = true;

    /// <summary>
    /// �ͻ� ���̸� true
    /// </summary>
    bool m_drownFlag = false;

    /// <summary>
    /// ���� ����
    /// </summary>
    float m_dieProgress = 0.0f;

    /// <summary>
    /// ���� ��Ȳ
    /// </summary>
    float m_buildProgress = 0.0f;

    /// <summary>
    /// ��ġ ����
    /// </summary>
    Vector3 m_tmpPos = Vector3.zero;

    /// <summary>
    /// ȸ�� ����
    /// </summary>
    Vector3 m_tmpRot = Vector3.zero;

    /// <summary>
    /// �ִϸ�����
    /// </summary>
    Animator m_animator = null;

    /// <summary>
    /// ������ �ٵ�
    /// </summary>
    Rigidbody m_rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inputFlag)
        {
            if (!m_buildFlag)
            {
                MoveController();
                if (m_clickDelayFlag)
                {
                    KeyController();
                }
            }
            BuildProgress();
            DieProgress();
        }
    }

    /// <summary>
    /// �ʱ� ����
    /// </summary>
    void Setting()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0.0f, -19f, 0.0f);

        m_playerHealth = m_playerMaxHealth;
        GManager.Instance.m_uiManager.UpdateHealth();
        gameObject.SetActive(false);

        m_layer = 0;
        m_layer = 1 << LayerMask.NameToLayer("Water");
        m_layer |= 1 << LayerMask.NameToLayer("Raft");
        m_layer |= 1 << LayerMask.NameToLayer("Default");
    }

    /// <summary>
    /// ĳ���� ����
    /// </summary>
    /// <param name="argCode"></param>
    public void ChangeCharacter(int argCode)
    {
        GameDataManager.Instance.ManageCharacter(argCode);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void GameStart()
    {
        m_viewObj = Instantiate(GameDataManager.Instance.ManageCharacter(GameDataManager.Instance.IsCharacter).m_viewObj, transform);
        m_animator = m_viewObj.GetComponent<Animator>();
        m_viewObj.transform.localPosition = new Vector3(0.0f, -1.0f);

        GManager.Instance.m_isGameStart = true;
        GManager.Instance.IsStopWatch.Start();
        m_inputFlag = true;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void GameOver()
    {
        if (m_playerHealth <= 0)
        {
            GManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// ������ ��Ʈ�ѷ�
    /// </summary>
    void MoveController()
    {
        switch (IsGround)
        {
            case 4:
                m_buildFlag = false;
                m_drownFlag = true;

                m_animator.SetBool("Jump", false);
                m_animator.SetBool("Drown", true);

                m_effect.SetActive(true);
                break;
            case 6:
                m_animator.SetBool("Jump", false);
                m_animator.SetBool("Drown", false);
                m_drownFlag = false;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                break;
            default:
                m_animator.SetBool("Drown", false);
                break;
        }

        if (transform.position.y < -10.0f)
        {
            GManager.Instance.GameOver();
            return;
        }

        if (m_drownFlag) return;

        Vector3 _vector = Vector3.zero;

        _vector.x = m_joystick.Horizontal;
        _vector.z = m_joystick.Vertical;

        _vector.x = Input.GetAxis("Horizontal");
        _vector.z = Input.GetAxis("Vertical");

        float _xAbs = Mathf.Abs(_vector.x);
        float _zAbs = Mathf.Abs(_vector.z);

        if (_vector == Vector3.zero)
        {
            m_animator.SetFloat("Base", 0.0f);
            return;
        }

        if (transform.position.x - _xAbs < GManager.Instance.m_minRaftWidth)
        {
            transform.position = new Vector3(GManager.Instance.m_minRaftWidth, transform.position.y, transform.position.z);
        }
        if (transform.position.x + _xAbs > GManager.Instance.m_maxRaftWidth)
        {
            transform.position = new Vector3(GManager.Instance.m_maxRaftWidth, transform.position.y, transform.position.z);
        }
        if (transform.position.z - _zAbs < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
        if (transform.position.z + _zAbs > GManager.Instance.m_maxRaftLength)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GManager.Instance.m_maxRaftLength);
        }

        transform.Translate(_vector.normalized * m_speed * Time.deltaTime);
        if (_vector.x != 0.0f || _vector.z != 0.0f)
        {
            m_viewObj.transform.localRotation = Quaternion.LookRotation(_vector);
        }
        else
        {
            m_viewObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        m_viewObj.transform.localPosition = new Vector2(0.0f, -1.0f);

        if (!m_drownFlag)
        {
            m_animator.SetFloat("Base", _xAbs + _zAbs);
        }
    }

    /// <summary>
    /// ���� ���
    /// </summary>
    public void DieProgress()
    {
        if (m_drownFlag)
        {
            m_dieProgress += m_dieSpeed * Time.deltaTime;
            if (GManager.Instance.m_soundManager.m_drownSound.isPlaying == false)
            {
                GManager.Instance.m_soundManager.m_drownSound.Play();
            }

            if (m_dieProgress >= 1.0f)
            {
                Vector3 _spawnPos = GManager.Instance.CloseRaft((int)transform.position.x, (int)transform.position.z);

                m_playerHealth -= 1;
                transform.localPosition = new Vector3(_spawnPos.x, m_playerBasePos.y, _spawnPos.z);
                GManager.Instance.m_uiManager.UpdateHealth();

                if (m_playerHealth <= 0)
                {
                    GameOver();
                }

                m_dieProgress = 0.0f;
                m_drownFlag = false;
                GManager.Instance.m_soundManager.m_drownSound.Stop();
                m_effect.SetActive(false);
            }
        }
        else
        {
            m_dieProgress = 0.0f;
            m_drownFlag = false;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Jump()
    {
        if (IsGround == 6)
        {
            m_animator.SetBool("Jump", true);
            m_rigidbody.velocity = Vector3.zero;
            m_rigidbody.velocity = Vector3.up * m_jumpForce;
        }
    }

    /// <summary>
    /// �÷��̾� Ű ��Ʈ�ѷ�
    /// </summary>
    void KeyController()
    {
        if (m_blockChecker.m_checkBlock == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            
            BuildRaft();
        }

        if (Input.GetMouseButtonDown(1))
        {
            UpgradeRaft();
        }

        m_clickDelayFlag = true;
        //Invoke("ClickDelay", m_clickDelay);
    }

    /// <summary>
    /// ���� ��Ȳ
    /// </summary>
    void BuildProgress()
    {
        if (m_blockChecker.m_checkBlock == null)
        {
            GManager.Instance.m_uiManager.m_buildSlider.gameObject.SetActive(false);
            m_buildProgress = 0.0f;
            m_buildFlag = false;
            return;
        }

        if (m_buildFlag)
        {
            if (GManager.Instance.m_soundManager.m_buildSound.isPlaying == false)
            {
                GManager.Instance.m_soundManager.m_buildSound.Play();
            }

            PlayerItem _itemData = GManager.Instance.ManageItem(10000);

            if (_itemData.m_needMatAmount > GManager.Instance.ManageItem(_itemData.m_needMatCode).m_amount)
            {
                GManager.Instance.m_uiManager.m_buildSlider.gameObject.SetActive(false);
                m_buildProgress = 0.0f;
                m_buildFlag = false;
                GManager.Instance.m_uiManager.Warning("Not Enough Material!");
                GManager.Instance.m_soundManager.m_buildSound.Stop();
                return;
            }

            transform.position = m_tmpPos;
            transform.rotation = Quaternion.Euler(m_tmpRot);
            GManager.Instance.m_uiManager.m_buildSlider.gameObject.SetActive(true);
            m_buildProgress += m_buildSpeed * Time.deltaTime;
            GManager.Instance.m_uiManager.m_buildSlider.value = m_buildProgress;

            if (m_buildProgress >= 1.0f)
            {
                GManager.Instance.ManageItem(_itemData.m_needMatCode, -_itemData.m_needMatAmount);

                Transform _trans = m_blockChecker.m_checkBlock.transform;

                GManager.Instance.ManageRaft((int)_trans.localPosition.x, (int)_trans.localPosition.z, 10000);
                GManager.Instance.m_uiManager.m_buildSlider.gameObject.SetActive(false);

                m_buildProgress = 0.0f;
                m_buildFlag = false;
                GManager.Instance.m_soundManager.m_buildSound.Stop();
            }
        }
        else
        {
            m_buildProgress = 0.0f;
            m_buildFlag = false;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void BuildRaft()
    {
        if (m_blockChecker.m_checkBlock == null)
        {
            return;
        }

        Raft _raft = m_blockChecker.m_checkBlock.GetComponent<Raft>();
        Transform _trans = m_blockChecker.m_checkBlock.transform;

        if (m_blockChecker.m_checkBlock.layer == 6)
        {
            /*
            if (m_takeFlag)
            {
                return;
            }

            m_animator.SetBool("Take", true);

            m_takeFlag = true;
            m_takeCode = _raft.m_code;
            m_tmpSpeed = m_speed;
            m_speed = m_speed / 2.0f;
            */

            PlayerItem _raftData = GManager.Instance.ManageItem(_raft.m_code);

            GManager.Instance.ManageRaft((int)_trans.localPosition.x, (int)_trans.localPosition.z, -1);
            GManager.Instance.RaftDestroyEffect((int)_trans.localPosition.x, (int)_trans.localPosition.z);
            GManager.Instance.ManageItem(_raftData.m_needMatCode, (int)(_raftData.m_needMatAmount - _raftData.m_needMatAmount * 0.5f));

            GManager.Instance.m_soundManager.m_woodSound.Play();
        }
        else if (m_blockChecker.m_checkBlock.layer == 7)
        {
            m_buildFlag = true;

            m_tmpPos = transform.position;
            m_tmpRot = transform.rotation.eulerAngles;

            /*
            if (m_takeFlag)
            {
                GManager.Instance.ManageRaft((int)_trans.localPosition.x, (int)_trans.localPosition.z, m_takeCode);
                m_takeCode = 0;
                m_takeFlag = false;
                m_animator.SetBool("Take", false);
                m_speed = m_tmpSpeed;
                m_tmpSpeed = 0.0f;
            }
            else
            {
                m_buildFlag = true;
            }
            */
        }
    }

    /// <summary>
    /// ���׷��̵�
    /// </summary>
    public void UpgradeRaft()
    {
        if (m_blockChecker.m_checkBlock == null)
        {
            return;
        }

        Raft _raft = m_blockChecker.m_checkBlock.GetComponent<Raft>();
        Transform _trans = m_blockChecker.m_checkBlock.transform;

        if (m_blockChecker.m_checkBlock.layer == 6)
        {
            PlayerItem _itemData = GManager.Instance.ManageItem(_raft.m_code + 1);

            if (_itemData == null)
            {
                return;
            }

            if (_itemData.m_needMatAmount > GManager.Instance.ManageItem(_itemData.m_needMatCode).m_amount)
            {
                GManager.Instance.m_uiManager.Warning("Not Enough Material!");
                return;
            }
            GManager.Instance.ManageItem(_itemData.m_needMatCode, -_itemData.m_needMatAmount);
            GManager.Instance.ManageRaft((int)_trans.localPosition.x, (int)_trans.localPosition.z, _raft.m_code + 1);
        }
    }

    /// <summary>
    /// �ִϸ��̼� ����
    /// </summary>
    public void StartAni()
    {
        m_camAni.PlayQueued("CamAni");
    }

    /// <summary>
    /// cam ani back
    /// </summary>
    public void OverAni()
    {
        m_camAni.PlayQueued("CamAniBack");
    }

    public void ClickDelay()
    {
        m_clickDelayFlag = true;
    }

    /// <summary>
    /// �׶��� üũ
    /// </summary>
    int IsGround
    {
        get
        {
            Vector3 _pos = transform.position;
            _pos.y -= 0.5f;
            Collider[] _coll = Physics.OverlapSphere(_pos, 0.5f, m_layer);

            try
            {
                return _coll[0].gameObject.layer;
            }
            catch
            {
                return 0;
            }

        }
    }

    /// <summary>
    /// �÷��̾� ü��
    /// </summary>
    public int IsPlayerHealth
    {
        get
        {
            return m_playerHealth;
        }
        set
        {
            m_playerHealth = value;
        }
    }
}
