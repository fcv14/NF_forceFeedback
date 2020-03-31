using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LabData;
using DataSync;

public class BuildUI : MonoBehaviour
{
    [Header("GameUI")]
    public Slider Slider_ColorAlpha;
    public Button Btn_MakeHole;
    public Button Btn_SaveTask;
    public Button Btn_Return2MainScene;

    [Space(10)]
    public GameObject Obj_AfterSaveButtonClicked;
    public InputField IPF_TaskName;
    public Button Btn_ConfirmSave;

    [Space(10)]
    public GameObject Obj_taskNameNull;
    public Text Txt_taskNameNull;
    public Button Btn_Okay_taskNameNull;


    [Space(10)]
    public GameObject Obj_TaskBuildSuccessfully;
    public Text Txt_TaskBuildScuccessfully;
    public Button Btn_Okay_TaskBuildSuccessfully;



    
    private float Cube_alpha;
    private int color_Num = 0;
    private int parent_Num = 0;
    private Color32[] color_Array; //color陣列
    private float parent_z_pos = 0;  //物件初始位置

    private List<GameObject> tempSavedParent = new List<GameObject>();            //make_btn後將parent暫時存入之List
    private List<GameObject> tempSavedTrgParent = new List<GameObject>();         //將trgParent暫時存入之List


    Data_TaskS data_tasks = GameDataManager.FlowData.DataTasks;
    TaskData taskData = new TaskData();
    string taskName = "task_000";
    

    void Start()
    {
        Slider_ColorAlpha.onValueChanged.AddListener(delegate { Slider_alphaChange(); });
        Btn_MakeHole.onClick.AddListener(delegate { Make_Hole(); });
        Btn_SaveTask.onClick.AddListener(delegate { SaveBtn_clicked(); });
        Btn_ConfirmSave.onClick.AddListener(delegate { ConfirmBtn_clicked(); });
        Btn_Okay_taskNameNull.onClick.AddListener(delegate { OkayBtn_TaskNameNull(); });
        Btn_Okay_TaskBuildSuccessfully.onClick.AddListener(delegate { OkayBtn_TaskBuildSuc(); });
        Btn_Return2MainScene.onClick.AddListener(delegate { Return2MainUI(); });

        ColorInit();
    }

    void Make_Hole()
    {
        if (BuildTask.List_selectedCubes.Count != 0)
        {
            //換色
            ClickableCubeEntity.Color_clicked = color_Array[color_Num %= 4];
            color_Num++;

            //生成一新空物件parent
            GameObject cube_parent = new GameObject("Cube_parent " + parent_Num);
            cube_parent.transform.SetParent(this.gameObject.transform);

            //修正parent的初始pos
            foreach (var selectedCube in BuildTask.List_selectedCubes)
            {
                parent_z_pos += selectedCube.transform.position.z;
            }
            parent_z_pos /= BuildTask.List_selectedCubes.Count;
            cube_parent.gameObject.transform.position = new Vector3(0, 0, parent_z_pos);

            //將每一個選到的cube複製且留下其trigger
            int trg_num = 0;
            GameObject trg_parent = new GameObject("Trg_parent_" + parent_Num);
            tempSavedTrgParent.Add(trg_parent);
            foreach (var selectedCube in BuildTask.List_selectedCubes)
            {
                GameObject trg = new GameObject("trg_" + trg_num);
                trg_num++;
                trg.transform.position = selectedCube.transform.position;
                trg.transform.SetParent(trg_parent.transform);
                BuildTask.List_triggers.Add(trg);

                var trg_renderer = selectedCube.GetComponent<Renderer>().material;
                Color color = new Color(trg_renderer.color.r, trg_renderer.color.g, trg_renderer.color.b, Cube_alpha);
                trg.AddComponent<MeshFilter>();
                trg.GetComponent<MeshFilter>().mesh = selectedCube.GetComponent<MeshFilter>().mesh;
                trg.AddComponent<MeshRenderer>();
                trg.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
                trg.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");//doing some alpha chaging work

                trg.AddComponent<BoxCollider>();
                trg.GetComponent<BoxCollider>().isTrigger = true;

                trg.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);    //縮小
            }

            //修正完pos後,將select到的set_parent
            foreach (var selectedCube in BuildTask.List_selectedCubes)
            {
                selectedCube.transform.SetParent(cube_parent.transform);
                selectedCube.GetComponent<ClickableCubeEntity>().isOnClick = false;
                //selectedCube.layer = 0;
            }
            cube_parent.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);    //稍微縮小

            //再對parent做操作
            cube_parent.gameObject.transform.position = new Vector3(Random.Range(-5, 8), 4, -8.5f);
            Rigidbody rb = cube_parent.AddComponent<Rigidbody>();                //增加rigibody

