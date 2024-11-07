using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private SceneStateController m_SceneStateController = new SceneStateController();
    public static GameLoop inst;

    #region 定義遊戲開發時所使用的基礎解析度寬高
    //定義遊戲開發時所使用的基礎解析度寬高
    public float baseWidth = 1080;
    public float baseHeight = 1920;
    public float baseOrthographicSize = 5;
    #endregion

    #region BGM
    [Header("BGM")]
    public AudioSource audioSource;
    public List<AudioClip> list_BGM;
    #endregion

    private void Awake()
    {
        //轉換場景時此物件不會被刪除
        GameObject.DontDestroyOnLoad(this.gameObject);
        inst = this;
        //
        //Camera.main.aspect = this.baseWidth / this.baseHeight;

        //float newOrthographicSize = (float)Screen.height / (float)Screen.width * this.baseWidth / this.baseHeight * this.baseOrthographicSize;
        //Camera.main.orthographicSize = Mathf.Max(newOrthographicSize, this.baseOrthographicSize);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_SceneStateController.SetState(new StartState(m_SceneStateController),"");
        PlayStartMainMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        m_SceneStateController.StateUpdate();
    }

    /// <summary>
    /// 播放
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }

    /// <summary>
    /// 暫停
    /// </summary>
    public void Pause()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// 切歌
    /// </summary>
    public void CutSong(int index)
    {
        audioSource.clip = list_BGM[index];
    }

    /// <summary>
    /// 播放開始主選單音樂
    /// </summary>
    public void PlayStartMainMenuMusic()
    {
        //CutSong(index);
        audioSource.clip = list_BGM[2];
        Play();
    }

    /// <summary>
    /// 播放戰鬥開始音樂
    /// </summary>
    public void PlayBattleStartMusic()
    {
        //CutSong(index);
        audioSource.clip = list_BGM[1];
        Play();
    }

    public void PlayBattleBossMusic()
    {
        //CutSong(index);
        audioSource.clip = list_BGM[0];
        Play();
    }

    public void PlayVictoryMusic()
    {
        //CutSong(index);
        audioSource.clip = list_BGM[3];
        Play();
    }
}
