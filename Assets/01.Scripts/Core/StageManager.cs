using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] GameObject _upCloseDoor;
    [SerializeField] GameObject _upOpenDoor;
    [SerializeField] GameObject _downCloseDoor;
    [SerializeField] GameObject _downOpenDoor;
    [SerializeField] GameObject _escPanel;
    [SerializeField] GameObject _settingPanel;

    private Vector3 _initPos = new Vector3(0, -4.5f, 0);
    private RectTransform _blackImage;
    private Image _blackPanelImg;
    private Transform _playerTrm;
    private bool _isESC = false;
    public bool IsESC
    {
        set => _isESC = value;
        get => _isESC;
    }
    private bool _isSetting = false;
    public bool IsSetting
    {
        set => _isSetting = value;
        get => _isSetting;
    }
    private bool _isGameStart = false;
    public bool IsGameStart
    {
        set => _isGameStart = value;

        get => _isGameStart;
    }

    private Sequence seq;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        _blackImage = GameObject.Find("Canvas/StageSkipPanel").GetComponent<RectTransform>();
        _blackPanelImg = _blackImage.GetComponent<Image>();

        StageStart();
        CameraManager.Instance.BossToPlayer(()=>
        {
            _isGameStart = true;
            Debug.Log(IsGameStart);
        });
    }

    private void Update()
    {
        if(_playerTrm.position.y >= 18f)
        {
            BlackScreenUp();
            _playerTrm.position = _initPos;
            Invoke("SceneChange", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isESC = true;
        }

        _escPanel.SetActive(_isESC ? true : false);
        _settingPanel.SetActive(_isSetting ? true : false);
        Time.timeScale = _isESC || _isSetting ? 0 : 1;
    }

    public void SceneChange()
    {
        seq.Kill();
        SceneManager.LoadScene("StartCutScene");
    }

    public void BlackScreenUp()
    {
        seq = DOTween.Sequence();
        seq.Append(_blackPanelImg.DOFade(1, 0.5f));
    }

    public void BlackScreenDown()
    {
        seq = DOTween.Sequence();
        seq.Append(_blackPanelImg.DOFade(0, 0.5f));
    }

    public void StageStart()
    {
        _upCloseDoor.SetActive(true);
        _upOpenDoor.SetActive(false);

        _downCloseDoor.SetActive(true);
        _downOpenDoor.SetActive(false);
        BlackScreenUp();
        Invoke("BlackScreenDown", 0.5f);

        _playerTrm.position = _initPos;
    }

    public void PlayerDie()
    {
        StartCoroutine(PlayerDieCoroutine());
    }

    IEnumerator PlayerDieCoroutine()
    {
        _isGameStart = false;
        _blackPanelImg.DOFade(1, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    public void StageClear()
    {
        _upCloseDoor.SetActive(false);
        _upOpenDoor.SetActive(true);

        _downCloseDoor.SetActive(false);
        _downOpenDoor.SetActive(true);
        //SceneManager.LoadScene("StartCutScene");
    }
}
