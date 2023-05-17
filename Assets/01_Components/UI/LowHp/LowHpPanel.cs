using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class EventManager
{
    public static event Action OnLowHpPanel;
    public static void Fire_OnLowHpPanel() { OnLowHpPanel?.Invoke(); }
}

public class LowHpPanel : MonoBehaviour
{
    private Image image;
    private IEnumerator changeCR;
    private void Awake()
    {
        image = GetComponent<Image>();

        EventManager.OnLowHpPanel += OnLowHpPanel;
       

        image.enabled = false;
    }

    float aimAlpha;
    Color aimColor;
    private IEnumerator Change()
    {
        image.enabled = true;

        aimColor = image.color;

        while (Mathf.Abs(aimColor.a - aimAlpha) > 0.1f)
        {
            aimColor.a = Mathf.MoveTowards(aimColor.a, aimAlpha, Time.deltaTime * 3);

            image.color = aimColor;

            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        aimAlpha = 0;

        while (Mathf.Abs(aimColor.a - aimAlpha) > 0.1f)
        {
            aimColor.a = Mathf.MoveTowards(aimColor.a, aimAlpha, Time.deltaTime * 3);

            image.color = aimColor;

            yield return null;
        }
        
        image.enabled = false;
    }
    void OnLowHpPanel()
    {
        aimAlpha = 0.7f;

        changeCR = Change();

        if (changeCR != null)
            StopCoroutine(changeCR);

        

        StartCoroutine(changeCR);
    }

    private void OnDestroy()
    {
        EventManager.OnLowHpPanel -= OnLowHpPanel;
    }
}
