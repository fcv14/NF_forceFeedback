using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTask : TaskBase
{
    private ClickableCubeEntity ClickableCube;
    private GamePlayerEntity GamePlayerEntity;
    private Transform spawn_parent;
    
    public static List<ClickableCubeEntity> List_wallCubes = new List<ClickableCubeEntity>();          //牆之方塊List
    public static List<GameObject> List_triggers = new List<GameObject>();           //全部之trigger
    public static List<ClickableCubeEntity> List_selectedCubes = new List<ClickableCubeEntity>();      //被選取方塊



    private List<ClickableCubeEntity> List_CliclableCubes = new List<ClickableCubeEntity>();  //全部方塊

    public override IEnumerator TaskInit()
    {
        ClickableCube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().clickable_prefab;
        spawn_parent = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().spanwPos;
        GamePlayerEntity = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().GamePlayerEntity;



        yield return null;
    }


    public override IEnumerator TaskStart()
    {
        Create_CubeWall(10, 10);

        yield return null;
    }

    public override IEnumerator TaskStop()
    {
        yield return null;
    }



    //造牆且加入List
     void Create_CubeWall(float height, float width)
    {
        int cube_Num = 0;
        float x = 0, y = -1, z = 0;
        Vector3 pos = new Vector3(x, y, z);

        for (z = -height / 2; z < height / 2; z++)
        {
            for (x = -width / 2; x < width / 2; x++)
            {
                pos = new Vector3(x, y, z);
                var cube = GameObject.Instantiate(ClickableCube, pos, Quaternion.identity, spawn_parent);
                cube.name = "cube" + cube_Num;
                cube_Num++;
                List_wallCubes.Add(cube);
            }
        }
    }

    
}
