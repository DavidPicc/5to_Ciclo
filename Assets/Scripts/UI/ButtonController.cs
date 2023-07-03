using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public List<Image> targetImages;
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
        BackButton.interactable = !targetImages[0].enabled;
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
            targetImages[currentIndex].enabled = false;
            disabledIndices.Push(currentIndex);

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
            targetImages[previousIndex].enabled = true;
            currentIndex = previousIndex;

            if (currentIndex == 0)
            {
                BackButton.interactable = false;
            }
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
