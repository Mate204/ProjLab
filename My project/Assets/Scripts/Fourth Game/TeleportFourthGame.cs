using UnityEngine;

public class TeleportFourthGame : MonoBehaviour
{
    public Transform otherGate;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTeleportControl control = other.GetComponent<PlayerTeleportControl>();
            if (control != null && control.canTeleport)
            {
                Vector3 localPos = transform.InverseTransformPoint(other.transform.position);
                Vector3 newWorldPos = otherGate.TransformPoint(localPos);

                other.transform.position = newWorldPos;
                control.TriggerCoolDown();

              
            }
        }
    }
    
}
