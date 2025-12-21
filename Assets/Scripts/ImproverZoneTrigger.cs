using System;
using PlayerContent;
using UnityEngine;

public class ImproverZoneTrigger : MonoBehaviour
{
   public event Action Triggered;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out Player player))
         Triggered?.Invoke();
   }
}