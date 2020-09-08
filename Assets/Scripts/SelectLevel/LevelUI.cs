using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public LevelScriptableObject levelScriptableObject;

    [SerializeField] private Text levelText;

    private void Awake()
    {
        levelText.text = levelScriptableObject.levelName;
    }
}