            tempSavedParent.Add(cube_parent);
            parent_Num++;
            //DataTempSave();
        }
        BuildTask.List_selectedCubes.Clear();
        //triggers_List.Clear();
        parent_z_pos = 0;
        //還原
    }
    
    void Slider_alphaChange()
    {
        Cube_alpha = Slider_ColorAlpha.value;
        foreach (var trg_P in BuildTask.List_triggers)
        {
            Color C_A = new Color();
            C_A.a = Cube_alpha;
            C_A.r = trg_P.GetComponentInChildren<Renderer>().material.color.r;
            C_A.g = trg_P.GetComponentInChildren<Renderer>().material.color.g;
            C_A.b = trg_P.GetComponentInChildren<Renderer>().material.color.b;
            trg_P.transform.GetComponent<Renderer>().material.color = C_A;
        }
    }

    void SaveBtn_clicked()
    {
        Obj_AfterSaveButtonClicked.SetActive(true);
    }

    void ConfirmBtn_clicked()
    {
        if(IPF_TaskName.text == "")
        {
            Txt_taskNameNull.text = "請輸入Task名稱";
            Obj_taskNameNull.SetActive(true);
        }
        else
        {
            try
            {
                TaskSave();
                Txt_TaskBuildScuccessfully.text = "Task建立完成!";
                Obj_TaskBuildSuccessfully.SetActive(true);
                //saveWork
            }
            catch 
            {
                Txt_TaskBuildScuccessfully.text = "關卡名稱錯誤!";
                Obj_TaskBuildSuccessfully.SetActive(true);
            }
            
        }
    }

    void ColorInit()
    {
        color_Array = new Color32[4];
        ClickableCubeEntity.Color_clicked = new Color32(0, 44, 188, 255);  //blue
        color_Array[0] = new Color32(184, 39, 39, 255);  //red
        color_Array[1] = new Color32(202, 204, 47, 255);  //smooth yellow
        color_Array[2] = new Color32(121, 201, 61, 255);  //green
        color_Array[3] = new Color32(0, 44, 188, 255);  //blue
    }

    void CubesData()
    {
        //free cube
        taskData.List_cubeParentData= new List<CubeParentData>();
        for (int i = 0; i < tempSavedParent.Count; i++)
        {
            CubeParentData parentData = new CubeParentData();
            parentData.List_cubeData = new List<CubeData>();
            parentData.parentName = "parent_" + i;

            parentData.parentPos = new float[3];
            parentData.parentPos[0] = tempSavedParent[i].transform.position.x;
            parentData.parentPos[1] = tempSavedParent[i].transform.position.y;
            parentData.parentPos[2] = tempSavedParent[i].transform.position.z;

            parentData.parentRot = new float[4];
            parentData.parentRot[0] = tempSavedParent[i].transform.rotation.x;
            parentData.parentRot[1] = tempSavedParent[i].transform.rotation.y;
            parentData.parentRot[2] = tempSavedParent[i].transform.rotation.z;
            parentData.parentRot[3] = tempSavedParent[i].transform.rotation.w;

            parentData.parentScl = new float[3];
            parentData.parentScl[0] = tempSavedParent[i].transform.localScale.x;
            parentData.parentScl[1] = tempSavedParent[i].transform.localScale.y;
            parentData.parentScl[2] = tempSavedParent[i].transform.localScale.z;

            for (int j = 0; j < tempSavedParent[i].transform.childCount; j++)
            {
                CubeData cubeData = new CubeData();

                cubeData.cubeName = tempSavedParent[i].transform.GetChild(j).name;

                //cubeData.cubesParentName = tempSavedParent[i].name;

                cubeData.cubePos = new float[3];
                cubeData.cubePos[0] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().position.x;
                cubeData.cubePos[1] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().position.y;
                cubeData.cubePos[2] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().position.z;

                cubeData.cubeRot = new float[4];
                cubeData.cubeRot[0] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localRotation.x;
                cubeData.cubeRot[1] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localRotation.y;
                cubeData.cubeRot[2] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localRotation.z;
                cubeData.cubeRot[3] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localRotation.w;

                cubeData.cubeScl = new float[3];
                cubeData.cubeScl[0] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localScale.x;
                cubeData.cubeScl[1] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localScale.y;
                cubeData.cubeScl[2] = tempSavedParent[i].transform.GetChild(j).GetComponent<Transform>().localScale.z;

                cubeData.cubeColorRGBA = new float[4];
                cubeData.cubeColorRGBA[0] = tempSavedParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.r;
                cubeData.cubeColorRGBA[1] = tempSavedParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.g;
                cubeData.cubeColorRGBA[2] = tempSavedParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.b;
                cubeData.cubeColorRGBA[3] = tempSavedParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.a;

                parentData.List_cubeData.Add(cubeData);
            }
            taskData.List_cubeParentData.Add(parentData);
        }

        //non-free cube
        taskData.List_originCubeData = new List<OriginCubeData>();
        for (int i = 0; i < BuildTask.List_wallCubes.Count; i++)
        {
            OriginCubeData originCubeData = new OriginCubeData();

            originCubeData.originCubeName = BuildTask.List_wallCubes[i].name;

            originCubeData.originCubePos = new float[3];
            originCubeData.originCubePos[0] = BuildTask.List_wallCubes[i].transform.position.x;
            originCubeData.originCubePos[1] = BuildTask.List_wallCubes[i].transform.position.y;
            originCubeData.originCubePos[2] = BuildTask.List_wallCubes[i].transform.position.z;

            originCubeData.originCubeColorRGBA = new float[4];
            originCubeData.originCubeColorRGBA[0] = BuildTask.List_wallCubes[i].transform.GetComponent<Renderer>().material.color.r;
            originCubeData.originCubeColorRGBA[1] = BuildTask.List_wallCubes[i].transform.GetComponent<Renderer>().material.color.g;
            originCubeData.originCubeColorRGBA[2] = BuildTask.List_wallCubes[i].transform.GetComponent<Renderer>().material.color.b;
            originCubeData.originCubeColorRGBA[3] = BuildTask.List_wallCubes[i].transform.GetComponent<Renderer>().material.color.a;

            taskData.List_originCubeData.Add(originCubeData);
        }

        //triggers
        taskData.List_triggerParentData = new List<TriggerParentData>();
        for (int i = 0; i < tempSavedParent.Count; i++)
        {
            TriggerParentData triggerParentData = new TriggerParentData();
            triggerParentData.triggerParentName = tempSavedTrgParent[i].name;

            triggerParentData.triggerParentPos = new float[3];
            triggerParentData.triggerParentPos[0] = tempSavedTrgParent[i].transform.position.x;
            triggerParentData.triggerParentPos[1] = tempSavedTrgParent[i].transform.position.y;
            triggerParentData.triggerParentPos[2] = tempSavedTrgParent[i].transform.position.z;

            triggerParentData.List_triggerData = new List<TriggerData>();
            for (int j = 0; j < tempSavedTrgParent[i].transform.childCount; j++)
            {
                TriggerData triggerData = new TriggerData();
                triggerData.TriggerName = tempSavedTrgParent[i].transform.GetChild(j).name;

                triggerData.TriggerPos = new float[3];
                triggerData.TriggerPos[0] = tempSavedTrgParent[i].transform.GetChild(j).transform.position.x;
                triggerData.TriggerPos[1] = tempSavedTrgParent[i].transform.GetChild(j).transform.position.y;
                triggerData.TriggerPos[2] = tempSavedTrgParent[i].transform.GetChild(j).transform.position.z;

                triggerData.TriggerScl = new float[3];
                triggerData.TriggerScl[0] = tempSavedTrgParent[i].transform.GetChild(j).transform.localScale.x;
                triggerData.TriggerScl[1] = tempSavedTrgParent[i].transform.GetChild(j).transform.localScale.y;
                triggerData.TriggerScl[2] = tempSavedTrgParent[i].transform.GetChild(j).transform.localScale.z;

                triggerData.TriggerColorRGBA = new float[4];
                triggerData.TriggerColorRGBA[0] = tempSavedTrgParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.r;
                triggerData.TriggerColorRGBA[1] = tempSavedTrgParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.g;
                triggerData.TriggerColorRGBA[2] = tempSavedTrgParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.b;
                triggerData.TriggerColorRGBA[3] = tempSavedTrgParent[i].transform.GetChild(j).GetComponent<Renderer>().material.color.a;

                triggerParentData.List_triggerData.Add(triggerData);
            }
            taskData.List_triggerParentData.Add(triggerParentData);
        }
    }

    void TaskSave()
    {
        CubesData();
        taskName = IPF_TaskName.text;
        data_tasks.data_TasksDic.Add(taskName, taskData);

        LabTools.CreateDataFolder<Data_TaskS>();
        LabTools.WriteData<Data_TaskS>(data_tasks, "AllTasks", true);
    }

    void OkayBtn_TaskBuildSuc()
    {
        IPF_TaskName.text = "";
        Obj_TaskBuildSuccessfully.SetActive(false);
    }
    
    void OkayBtn_TaskNameNull()
    {
        Obj_taskNameNull.SetActive(false);
    }

    void Return2MainUI()
    {
        GameSceneManager.Instance.Change2MainUI();
    }
}
