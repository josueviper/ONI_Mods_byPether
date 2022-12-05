﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SlimyPollutedOxygen : RandomDiseaseEvent
    {
        public SlimyPollutedOxygen(int weight = 1)
        {
            ID = nameof(SlimyPollutedOxygen);
            GeneralName = "Slimy Polluted Oxygen";
            AppearanceWeight = weight;
            DangerLevel = Helpers.EstimateGermDanger(GermIdx.SlimelungIdx);

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    int germsToAdd = 100000;
                    foreach (int cell in ONITwitchLib.GridUtil.ActiveSimCells())
                        if (Grid.Element[cell].id == SimHashes.ContaminatedOxygen)
                            SimMessages.ModifyDiseaseOnCell(cell, GermIdx.SlimelungIdx, germsToAdd);
                });
        }
    }
}
