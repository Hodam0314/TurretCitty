using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoMain : MonoBehaviour
{
    [SerializeField] Button MainButton;

    private void Awake()
    {
        MainButton.onClick.AddListener(() =>
        {
            Fade.instance.FadeOut(() =>
            {
            SceneManager.LoadSceneAsync((int)enumScene.StartScene);
            });

        });

    }

}
