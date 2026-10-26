using UnityEngine;

public class BoostPad : MonoBehaviour
{

    public float boostForce = 40f;
    public Transform boostDirection;

    private void Reset()
    {
       GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        Vector3 dir = (boostDirection != null ? boostDirection : transform).forward;
        player.AddImpulse(dir.normalized * boostForce);
    }

    public void OnDrawGizmos()
    {
        Transform t = boostDirection != null ? boostDirection : transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(t.position, t.forward * 3f);
    }
}
