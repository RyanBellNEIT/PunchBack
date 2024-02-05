using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{

    [SerializeField] private GameObject blackFade;
    private bool playing = false;
    public int destroyed = 0;

    void Update()
    {
        if(destroyed == 1 && playing == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(FadeBlackOutSquare());
        }
    }


    public IEnumerator FadeBlackOutSquare(bool FadeToBlack = true, int fadespeed = 1)
    {
        playing = true;
        Color objectColor = blackFade.GetComponent<Image>().color;
        float fadeAmount;

        if(FadeToBlack)
        {
            yield return new WaitForSeconds(2f);
            while (blackFade.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadespeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackFade.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            SceneManager.LoadScene("Win");
        }
    }

}
