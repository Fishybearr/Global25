using System.Collections.Generic;
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

        _title.style.display = DisplayStyle.None;
        _playButton.style.display = DisplayStyle.None;
        _optionsButton.style.display = DisplayStyle.None;
        _quitButton.style.display = DisplayStyle.None;

        _creditsButton.text = "Main Dev Team:\nAaron Radliff\nJeremiah Opare\nRae Benkovich\nWolfgang Groetz\n\nMusic:\nGarrison Bouchard-Ferdon\n\nFont:\nDynaPuff Font from Google Fonts";
        
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("width"));
        properties.Add(new StylePropertyName("height"));
        properties.Add(new StylePropertyName("text"));
        _creditsButton.style.transitionProperty = new StyleList<StylePropertyName>(properties);

        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(0.5f, TimeUnit.Second));
        _creditsButton.style.transitionDuration = new StyleList<TimeValue>(durations);

        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.Ease));
        _creditsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);

        _creditsButton.style.width = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        _creditsButton.style.height = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        
        List<StylePropertyName> properties2 = new List<StylePropertyName>();
        properties2.Add(new StylePropertyName("display"));
        _creditsXButton.style.transitionProperty = new StyleList<StylePropertyName>(properties2);
        _creditsXButton.style.transitionDuration = new StyleList<TimeValue>(durations);
        _creditsXButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _creditsXButton.style.display = DisplayStyle.Flex;
        
        List<TimeValue> durations2 = new List<TimeValue>();
        durations2.Add(new TimeValue(buttonDisableDuration, TimeUnit.Second));
        
        _title.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _title.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _title.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _title.style.display = DisplayStyle.None;
        
        _playButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _playButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _playButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _playButton.style.display = DisplayStyle.None;
        
        _optionsButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _optionsButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _optionsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _optionsButton.style.display = DisplayStyle.None;
        
        _quitButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _quitButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _quitButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _quitButton.style.display = DisplayStyle.None;
    }

    private void OnCreditsClosed()
    {
        Debug.Log("Credits Closed");

        _creditsButton.text = "Credits";
        
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("scale"));
        properties.Add(new StylePropertyName("width"));
        properties.Add(new StylePropertyName("height"));
        _creditsButton.style.transitionProperty = new StyleList<StylePropertyName>(properties);

        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(0.5f, TimeUnit.Second));
        _creditsButton.style.transitionDuration = new StyleList<TimeValue>(durations);

        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.Ease));
        _creditsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);

        _creditsButton.style.width = new StyleLength(new Length(20.0f, LengthUnit.Percent));
        _creditsButton.style.height = new StyleLength(new Length(12.0f, LengthUnit.Percent));
        
        List<StylePropertyName> properties2 = new List<StylePropertyName>();
        properties2.Add(new StylePropertyName("display"));
        _creditsXButton.style.transitionProperty = new StyleList<StylePropertyName>(properties2);
        _creditsXButton.style.transitionDuration = new StyleList<TimeValue>(durations);
        _creditsXButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _creditsXButton.style.display = DisplayStyle.None;
        
        List<TimeValue> durations2 = new List<TimeValue>();
        durations2.Add(new TimeValue(buttonDisableDuration, TimeUnit.Second));
        
        _title.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _title.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _title.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _title.style.display = DisplayStyle.Flex;
        
        _playButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _playButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _playButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _playButton.style.display = DisplayStyle.Flex;
        
        _optionsButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _optionsButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _optionsButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _optionsButton.style.display = DisplayStyle.Flex;
        
        _quitButton.style.transitionProperty  = new StyleList<StylePropertyName>(properties2);
        _quitButton.style.transitionDuration = new StyleList<TimeValue>(durations2);
        _quitButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
        _quitButton.style.display = DisplayStyle.Flex;
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
