using PlayerContent;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] private ResourceInteractor _resourceInteractor;

    public void SwingHit() => _resourceInteractor.HandleGrassCut();

    public void EndSwing() => _resourceInteractor.SetValueCanCut(true);
}