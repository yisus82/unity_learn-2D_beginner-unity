using UnityEngine;

using UnityEngine.UIElements;


public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    
    private VisualElement _healthBar;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        _healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }


    public void SetHealthValue(float percentage)
    {
        _healthBar.style.width = Length.Percent(100 * percentage);
    }


}
