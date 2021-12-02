using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField]
    private Animation m_animation;
    [SerializeField]
    private AnimationClip m_jumpClip;

    void Start()
    {
        if (m_animation != null)
            m_animation.clip = m_jumpClip;
    }

    public void PlayJumpAnimation()
    {
        if (m_animation != null && m_animation.gameObject.activeSelf)
            m_animation.Play();
    }
}
