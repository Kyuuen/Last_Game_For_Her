using Cysharp.Threading.Tasks;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using ZBase.UnityScreenNavigator.Core.Activities;

public class StartSceenController : MonoBehaviour, IController, IPointerClickHandler
{
    [SerializeField] GameObject stText;
    [SerializeField] GameObject ndText;
    private int status;

    void Awake()
    {
        status = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        status++;
        switch (status)
        {
            case 0:
                {
                    stText.SetActive(true);
                    ndText.SetActive(false);
                    AudioManager.Instance.PlayClickSound();
                    break;
                }
            case 1:
                {
                    stText.SetActive(false);
                    ndText.SetActive(true);
                    AudioManager.Instance.PlayClickSound();
                    break;
                }
            case 2:
                {
                    ndText.SetActive(false);
                    AudioManager.Instance.PlayClickSound();
                    AudioManager.Instance.PlayTheme();
                    GameController.currentLevel = 1;
                    SceneManager.LoadScene("Level" + GameController.currentLevel);
                    break;
                }
        }
    }

    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return SadnessKnightAdventure.Interface;
    }
}
