using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopupController : MonoBehaviour
{
    public TMP_Text text;
    private float disappearSpeed;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        disappearTimer = 1f;
        textColor = text.color;
        disappearSpeed = 3f;
    }

    public void SetMessage(string message)
    {
        text.text = message;
    }

    private void Update()
    {
        float moveYSpeed = 1f;
        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0f)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if(textColor.a <= 0f) Destroy(gameObject);
        }
    }
}
