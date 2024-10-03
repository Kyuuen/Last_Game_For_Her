using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] GameObject exitBtn;

    private async void Awake()
    {
        exitBtn.SetActive(false);
        await UniTask.WaitForSeconds(7);
        exitBtn.SetActive(true);
        exitBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        exitBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
            AudioManager.Instance.PlayClickSound();
        });
    }
}
