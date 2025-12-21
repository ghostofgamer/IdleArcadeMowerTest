using System;
using PlayerContent;
using UnityEngine;

public class ImproverZoneTrigger : MonoBehaviour
{
   public event Action Triggered;
   
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Triggered");

      if (other.TryGetComponent(out Player player))
      {
         Debug.Log("Triggered Player");
         Triggered?.Invoke();
      }
   }
}
