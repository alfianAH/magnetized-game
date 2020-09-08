using UnityEngine;
using UnityEngine.UI;

public class DetailsManager : MonoBehaviour
{
    public LevelScriptableObject levelScriptableObject;
    [SerializeField] private Text levelName;

    private Animator animator;

    private bool isDetailsTouch, seeDetails;

    public bool IsDetailsTouch
    {
        get => isDetailsTouch;
        set => isDetailsTouch = value;
    }

    public bool SeeDetails
    {
        get => seeDetails;
        set => seeDetails = value;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if(levelScriptableObject && seeDetails)
        {
            seeDetails = false;
            levelName.text = levelScriptableObject.levelName;

            // Play animation
            animator.SetBool("isAppear", true);
        }
        
        // If player touch objects other than detailsContainer, ...
        if (!isDetailsTouch)
        {
            // Hide details
            animator.SetBool("isAppear", false); 
        }
    }

    /// <summary>
    /// PlayBtn in Details
    /// </summary>
    /// <param name="sceneLoader"></param>
    public void Play(SceneLoader sceneLoader)
    {
        if (sceneLoader)
        {
            sceneLoader.LoadScene(levelScriptableObject.sceneName);
        }
    }
}
