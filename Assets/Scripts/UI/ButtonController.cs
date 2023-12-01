using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public List<Image> targetImages;
    public List<TextMeshProUGUI> targetText;
    public Button NextButton;
    public Button BackButton;
    public Button SkipButton;
    public string nextSceneName;

    private int currentIndex = 0;
    private Stack<int> disabledIndices = new Stack<int>();

    private void Start()
    {
        NextButton.onClick.AddListener(DisableNextImage);
        BackButton.onClick.AddListener(EnablePreviousImages);
        SkipButton.onClick.AddListener(ChangeScene);
        BackButton.interactable = !targetImages[0].gameObject.activeSelf;
        BackButton.interactable = !targetText[0].gameObject.activeSelf;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisableNextImage();
          
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EnablePreviousImages();

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeScene();
        }
    }

    private void DisableNextImage()
    {
        if (currentIndex < targetImages.Count)
        {
            targetImages[currentIndex].gameObject.SetActive(false);
            disabledIndices.Push(currentIndex);
            DisableNextText();

            if (currentIndex == 0)
            {
                BackButton.interactable = true;
            }

            currentIndex++;

            if (currentIndex >= targetImages.Count)
            {
                ChangeScene();
            }
        }
    }

    private void EnablePreviousImages()
    {
        if (disabledIndices.Count > 0)
        {
            int previousIndex = disabledIndices.Pop();
            targetImages[previousIndex].gameObject.SetActive(true);
            currentIndex = previousIndex;

            EnablePreviousText();

            if (currentIndex == 0)
            {
                BackButton.interactable = false;
            }
        }
    }

    private void DisableNextText()
    {
        if (currentIndex < targetText.Count)
        {
            targetText[currentIndex].gameObject.SetActive(false);
            disabledIndices.Push(currentIndex);
        }
    }

    private void EnablePreviousText()
    {
        if (disabledIndices.Count > 0)
        {
            int previousIndex = disabledIndices.Pop();
            targetText[previousIndex].gameObject.SetActive(true);
            currentIndex = previousIndex;
        }
    }

    private void ChangeScene()
    {
        ClearPlayerPrefs();
        SceneManager.LoadScene(nextSceneName);
    }

    void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        if (FindObjectOfType<LevelUnlock>() != null) PlayerPrefs.SetInt("LevelsUnlocked", LevelUnlock.Instance.levels);
    }
}
