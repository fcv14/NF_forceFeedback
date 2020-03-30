using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEntity : GameEntityBase
{
    bool triggerOn = false;

    public static bool Bool_ALL_TEandCC = false;

    private void OnTriggerExit(Collider other)
    {
        triggerOn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerOn = true;

        bool allGood = true;
        for (int i = 0; i < MissionTask.List_triggers.Count; i++)
        {
            if (MissionTask.List_triggers[i].GetComponent<TriggerEntity>().triggerOn != true)
            {
                allGood = false;
                break;
            }
        }

        bool colorCorrect = true;
        if (allGood)
        {
            for (int i = 0; i < MissionTask.List_triggers.Count; i++)
            {
                if (this.GetComponent<MeshRenderer>().material.color.r != other.GetComponent<MeshRenderer>().material.color.r)
                {
                    colorCorrect = false;
                    break;
                }
            }

            if (colorCorrect)
            {
                Debug.Log("all CC");
                Bool_ALL_TEandCC = true;

                //next Level or leave
                //跳出過關提示
            }
        }
    }

    public override void EntityDispose()
    {

    }
}
