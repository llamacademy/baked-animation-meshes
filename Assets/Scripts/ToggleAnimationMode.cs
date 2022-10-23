using UnityEngine;

public class ToggleAnimationMode : MonoBehaviour
{
    private bool AnimatorsActive = true;
    [SerializeField]
    private AnimatorController SMRController;
    [SerializeField]
    private AnimatedMeshController ThrottledController;

    private void Start()
    {
        ThrottledController.DeactivateAll();
        SMRController.ActivateAll();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2f - 100, 10, 200, 30), $"Use {(AnimatorsActive ? "Mesh" : "Skinned")} Animators"))
        {
            AnimatorsActive = !AnimatorsActive;
            if (AnimatorsActive)
            {
                SMRController.ActivateAll();
                ThrottledController.DeactivateAll();
            }
            else
            {
                SMRController.DeactivateAll();
                ThrottledController.ActivateAll();
            }
        }
    }
}
