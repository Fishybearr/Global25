using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement _ui;

    private Button _playButton;
    private Button _optionsButton;
    private Button _creditsButton;
    private Button _quitButton;
    
    private void Awake()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        _playButton = _ui.Q<Button>("PlayButton");
        _playButton.clicked += OnPlayButtonClicked;
        
        _optionsButton = _ui.Q<Button>("OptionsButton");
        _optionsButton.clicked += OnOptionsButtonClicked;
        
        _creditsButton = _ui.Q<Button>("CreditsButton");
        _creditsButton.clicked += OnCreditsButtonClicked;
        
        _quitButton = _ui.Q<Button>("QuitButton");
        _quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        _ui.visible = false;
    }
    
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options Button clicked");
    }

    private void OnCreditsButtonClicked()
    {
        Debug.Log("Credits Button clicked");
    }
    
    private void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
