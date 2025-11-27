using UnityEngine;

public class PathNode : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.azure;
        Gizmos.DrawCube(transform.position, Vector3.one*0.3f);
    }
}
