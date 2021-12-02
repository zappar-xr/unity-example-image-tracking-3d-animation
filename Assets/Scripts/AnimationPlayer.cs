using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Animation m_animation;
    [SerializeField]
    private AnimationClip m_clip;

    void Start()
    {
        if (m_animation != null)
            m_animation.clip = m_clip;
    }

    public void PlayAnimation()
    {
        if (m_animation != null && m_animation.gameObject.activeSelf)
            m_animation.Play();
    }

}
