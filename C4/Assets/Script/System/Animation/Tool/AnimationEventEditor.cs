using UnityEngine;
#if UNITY_EDITOR
using System.Collections;
using UnityEditor;

public class AnimationEventEditor : BaseAnimationWindow
{

	public string assetPath;

    override public void Awake()
    {

        base.Awake();
        property = new AnimationEditorProperty();

        AnimationListWindow animationListWindow = transform.gameObject.AddComponent<AnimationListWindow>();
        AnimationEventListWindow animationEventListWindow = transform.gameObject.AddComponent<AnimationEventListWindow>();
        AnimationEventPropertyWindow animationEventPropertyWindow = transform.gameObject.AddComponent<AnimationEventPropertyWindow>();
        AnimationPlayerWindow animationPlayerWindow = transform.gameObject.AddComponent<AnimationPlayerWindow>();

        animationListWindow.parentWindow = this;
        animationEventListWindow.parentWindow = animationListWindow;
        animationEventPropertyWindow.parentWindow = animationEventListWindow;
        animationPlayerWindow.parentWindow = animationListWindow;

        property.AddPropertyListener(animationListWindow);
        property.AddPropertyListener(animationEventListWindow);
        property.AddPropertyListener(animationEventPropertyWindow);
        property.AddPropertyListener(animationPlayerWindow);

        Animator animator = GetComponent<Animator>();
        property.Animator = animator;

		AnimationClip[] clips = AnimationUtility.GetAnimationClips (gameObject);

		property.Infos = clips;
    }

    void OnGUI()
    {

    }
}
#endif