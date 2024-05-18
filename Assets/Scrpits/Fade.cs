using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    Image img;
    [SerializeField] Button fadein;
    [SerializeField] Button fadeout;
    private float time = 0f;
    private float F_time = 1f;
    private UnityAction action = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //fadein.onClick.AddListener(() =>
        //{
        //    checkfadein = true;
        //    FadeOut();
        //});
        //fadeout.onClick.AddListener(() =>
        //{
        //    checkfadeout = true;
        //    FadeIn();
        //});
        img = GetComponentInChildren<Image>();
    }

    public void FadeOut(UnityAction _action)
    {
        action = _action;
        StartCoroutine(FadeOn());

    }
    public void FadeIn()
    {

        StartCoroutine(FadeOff());
    }

    IEnumerator FadeOn()
    {

        img.gameObject.SetActive(true);
        time = 0f;
        Color alpha = img.color;
        while (alpha.a < 1f)
        {
            time += Time.unscaledDeltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            img.color = alpha;
            yield return null;
        }
        if (action != null)
        {
            action.Invoke();
            action = null;
        }
        yield return new WaitForSeconds(1);
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.unscaledDeltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            img.color = alpha;
            yield return null;
        }
        img.gameObject.SetActive(false);
    }

    IEnumerator FadeOff()
    {

        Color alpha = img.color;
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.unscaledDeltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            img.color = alpha;
            yield return null;
        }

        img.gameObject.SetActive(false);

    }


}
