using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum enumScene
{
    GameScene,
    StartScene,
    EndScene,
    ClearScene,
}
public class ScenceManager : MonoBehaviour
{


    [SerializeField] Button btnStart;
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnEnd;
    [SerializeField] Button checkEndYes;
    [SerializeField] Button checkEndNo;
    [SerializeField] GameObject checkEnd;
    void Awake()
    {
        btnStart.onClick.AddListener(() =>
        {
            Fade.instance.FadeOut(() =>
            {
                SceneManager.LoadSceneAsync((int)enumScene.GameScene);
            });
        });

        btnEnd.onClick.AddListener(() =>
        {

            checkEnd.SetActive(true);
            btnSetting.interactable = false;
            btnEnd.interactable = false;
            btnStart.interactable = false;

        });

        checkEndYes.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });

        checkEndNo.onClick.AddListener(() =>
        {
            checkEnd.SetActive(false);
            btnSetting.interactable = true;
            btnEnd.interactable = true;
            btnStart.interactable = true;
        });

    }
    private void Start()
    {
        Fade.instance.FadeIn();
    }


}