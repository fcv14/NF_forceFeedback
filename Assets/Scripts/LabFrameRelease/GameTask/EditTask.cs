using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;

public class EditTask : TaskBase
{
    private GameObject ClickableCube ;
    private GameObject GamePlayerEntity ;
    private Transform reSpawn_parent;
    
    public static List<ClickableCubeEntity> List_wallCubes = new List<ClickableCubeEntity>();          //牆之方塊List
    public static List<GameObject> List_triggers = new List<GameObject>();           //全部之trigger
    public static List<ClickableCubeEntity> List_selectedCubes = new List<ClickableCubeEntity>();      //被選取方塊


    private List<ClickableCubeEntity> List_CliclableCubes = new List<ClickableCubeEntity>();  //全部方塊


    private string taskname;
    public override IEnumerator TaskInit()
    {
        ClickableCube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().clickable_prefab.gameObject;
        GamePlayerEntity = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().GamePlayerEntity.gameObject;
        reSpawn_parent = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().spanwPos;
        

        taskname = GameDataManager.FlowData.TaskName;
        LoadTaskData(taskname);
        yield return null;
    }

    public override IEnumerator TaskStart()
    {
        yield return null;
    }

    public override IEnumerator TaskStop()
    {
        yield return null;
    }

    void LoadTaskData(string taskName)
    {
        List<GameObject> List_TempSavedParent = new List<GameObject>();
        List<GameObject> List_WallCubes = new List<GameObject>();
        List<GameObject> List_TempSavedTrgParent = new List<GameObject>();

        Data_TaskS data_TaskS = LabTools.GetData<Data_TaskS>("AllTasks");
        TaskData taskdata = data_TaskS.data_TasksDic[taskName];
        //free cubes
        for (int i = 0; i < taskdata.List_cubeParentData.Count; i++)
        {
            GameObject cubeParentRebiuld = new GameObject();
            cubeParentRebiuld.name = taskdata.List_cubeParentData[i].parentName;

            Vector3 temp_ParentPos = new Vector3();
            temp_ParentPos.x = taskdata.List_cubeParentData[i].parentPos[0];
            temp_ParentPos.y = taskdata.List_cubeParentData[i].parentPos[1];
            temp_ParentPos.z = taskdata.List_cubeParentData[i].parentPos[2];

            Quaternion temp_ParentRot = new Quaternion();
            temp_ParentRot.x = taskdata.List_cubeParentData[i].parentRot[0];
            temp_ParentRot.y = taskdata.List_cubeParentData[i].parentRot[1];
            temp_ParentRot.z = taskdata.List_cubeParentData[i].parentRot[2];
            temp_ParentRot.w = taskdata.List_cubeParentData[i].parentRot[3];
            cubeParentRebiuld.transform.rotation = temp_ParentRot;

            Vector3 temp_ParentScl = new Vector3();
            temp_ParentScl.x = taskdata.List_cubeParentData[i].parentScl[0];
            temp_ParentScl.y = taskdata.List_cubeParentData[i].parentScl[1];
            temp_ParentScl.z = taskdata.List_cubeParentData[i].parentScl[2];
            cubeParentRebiuld.transform.localScale = temp_ParentScl;

            cubeParentRebiuld.transform.position = temp_ParentPos;

            cubeParentRebiuld.AddComponent<Rigidbody>();
            cubeParentRebiuld.tag = "Touchable";

            for (int j = 0; j < taskdata.List_cubeParentData[i].List_cubeData.Count; j++)
            {
                GameObject cubeRebiuld = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cubeRebiuld.transform.SetParent(cubeParentRebiuld.transform);

                cubeRebiuld.name = taskdata.List_cubeParentData[i].List_cubeData[j].cubeName;

                Vector3 temp_CubePos = new Vector3();
                temp_CubePos.x = taskdata.List_cubeParentData[i].List_cubeData[j].cubePos[0];
                temp_CubePos.y = taskdata.List_cubeParentData[i].List_cubeData[j].cubePos[1];
                temp_CubePos.z = taskdata.List_cubeParentData[i].List_cubeData[j].cubePos[2];
                cubeRebiuld.transform.position = temp_CubePos;

                Vector3 temp_CubeScl = new Vector3();
                temp_CubeScl.x = taskdata.List_cubeParentData[i].List_cubeData[j].cubeScl[0];
                temp_CubeScl.y = taskdata.List_cubeParentData[i].List_cubeData[j].cubeScl[1];
                temp_CubeScl.z = taskdata.List_cubeParentData[i].List_cubeData[j].cubeScl[2];
                cubeRebiuld.transform.localScale = temp_CubeScl;

                Quaternion temp_CubeRot = new Quaternion();
                temp_CubeRot.x = taskdata.List_cubeParentData[i].List_cubeData[j].cubeRot[0];
                temp_CubeRot.y = taskdata.List_cubeParentData[i].List_cubeData[j].cubeRot[1];
                temp_CubeRot.z = taskdata.List_cubeParentData[i].List_cubeData[j].cubeRot[2];
                temp_CubeRot.w = taskdata.List_cubeParentData[i].List_cubeData[j].cubeRot[3];
                cubeRebiuld.transform.localRotation = temp_CubeRot;

                Color temp_CubeColor = new Color();
                temp_CubeColor.r = taskdata.List_cubeParentData[i].List_cubeData[j].cubeColorRGBA[0];
                temp_CubeColor.g = taskdata.List_cubeParentData[i].List_cubeData[j].cubeColorRGBA[1];
                temp_CubeColor.b = taskdata.List_cubeParentData[i].List_cubeData[j].cubeColorRGBA[2];
                temp_CubeColor.a = taskdata.List_cubeParentData[i].List_cubeData[j].cubeColorRGBA[3];
                cubeRebiuld.GetComponent<Renderer>().material.color = temp_CubeColor;

                cubeRebiuld.tag = "Touchable";
            }
            List_TempSavedParent.Add(cubeParentRebiuld);
        }
        //wall cubes
        for (int i = 0; i < taskdata.List_originCubeData.Count; i++)
        {
            GameObject cubeRebiuld = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //可加入 addcomponent<cube的script讓他可以再次被滑鼠選取>
            cubeRebiuld.transform.SetParent(reSpawn_parent);

            cubeRebiuld.name = taskdata.List_originCubeData[i].originCubeName;

            Vector3 temp_CubePos = new Vector3();
            temp_CubePos.x = taskdata.List_originCubeData[i].originCubePos[0];
            temp_CubePos.y = taskdata.List_originCubeData[i].originCubePos[1];
            temp_CubePos.z = taskdata.List_originCubeData[i].originCubePos[2];
            cubeRebiuld.transform.position = temp_CubePos;

            Color temp_CubeColor = new Color();
            temp_CubeColor.r = taskdata.List_originCubeData[i].originCubeColorRGBA[0];
            temp_CubeColor.g = taskdata.List_originCubeData[i].originCubeColorRGBA[1];
            temp_CubeColor.b = taskdata.List_originCubeData[i].originCubeColorRGBA[2];
            temp_CubeColor.a = taskdata.List_originCubeData[i].originCubeColorRGBA[3];
            cubeRebiuld.GetComponent<Renderer>().material.color = temp_CubeColor;

            cubeRebiuld.layer = 8;             //8為clickableLayer
            cubeRebiuld.tag = "Touchable";
            List_WallCubes.Add(cubeRebiuld);
        }
        //trgs
        for (int i = 0; i < taskdata.List_triggerParentData.Count; i++)
        {
            GameObject trgParent = new GameObject();
            trgParent.name = taskdata.List_triggerParentData[i].triggerParentName;

            Vector3 temp_trgParentPos = new Vector3();
            temp_trgParentPos.x = taskdata.List_triggerParentData[i].triggerParentPos[0];
            temp_trgParentPos.y = taskdata.List_triggerParentData[i].triggerParentPos[1];
            temp_trgParentPos.z = taskdata.List_triggerParentData[i].triggerParentPos[2];
            trgParent.transform.position = temp_trgParentPos;

            List_TempSavedTrgParent.Add(trgParent);
            //triggers
            for (int j = 0; j < taskdata.List_triggerParentData[i].List_triggerData.Count; j++)
            {
                GameObject trg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                trg.name = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerName;
                trg.transform.SetParent(trgParent.transform);
                trg.GetComponent<BoxCollider>().isTrigger = true;

                //將trigger縮小並下移(使遊戲能夠較完整的結束)
                trg.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
                Vector3 trgCenterPos = new Vector3();
                trgCenterPos = trg.GetComponent<BoxCollider>().center;
                trgCenterPos.y -= 0.4f;
                trg.GetComponent<BoxCollider>().center = trgCenterPos;

                //trg.AddComponent<Trigger>();//script

                Vector3 temp_trgPos = new Vector3();
                temp_trgPos.x = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerPos[0];
                temp_trgPos.y = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerPos[1];
                temp_trgPos.z = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerPos[2];
                trg.transform.position = temp_trgPos;

                Vector3 temp_trgScale = new Vector3();
                temp_trgScale.x = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerScl[0];
                temp_trgScale.y = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerScl[1];
                temp_trgScale.z = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerScl[2];
                trg.transform.localScale = temp_trgScale;

                Color temp_trgColor = new Color();
                temp_trgColor.r = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerColorRGBA[0];
                temp_trgColor.g = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerColorRGBA[1];
                temp_trgColor.b = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerColorRGBA[2];
                temp_trgColor.a = taskdata.List_triggerParentData[i].List_triggerData[j].TriggerColorRGBA[3];
                trg.GetComponent<MeshRenderer>().material.SetColor("_Color", temp_trgColor);
                trg.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");

                List_triggers.Add(trg);
            }
        }
    }


}
