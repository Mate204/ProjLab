using UnityEngine;

public class PlayerTeleportControl : MonoBehaviour
{
    public float cooldown = 1f;
    public bool canTeleport = true;

    public void TriggerCoolDown()
    {
        //if (canTeleport)
        //{
        //    StartCoroutine(CoolDownRoutine());
        //}
        //ignore cooldown in current setup
    }

    private System.Collections.IEnumerator CoolDownRoutine()
    {
        canTeleport = false;
        yield return new WaitForSeconds(cooldown);
        canTeleport = true;
    }

}
