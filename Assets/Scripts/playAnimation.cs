using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{

  public AnimationClip jump;
  Animation anim;

  void Awake()
  {
    anim = GetComponent<Animation>();
    anim.clip = jump;
  }

  public void PlayJumpAnimation()
  {
    anim.Play();
  }
}
