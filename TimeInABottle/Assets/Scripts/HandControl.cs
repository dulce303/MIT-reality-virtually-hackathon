using UnityEngine;
using System.Collections;

public class HandControl : SteamVR_TrackedController
{
    private Transform target;

    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)controllerIndex); } }
    public Vector3 velocity { get { return controller.velocity; } }
    public Vector3 angularVelocity { get { return controller.angularVelocity; } }

    protected LineRenderer lineRenderer;
    protected Vector3[] lineRendererVertices;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        // Initialize our LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.01f, 0.01f);
        lineRenderer.SetVertexCount(2);

        // Initialize our vertex array. This will just contain
        // two Vector3's which represent the start and end locations
        // of our LineRenderer
        lineRendererVertices = new Vector3[2];
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        
        // Update our LineRenderer
        if (lineRenderer && lineRenderer.enabled)
        {
            RaycastHit hit;
            Vector3 startPos = transform.position;

            // If our raycast hits, end the line at that position. Otherwise,
            // just make our line point straight out for 1000 meters.
            // If the raycast hits, the line will be green, otherwise it'll be red.
            if (Physics.Raycast(startPos, transform.forward, out hit, 1000.0f))
            {
                lineRendererVertices[1] = hit.point;
                lineRenderer.SetColors(Color.green, Color.green);
            }
            else
            {
                lineRendererVertices[1] = startPos + transform.forward * 1000.0f;
                lineRenderer.SetColors(Color.red, Color.red);
            }

            lineRendererVertices[0] = transform.position;
            lineRenderer.SetPositions(lineRendererVertices);
        }
    }

    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        base.OnTriggerClicked(e);

        // We want to move the whole [CameraRig] around when we teleport,
        // which should be the parent of this controller. If we can't find the
        // [CameraRig], we can't teleport, so return.
        if (transform.parent == null)
            return;

        RaycastHit hit;
        Vector3 startPos = transform.position;

        // Perform a raycast starting from the controller's position and going 1000 meters
        // out in the forward direction of the controller to see if we hit something to teleport to.
        if (Physics.Raycast(startPos, transform.forward, out hit, 1000.0f))
        {
            transform.parent.position = hit.point;
        }
    }

    public override void OnGripped(ClickedEventArgs e)
    {
        base.OnGripped(e);

        RaycastHit hit;
        
        // Perform a raycast starting from the controller's position and going 1000 meters
        // out in the forward direction of the controller to see if we hit something to teleport to.
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000.0f))
        {
            target = hit.collider.transform;
            target.parent = transform;
        }
    }

    public override void OnUngripped(ClickedEventArgs e)
    {
        base.OnUngripped(e);

        target.parent = null;

        target = null;
    }
}
