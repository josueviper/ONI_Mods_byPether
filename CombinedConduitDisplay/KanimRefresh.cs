﻿using HarmonyLib;
using UnityEngine;

namespace CombinedConduitDisplay
{
    class KanimRefresh
    {
        public static void RefreshKbacForLayerTarget(GameObject go)
        {
            KBatchedAnimController kbac = go.GetComponent<KBatchedAnimController>();
            if ((UnityEngine.Object)kbac == (UnityEngine.Object)null)
                return;

            kbac.enabled = false;
            kbac.enabled = true;

            TryMeterRefresh_SolidConduitOutbox(go);
            TryMeterRefresh_SolidConduitInbox(go);
        }

        public static void TryMeterRefresh_LogicCounter(GameObject go)
        {
            LogicCounter counter = go.GetComponent<LogicCounter>();
            if (counter == null)
                return;

            MeterController meter = Traverse.Create(counter).Field("meter").GetValue<MeterController>();
            if (meter == null)
                return;

            KBatchedAnimController kbac = meter.gameObject.GetComponent<KBatchedAnimController>();
            if (kbac == null)
                return;

            kbac.enabled = false;
            kbac.enabled = true;

        }
        public static void TryMeterRefresh_SolidConduitOutbox(GameObject go)
        {
            SolidConduitOutbox outbox = go.GetComponent<SolidConduitOutbox>();
            if (outbox == null)
                return;

            MeterController meter = Traverse.Create(outbox).Field("meter").GetValue<MeterController>();
            if (meter == null)
                return;

            KBatchedAnimController meterKbac = meter.gameObject.GetComponent<KBatchedAnimController>();
            if (meterKbac != null)
            {
                meterKbac.enabled = false;
                meterKbac.enabled = true;
            }
        }

        public static void TryMeterRefresh_SolidConduitInbox(GameObject go)
        {
            SolidConduitInbox inbox = go.GetComponent<SolidConduitInbox>();
            if (inbox == null)
                return;

            FilteredStorage filteredStorage = Traverse.Create(inbox).Field("filteredStorage").GetValue<FilteredStorage>();
            if (filteredStorage == null)
                return;

            MeterController meter = Traverse.Create(filteredStorage).Field("meter").GetValue<MeterController>();
            if (meter == null)
                return;

            KBatchedAnimController meterKbac = meter.gameObject.GetComponent<KBatchedAnimController>();
            if (meterKbac != null)
            {
                meterKbac.enabled = false;
                meterKbac.enabled = true;
            }
        }
    }
}
