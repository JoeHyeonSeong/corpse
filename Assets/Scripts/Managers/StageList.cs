﻿using System.Collections;
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
    private static int[] StageNo = new int[] { 9, 9, 9 };
    private static string[,] stageList = new string[,] {
        {"jslevel1","jslevel2","jslevel3","jslevel4","jslevel5","jslevel6","jslevel7","jslevel8","jslevel9"},//world0
        {"js1","Level","Level","Level","Level","Level","Level","Level","Level" },//world1
        {"Level","Level","Level","Level","Level","Level","Level","Level","Level" }//world2
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