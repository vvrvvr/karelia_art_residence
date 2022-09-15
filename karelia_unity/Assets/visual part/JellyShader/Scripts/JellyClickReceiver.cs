using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyClickReceiver : MonoBehaviour {

    RaycastHit hit;
    Ray clickRay;

    //Renderer modelRenderer;
    private SkinnedMeshRenderer modelRenderer;
    float controlTime;

	// Use this for initialization
	void Start () {
        modelRenderer = GetComponent<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        controlTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            controlTime = 0;
            //clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(clickRay, out hit))
            //{
            //    controlTime = 0;

            //    modelRenderer.material.SetVector("_ModelOrigin", transform.position);
            //    modelRenderer.material.SetVector("_ImpactOrigin", hit.point);
            //}
        }

        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}
}
