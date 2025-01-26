using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    public float popAnimationDuration = 0.1f;

    public AudioClip[] popSounds;
    
    private VisualElement _ui;

    private Label _title;

    private Button _playButton;
    private Button _optionsButton;
    private Button _optionsXButton;
    
    private Button _creditsButton;
    private Button _creditsXButton;
    
    private Button _quitButton;
    
    private AudioSource _audio;
    
    public GlobalSettings settings;
    public Camera cam;
    public GameObject player;

    private bool _menuVisible = true;

    public void ToggleMenu()
    {
        if (_menuVisible)
        {
            _menuVisible = false;
            
            Physics.simulationMode = SimulationMode.FixedUpdate;
            SetVisibility(DisplayStyle.None, new VisualElement[]{_title, _playButton, _optionsButton, _creditsButton, _quitButton});
            _ui.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
            
            cam.GetComponent<CinemachineBrain>().enabled = true;
            player.GetComponent<TPCharacterController>().enabled = true;
        }
        else
        {
            _menuVisible = true;
            Physics.simulationMode = SimulationMode.Script;
            cam.GetComponent<CinemachineBrain>().enabled = false;
            player.GetComponent<TPCharacterController>().enabled = false;

            _playButton.style.transitionProperty = StyleKeyword.Null;
            _playButton.style.transitionTimingFunction = StyleKeyword.Null;
            _playButton.style.transitionDuration = StyleKeyword.Null;
            
            _playButton.style.opacity = 1.0f;
            _playButton.style.scale = StyleKeyword.Null;
            _playButton.style.visibility = Visibility.Visible;
            
            SetVisibility(DisplayStyle.Flex, new VisualElement[]{_title, _playButton, _optionsButton, _creditsButton, _quitButton});
            _ui.style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
        }
    }
    
    private void Awake()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _title = _ui.Q<Label>("Title");
        _playButton = _ui.Q<Button>("PlayButton");
        _playButton.clicked += OnPlayButtonClicked;
        _playButton.RegisterCallback<TransitionEndEvent>(OnPopTransitionEnd);
        _playButton.focusable = true;
        _playButton.Focus();

        _optionsButton = _ui.Q<Button>("OptionsButton");
        _optionsButton.clicked += OnOptionsButtonClicked;
        _optionsButton.RegisterCallback<TransitionEndEvent>(OnPopTransitionEnd);
        _optionsButton.focusable = true;

        _optionsXButton = _ui.Q<Button>("OptionsX");
        _optionsXButton.clicked += OnOptionsClosed;
        _optionsXButton.focusable = true;

        _creditsButton = _ui.Q<Button>("CreditsButton");
        _creditsButton.clicked += OnCreditsButtonClicked;
        _creditsButton.RegisterCallback<TransitionEndEvent>(OnPopTransitionEnd);
        _creditsButton.focusable = true;

        _creditsXButton = _ui.Q<Button>("CreditsX");
        _creditsXButton.clicked += OnCreditsClosed;
        _creditsXButton.focusable = true;

        _quitButton = _ui.Q<Button>("QuitButton");
        _quitButton.clicked += OnQuitButtonClicked;
        _quitButton.RegisterCallback<TransitionEndEvent>(OnPopTransitionEnd);

        _ui.Q<Slider>("MusicVolume").dataSource = settings;
        _ui.Q<Slider>("SfxVolume").dataSource = settings;

        cam.GetComponent<CinemachineBrain>().enabled = false;
        player.GetComponent<TPCharacterController>().enabled = false;

        _ui.RegisterCallback<NavigationCancelEvent>(OnCancelEvent);
    }

    private void OnCancelEvent(NavigationCancelEvent evt)
    {
        Debug.Log(_ui.focusController.focusedElement);
        if (_ui.focusController.focusedElement.Equals(_creditsButton))
        {
            OnCreditsClosed();
        }
        else if(_ui.focusController.focusedElement.Equals(_optionsButton))
        {
            OnOptionsClosed();
        }
    }

    private void OnPlayButtonClicked()
    {
        ButtonPopAnimation(_playButton);
    }
    
    private void OnOptionsButtonClicked()
    {
        _optionsButton.text = "";
        GrowBubbleAnimation(_optionsButton);
        _optionsXButton.style.display = DisplayStyle.Flex;
        
        SetVisibility(DisplayStyle.None, new VisualElement[]{_title, _playButton, _creditsButton, _quitButton});

        foreach (VisualElement child in _optionsButton.Children())
        {
            child.style.display = DisplayStyle.Flex;
            child.focusable = true;
        }

        _optionsButton.Children().First().Focus();
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

    private void OnCreditsButtonClicked()
    {
        _creditsButton.text = "Main Dev Team:\nAaron Radliff\nJeremiah Opare\nRae Benkovich\nWolfgang Groetz\n\nMusic:\nGarrison Bouchard-Ferdon\n\nFont:\nDynaPuff Font from Google Fonts";
        
        GrowBubbleAnimation(_creditsButton);
        
        _creditsXButton.style.display = DisplayStyle.Flex;
        _creditsXButton.focusable = true;
        SetVisibility(DisplayStyle.None, new VisualElement[]{_title, _playButton, _optionsButton, _quitButton});
    }

    private void GrowBubbleAnimation(Button button)
    {
        List<StylePropertyName> properties = new List<StylePropertyName>();
        properties.Add(new StylePropertyName("width"));
        properties.Add(new StylePropertyName("height"));
        properties.Add(new StylePropertyName("scale"));
        button.style.transitionProperty = new StyleList<StylePropertyName>(properties);

        List<TimeValue> durations = new List<TimeValue>();
        durations.Add(new TimeValue(0.5f, TimeUnit.Second));
        button.style.transitionDuration = new StyleList<TimeValue>(durations);

        List<EasingFunction> easingFunctions = new List<EasingFunction>();
        easingFunctions.Add(new EasingFunction(EasingMode.EaseOut));
        button.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);

        button.style.width = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        button.style.height = new StyleLength(new Length(90.0f, LengthUnit.Percent));
        button.style.scale = new StyleScale(new Vector2(1.0f, 1.0f));
    }

    private void OnOptionsClosed()
    {
        ButtonPopAnimation(_optionsButton);
        Debug.Log(_ui.focusController.focusedElement);
    }
    
    private void OnCreditsClosed()
    {
        ButtonPopAnimation(_creditsButton);
    }

    private void SetVisibility(DisplayStyle displayStyle, VisualElement[] elements)
    {
        foreach (VisualElement element in elements)
        {
            element.style.display = displayStyle;
        }
    }

    private void PlayPopSound()
    {
        _audio.PlayOneShot(popSounds[Random.Range(0, popSounds.Length)], settings.sfxVolume);
    }

    private void OnPopTransitionEnd(TransitionEndEvent endEvent)
    {
        if (endEvent.stylePropertyNames.Contains("opacity"))
        {
            PlayPopSound();
            
            Button tempButton = endEvent.currentTarget.ConvertTo<Button>();
            if (tempButton.Equals(_playButton) && _menuVisible)
            {
                Debug.Log("From Callback");
                ToggleMenu();
                return;
            }
            
            tempButton.text = endEvent.target.ToString().Substring(7, endEvent.target.ToString().IndexOf('B', 7) - 7);
            
            List<TimeValue> durations = new List<TimeValue>();
            durations.Add(new TimeValue(0.0f, TimeUnit.Second));
            tempButton.style.transitionDuration = new StyleList<TimeValue>(durations);

            List<EasingFunction> easingFunctions = new List<EasingFunction>();
            easingFunctions.Add(new EasingFunction(EasingMode.Ease));
            
            tempButton.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
            tempButton.style.opacity = 1.0f;
            tempButton.style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
            tempButton.style.scale = StyleKeyword.Null;
            tempButton.style.width = new StyleLength(new Length(20.0f, LengthUnit.Percent));
            tempButton.style.height = new StyleLength(new Length(12.0f, LengthUnit.Percent));

            if (tempButton.Equals(_creditsButton))
            {
                _creditsXButton.style.display = DisplayStyle.None;
            }
            else if(tempButton.Equals(_optionsButton))
            {
                foreach (VisualElement child in _optionsButton.Children())
                {
                    child.style.display = DisplayStyle.None;
                }
            }

            List<VisualElement> otherButtons = new List<VisualElement>
                { _title, _playButton, _optionsButton, _creditsButton, _quitButton };
            
            otherButtons.Remove(tempButton);
            
            SetVisibility(DisplayStyle.Flex, otherButtons.ToArray());
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
