using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Button _selectButton;
    [SerializeField] UnityEvent _selectMethod;
    [SerializeField] Transform _buttonsParent;
    public Button[] levelsButtons;

    private void Start()
    {
        LevelUnlock.Instance.CheckUnlockedLevels();
        //Debug.Log("checking");

        if (LevelUnlock.Instance.levels > 0)
        {
            var button = Instantiate(_selectButton, _buttonsParent);
            button.transform.SetSiblingIndex(1);
            button.onClick.AddListener(() => InvokeSelectMethod());
            button.interactable = true;
        }
        else
            _selectButton.interactable = false;

        foreach(Button button in levelsButtons)
        {
            button.interactable = false;
            Color transparency = button.GetComponentInChildren<TextMeshProUGUI>().color;
            transparency.a = 0.1f;
            button.GetComponentInChildren<TextMeshProUGUI>().color = transparency;
        }
        LevelUnlock.Instance.UnlockLevels();
    }

    public void InvokeSelectMethod()
    {
        _selectMethod.Invoke();
    }

    public void LoadingUnlockedLevel(string levelName)
    {
        LevelUnlock.Instance.loadedUnlockedLevel = true;
        //Debug.Log("Loading unlocked level.");
        SceneManager.LoadScene(levelName);
    }
}
