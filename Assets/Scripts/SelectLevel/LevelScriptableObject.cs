using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "NewLevel")]
public class LevelScriptableObject : ScriptableObject
{
    public string levelName;
    public string sceneName;
}
