using PlayerContent;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] private GrassInteractor _grassInteractor;

    public void SwingHit()
    {
        // Debug.Log("Swing hit");
        _grassInteractor.HandleGrassCut();
    }

    public void StartSwing()
    {
        // Debug.Log("Start swing");
        _grassInteractor.SetValueCanCut(false);
    }

    public void EndSwing()
    {
        // Debug.Log("End swing");
        _grassInteractor.SetValueCanCut(true);
    }
}