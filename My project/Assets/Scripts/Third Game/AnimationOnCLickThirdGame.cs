using UnityEngine;

public class AnimationOnCLickThirdGame : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        Debug.Log("PlayAnimation triggered!");
        animator.SetTrigger("PlayAnimation");
    }

}
