using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingGun : MonoBehaviour
{

    public Image Reticle;
    public Color ReticleColorOn;
    public Color ReticleColorOff;

    LineRenderer lr;
    Vector3 grapplePoint;
    public LayerMask IsGrappleable;

    public Transform gunTip;
    public Transform cam;
    public Transform player;
    SpringJoint joint;

    public float maxDistance = 100f;

    public float jmaxDistance, jminDistance, jspring, jdamper, jscale;

    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }


        RaycastHit hit;
        if (Physics.Raycast(origin: cam.position, direction: cam.forward, out hit, maxDistance, IsGrappleable))
        {
            Reticle.color = ReticleColorOn;
        }
        else
        {
            Reticle.color = ReticleColorOff;
        }
    }

    private void LateUpdate()
    {
        DrawRope();

    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin: cam.position, direction: cam.forward, out hit, maxDistance, IsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(a: player.position, b: grapplePoint);

            //mess with these until we like them
            joint.maxDistance = distanceFromPoint * jmaxDistance;
            joint.minDistance = distanceFromPoint * jminDistance;
            joint.spring = jspring;
            joint.damper = jdamper;
            joint.massScale = jscale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }


    private Vector3 currentGrapplePosition;


    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);


        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
}
