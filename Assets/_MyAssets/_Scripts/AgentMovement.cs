using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Transform island;
    [SerializeField] private float loadDistance  = 0.75f;
    [SerializeField] private GameObject targetSprite;
    [SerializeField] private Transform shadow;
    private Vector3 targetPosition;
    private Vector3 camOffset;
    private Camera mainCam;

    private void Awake()
    {
        targetPosition = transform.position; // Stop plane from moving until target set
        mainCam = Camera.main;
        camOffset = mainCam.transform.position; // Store offset from camera and scene(world origin)
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition - camOffset); 
            targetSprite.transform.position = targetPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        LookAt2D(targetPosition);
        
        if((island.position - transform.position).magnitude <= loadDistance)
            SceneLoader.LoadSceneByIndex(2);
        
        DebunkFlatEarthTheorists();
    }

    void LookAt2D(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void DebunkFlatEarthTheorists()
    {
        shadow.position = transform.position + new Vector3(-0.26f, -0.2f, 0);
    }
    
}
