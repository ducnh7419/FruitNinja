using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Subject
{
    private bool isSlicing;
    [SerializeField] private Blade bladePrefabs;
    private Blade blade;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSlicing();
        }
        else if (isSlicing)
        {
            OnSlicing();
        }
    }

    

    private void StartSlicing()
    {
        isSlicing = true;
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 9f;
        Vector3 bladePos = GameManager.Instance.M_Camera.ScreenToWorldPoint(screenPosition);
        // bladePos.z=0;
        if (blade == null)
        {
            blade = SimplePool.Spawn<Blade>(bladePrefabs, bladePos, bladePrefabs.TF.rotation);
            blade.OnInit();
        }
    }

    private void EndSlicing()
    {
        
        isSlicing = false;
        SimplePool.Despawn(blade);
        blade=null;
    }

    private void OnSlicing()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 9f;
        Vector3 bladePos = GameManager.Instance.M_Camera.ScreenToWorldPoint(screenPosition);
        blade.Direction = (bladePos - blade.TF.position);
        blade.MoveBlade(bladePos);
    }

}
