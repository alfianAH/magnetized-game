using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public LevelScriptableObject levelScriptableObject;

    [SerializeField] private Text levelText;
    [SerializeField] private DetailsManager detailsManager;
    
    private void Awake()
    {
        levelText.text = levelScriptableObject.levelName;
    }

    public void SetDetailsUi()
    {
        detailsManager.levelScriptableObject = levelScriptableObject;
        detailsManager.SeeDetails = true;
        detailsManager.IsDetailsTouch = true;
    }
}
