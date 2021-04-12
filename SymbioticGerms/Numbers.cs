﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SymbioticGerms
{
    class Numbers
    {
        private static float MaxGerms = 100000;

        public static byte IndexFoodPoisoning => Db.Get().Diseases.GetIndex((HashedString)"FoodPoisoning");
        public static byte IndexSlimeLung => Db.Get().Diseases.GetIndex((HashedString)"SlimeLung");
        public static byte IndexZombieSpores => Db.Get().Diseases.GetIndex((HashedString)"ZombieSpores");

        public static float GetGermKillTempDelta(GameObject go)
        {
            PrimaryElement ele = go.GetComponent<PrimaryElement>();
            if (ele == null)
                return 0;

            int cell = Grid.PosToCell(go);
            int gridGerms = Grid.DiseaseCount[cell];
            byte idx = gridGerms >= ele.DiseaseCount ? Grid.DiseaseIdx[cell] : ele.DiseaseIdx;

            float killTemp = 0;
            if (idx == IndexFoodPoisoning)
                killTemp = Db.Get().Diseases.FoodGerms.temperatureRange.maxViable;
            else if (idx == IndexSlimeLung)
                killTemp = Db.Get().Diseases.SlimeGerms.temperatureRange.maxViable;
            else if (idx == IndexZombieSpores)
                killTemp = Db.Get().Diseases.ZombieSpores.temperatureRange.maxViable;

            float delta = killTemp - ele.Temperature;

            return delta * PercentOfMaxGerms(GetGermCount(go, idx));
        }

        public static int GetGermCount(GameObject go, byte germType)
        {
            PrimaryElement ele = go.GetComponent<PrimaryElement>();
            int cell = Grid.PosToCell(go);
            int count = (Grid.DiseaseIdx[cell] == germType ? Grid.DiseaseCount[cell] : 0);

            if (ele == null)
                return count;

            int higherGerms = Mathf.Max(count, (ele.DiseaseIdx == germType ? ele.DiseaseCount : 0));

            return higherGerms;
        }

        public static float PercentOfMaxGerms(int currentGerms)
        {
            if (currentGerms >= MaxGerms)
                return 1;
            return currentGerms / MaxGerms;
        }
    }
}
