using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerEntity : GameEntityBase
{
    [SerializeField] private LayerMask Layer;

    private void Start()
    {
        Layer = LayerMask.GetMask("clickableLayer");
    }
    
    private void Update()//滑鼠編輯Task
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, Layer))
            {
                Debug.Log("mouse left");
                rayHit.transform.GetComponent<ClickableCubeEntity>().ClickMe();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, Layer))
            {
                Debug.Log("mouse right");
                rayHit.collider.GetComponent<ClickableCubeEntity>().UnClickMe();
            }
        }

    }




    public override void EntityDispose()
    {
        throw new System.NotImplementedException();
    }


}
