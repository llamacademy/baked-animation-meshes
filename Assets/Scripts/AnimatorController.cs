using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator[] Animators;
    private void Awake()
    {
        Animators = FindObjectsOfType<Animator>();
    }

    public void DeactivateAll()
    {
        foreach(Animator animator in Animators)
        {
            animator.enabled = false;
            SkinnedMeshRenderer smr = animator.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr != null)
            {
                smr.enabled = false;
            }
        }
    }

    public void ActivateAll()
    {
        foreach(Animator animator in Animators)
        {
            animator.enabled = true;
            SkinnedMeshRenderer smr = animator.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr != null)
            {
                smr.enabled = true;
            }
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 30), "Run In Place"))
        {
            foreach(Animator animator in Animators)
            {
                animator.Play("Run_S");
            }
        }
        if (GUI.Button(new Rect(10, 45, 200, 25), "Idle"))
        {
            foreach (Animator animator in Animators)
            {
                animator.Play("Idle");
            }
        }
    }
}
