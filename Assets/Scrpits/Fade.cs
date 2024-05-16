using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Button fadein;
    [SerializeField] Button fadeout;
    private float time = 0f;
    private float F_time = 1f;
    private bool checkfadein = false;
    private bool checkfadeinstart = false;
    private bool checkfadeout = false;
    private bool checkfadeoutstart = false;

    private void Awake()
    {
        fadein.onClick.AddListener(() =>
        {
            checkfadein = true;
            Fadein();
        });
        fadeout.onClick.AddListener(() =>
        {
            checkfadeout = true;
            Fadeout();
        });
    }

    private void Fadein()
    {
        if(checkfadein == true && checkfadeinstart == false)
        {
        StartCoroutine(FadeOn());
        }
    }
    private void Fadeout()
    {
        if(checkfadeout == true && checkfadeoutstart == false)
        {
        StartCoroutine(FadeOff());
        }
    }

    IEnumerator FadeOn()
    {
        if (checkfadeout == false)
        {
            img.gameObject.SetActive(true);
            checkfadeinstart = true;
            time = 0f;
            Color alpha = img.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                img.color = alpha;
                yield return null;
            }
        }
        checkfadein = false;
        checkfadeinstart = false;
        yield return null;
    }

    IEnumerator FadeOff()
    {
        if (checkfadein == false)
        {
            checkfadeoutstart = true;
            Color alpha = img.color;
            time = 0f;
            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time);
                img.color = alpha;
                yield return null;
            }

        checkfadeout = false;
        checkfadeoutstart = false;
        img.gameObject.SetActive(false);
        yield return null;
        }
    }


}
