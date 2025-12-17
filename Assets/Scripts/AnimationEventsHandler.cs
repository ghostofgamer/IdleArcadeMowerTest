using PlayerContent;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] private ResourceInteractor _resourceInteractor;

    public void SwingHit() => _resourceInteractor.HandleGrassCut();

    public void EndSwing() => _resourceInteractor.SetValueCanCut(true);
}