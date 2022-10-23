using UnityEngine;

public class AnimatedMeshController : MonoBehaviour
{
    private AnimatedMesh[] Animators;
    private void Awake()
    {
        Animators = FindObjectsOfType<AnimatedMesh>();
    }

    public void DeactivateAll()
    {
        foreach (AnimatedMesh animator in Animators)
        {
            animator.enabled = false;
            animator.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    public void ActivateAll()
    {
        foreach (AnimatedMesh animator in Animators)
        {
            animator.enabled = true;
            animator.GetComponentInChildren<MeshRenderer>().enabled = true;
            animator.Play("Run_S");
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 30), "Run In Place"))
        {
            foreach (AnimatedMesh animator in Animators)
            {
                animator.Play("Run_S");
            }
        }
        if (GUI.Button(new Rect(10, 45, 200, 25), "Idle"))
        {
            foreach (AnimatedMesh animator in Animators)
            {
                animator.Play("Idle");
            }
        }
    }
}
