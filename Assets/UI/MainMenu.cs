using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public float popAnimationDuration = 0.1f;
    public float buttonDisableDuration = 0.1f;
    
    private VisualElement _ui;

    private Label _title;

    private Button _playButton;
    private Button _optionsButton;
    private Button _creditsButton;
    private Button _creditsXButton;
    
    private Button _quitButton;
    
    private void Awake()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        _title = _ui.Q<Label>("Title");
        _playButton = _ui.Q<Button>("PlayButton");
        _playButton.clicked += OnPlayButtonClicked;
        
        _optionsButton = _ui.Q<Button>("OptionsButton");
        _optionsButton.clicked += OnOptionsButtonClicked;
        
        _creditsButton = _ui.Q<Button>("CreditsButton");
        _creditsButton.clicked += OnCreditsButtonClicked;
        
        _creditsXButton = _ui.Q<Button>("CreditsX");
        _creditsXButton.clicked += OnCreditsClosed;
        _creditsButton.RegisterCallback<TransitionEndEvent>(OnCreditsPopTransitionEnd);
        
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

        _creditsButton.text = "Main Dev Team:\nAaron Radliff\nJeremiah Opare\nRae Benkovich\nWolfgang Groetz\n\nMusic:\nGarrison Bouchard-Ferdon\n\nFont:\nDynaPuff Font from Google Fonts";
        
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("width"));
        properties.Add(new StylePropertyName("height"));
        _creditsButton.style.transitionProperty = new StyleList<StylePropertyName>(properties);

        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(0.5f, TimeUnit.Second));
        _creditsButton.style.transitionDuration = new StyleList<TimeValue>(durations);

        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.EaseIn));
        _creditsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);

        _creditsButton.style.width = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        _creditsButton.style.height = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        
        _creditsXButton.style.display = DisplayStyle.Flex;
        SetVisibility(DisplayStyle.None, new VisualElement[]{_title, _playButton, _optionsButton, _quitButton});
    }

    private void OnCreditsClosed()
    {
        Debug.Log("Credits Closed");
        
        ButtonPopAnimation(_creditsButton);
    }

    private void SetVisibility(DisplayStyle displayStyle, VisualElement[] elements)
    {
        foreach (VisualElement element in elements)
        {
            element.style.display = displayStyle;
        }
    }

    private void OnCreditsPopTransitionEnd(TransitionEndEvent endEvent)
    {
        if (endEvent.target == _creditsButton && endEvent.stylePropertyNames.Contains("opacity") && _creditsButton.style.opacity.value < 1.0f)
        {
            Debug.Log(endEvent.target.ToString());
            _creditsButton.text = "Credits";
            
            List<TimeValue> durations = new List<TimeValue>();
            durations.Add(new TimeValue(0.0f, TimeUnit.Second));
            _creditsButton.style.transitionDuration = new StyleList<TimeValue>(durations);

            List<EasingFunction> easingFunctions = new List<EasingFunction>();
            easingFunctions.Add(new EasingFunction(EasingMode.Ease));
            
            _creditsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
            _creditsButton.style.opacity = 1.0f;
            _creditsButton.style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
            _creditsButton.style.scale = new StyleScale(new Vector2(1.0f, 1.0f));
            _creditsButton.style.width = new StyleLength(new Length(20.0f, LengthUnit.Percent));
            _creditsButton.style.height = new StyleLength(new Length(12.0f, LengthUnit.Percent));
            
            _creditsXButton.style.display = DisplayStyle.None;
            SetVisibility(DisplayStyle.Flex, new VisualElement[]{_title, _playButton, _optionsButton, _quitButton});
        }
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
