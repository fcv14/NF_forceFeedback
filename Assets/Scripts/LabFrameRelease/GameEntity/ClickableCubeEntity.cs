using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCubeEntity : GameEntityBase
{
    public Color32 Color_original;
    public bool Bool_firstClicked = false;

    public static Color32 Color_clicked;
    GameObject obj_first_selected;

    public bool isOnClick = false;

    private void Start()
    {
        Color_original = gameObject.GetComponent<Renderer>().material.color;
    }

    

    public void ClickMe()
    {
        //若是第一個選的
        if (BuildTask.List_selectedCubes.Count == 0 && Bool_firstClicked != true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color_clicked;
            Bool_firstClicked = true;
            BuildTask.List_selectedCubes.Add(this);
            BuildTask.List_wallCubes.Remove(this);
            obj_first_selected = this.gameObject;
        }
        //第一個之後選的
        else
        {
            for (int i = 0; i < BuildTask.List_selectedCubes.Count; i++)
            {
                //距離為1以內的才可以選取
                if ((this.gameObject.transform.position - BuildTask.List_selectedCubes[i].transform.position).magnitude == 1 && Bool_firstClicked != true)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color_clicked;
                    Bool_firstClicked = true;
                    BuildTask.List_selectedCubes.Add(this);
                    BuildTask.List_wallCubes.Remove(this);
                }
            }
        }
    }

    public void UnClickMe()
    {
        gameObject.GetComponent<Renderer>().material.color = Color_original;
        Bool_firstClicked = false;
        BuildTask.List_selectedCubes.Remove(this);
        BuildTask.List_wallCubes.Add(this);
    }




public override void EntityDispose()
    {
        throw new System.NotImplementedException();
    }
}
