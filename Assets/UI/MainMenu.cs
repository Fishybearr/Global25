using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public float popAnimationDuration = 0.1f;
    
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
        PlayButtonPopAnimation(_playButton);
    }
    
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options Button clicked");
        ButtonPopAnimation(_optionsButton);
    }

    private void ButtonPopAnimation(Button button)
    {
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("scale"));
        properties.Add(new StylePropertyName("opacity"));
        properties.Add(new StylePropertyName("visibility"));
        button.style.transitionProperty = new StyleList<StylePropertyName>(properties);
        
        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(popAnimationDuration, TimeUnit.Second));
        button.style.transitionDuration = new StyleList<TimeValue>(durations);
        
        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.EaseOut));
        button.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        
        button.style.scale = new StyleScale(new Vector2(1.3f, 1.3f));
        button.style.opacity = 0;
        button.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
    }

    private void PlayButtonPopAnimation(Button button)
    {
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("visibility"));
        _ui.style.transitionProperty = new StyleList<StylePropertyName>(properties);
        
        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(popAnimationDuration, TimeUnit.Second));
        _ui.style.transitionDuration = new StyleList<TimeValue>(durations);
        
        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.EaseOut));
        button.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _ui.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
        
        ButtonPopAnimation(button);
    }

    private void OnCreditsButtonClicked()
    {
        Debug.Log("Credits Button clicked");
        ButtonPopAnimation(_creditsButton);
    }
    
    private void OnQuitButtonClicked()
    {
        ButtonPopAnimation(_quitButton);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
