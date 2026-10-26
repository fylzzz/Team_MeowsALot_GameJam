using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float boostForce = 40f;
    public Vector3 boostDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        player.AddWind(boostDirection * boostForce);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, boostDirection * 10f);
    }
}
