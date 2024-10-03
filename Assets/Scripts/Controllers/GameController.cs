using Cysharp.Threading.Tasks;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IController
{
    public static int currentLevel;
    [SerializeField] List<GameObject> enemiesList;
    [SerializeField] GameObject text;
    [SerializeField] string message;
    bool isLoading = false;

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (AllIsNull(enemiesList) && !isLoading)
            {
                isLoading = true;
                text.SetActive(true);
                text.GetComponent<TextPopupController>().SetMessage(message);
                currentLevel++;
                AudioManager.Instance.PlaySFXSound("win");
                await UniTask.WaitForSeconds(2);
                if(currentLevel <= 3)
                {
                    SceneManager.LoadScene("Level" + currentLevel);
                }
                else
                {
                    SceneManager.LoadScene("EndSceen");
                    AudioManager.Instance.StopTheme();
                }
            }
        }
    }

    bool AllIsNull(List<GameObject> enemiesList)
    {
        foreach(GameObject enem in enemiesList)
        {
            if (enem != null) return false;
        }
        return true;
    }

    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return SadnessKnightAdventure.Interface;
    }   
}
