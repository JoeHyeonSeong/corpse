using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageList
{
    [System.Serializable]
    class BoolWrapper
    {
        public bool[] array;
        public BoolWrapper(bool[] array)
        {
            this.array = array;
        }
    }
    private static bool[] lockInfo;

    //각각의 월드에 스테이지가 몇개인지
    private static int[] StageNo = new int[] { 18, 0, 0 };
    private static string[,] stageList = new string[,] {
        {"jslevel0","jslevel1","jslevel2","jslevel3","jslevel4","jslevel9","b1","Jslevel6",//world0
        "Js1","jslevel7","jslevel5","a3","jslevel11","jslevel14","jslevel13","Jslevel15","Jslevel8b","Js2a"//world1
        }//world2
    };

    public static string GetStageName(int world, int stage)
    {
        if (StageNo[world] < stage)
        {
            Debug.Log("invalid stage");
            return null;
        }
        return stageList[world, stage];
    }

    public static int GetWorldSize(int world)
    {
        return StageNo[world];
    }

    public static void UnLock(int worldIndex, int stageIndex)
    {
        if (worldIndex > StageNo.Length - 1) return;
        if (stageIndex > StageNo[worldIndex] - 1) return;
        lockInfo[CalLockIndex(worldIndex, stageIndex)] = true;
        PlayerPrefs.SetString(PrefsKey.lockInfo, JsonUtility.ToJson(new BoolWrapper(lockInfo)));
    }

    private static void SetLockInfo()
    {
        string jsonData = PlayerPrefs.GetString(PrefsKey.lockInfo);
        if (jsonData == "")
        {
            int totalStageNum = 0;
            foreach (int num in StageNo) totalStageNum += num;
            lockInfo = new bool[totalStageNum];
            lockInfo[0] = true;
            //for debug
            for (int i = 0; i < lockInfo.Length; i++) lockInfo[i] = true;
            //
            string newJsonData = JsonUtility.ToJson(new BoolWrapper(lockInfo));
            PlayerPrefs.SetString(PrefsKey.lockInfo, newJsonData);
        }
        else
        {
            lockInfo = ((BoolWrapper)JsonUtility.FromJson(jsonData, typeof(BoolWrapper))).array;
        }
    }

    public static bool IsUnLocked(int worldIndex, int stageIndex)
    {
        if (worldIndex > StageNo.Length - 1) return false;
        if (stageIndex > StageNo[worldIndex] - 1) return false;
        if (lockInfo == null)
        {
            SetLockInfo();
        }
        return lockInfo[CalLockIndex(worldIndex, stageIndex)];
    }

    private static int CalLockIndex(int worldIndex, int stageIndex)
    {
        int accStageIndex = stageIndex;
        for (int i = 0; i < worldIndex; i++) accStageIndex += StageNo[i];
        return accStageIndex;
    }
}
