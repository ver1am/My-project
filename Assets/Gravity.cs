using UnityEngine;

public class GravityPlanet : MonoBehaviour
{
    private Rigidbody rb;

    GameObject[] planets;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    void FixedUpdate() {
        Vector3 totalg = Vector3.zero;

        foreach (GameObject pl in planets) 
        {
            if (pl == null) continue;

            Planet plcmp = pl.GetComponent<Planet>();
            if (plcmp == null) continue;

            Vector3 offset = pl.transform.position - transform.position;
            float distance = offset.magnitude;
            if (distance < 0.001f) continue;

            Vector3 gravityDirection = offset / distance;
            float currentGravity = plcmp.gravity / Mathf.Max(distance,1f); // Аркадная физика

            totalg += gravityDirection * currentGravity;
        }

        if (totalg == Vector3.zero) return;

        rb.AddForce(totalg, ForceMode.Acceleration);

        Vector3 maingdir = totalg.normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -maingdir) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, 5f * Time.fixedDeltaTime);
    }
}