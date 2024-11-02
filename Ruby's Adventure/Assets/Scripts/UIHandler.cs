using System;
using UnityEngine;

using UnityEngine.UIElements;


public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    public float displayTime = 3.0f;
    
    private VisualElement _healthBar;
    private VisualElement _dialogue;
    private float _timerDisplay;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        _healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        _dialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        _dialogue.style.display = DisplayStyle.None;
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController != null)
        {
            SetHealthValue(playerController.health / (float)playerController.maxHealth);
        }
    }

    private void Update()
    {
        if (_timerDisplay > 0)
        {
            _timerDisplay -= Time.deltaTime;
        }
        
        if (_timerDisplay <= 0)
        {
            _dialogue.style.display = DisplayStyle.None;
        }
    }


    public void SetHealthValue(float percentage)
    {
        _healthBar.style.width = Length.Percent(100 * percentage);
    }

    public void DisplayDialogue()
    {
        _dialogue.style.display = DisplayStyle.Flex;
        _timerDisplay = displayTime;
    }
}
