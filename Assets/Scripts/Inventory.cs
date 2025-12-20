using AudioContent;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip _collectResourceClip;

    public void AddResource()
    {
        AudioService.Instance.PlayClip(_collectResourceClip);
    }
}