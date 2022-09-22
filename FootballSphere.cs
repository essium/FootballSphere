using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using FootballSphere.Geometry;

namespace FootballSphere
{
    [BepInPlugin(package, plugin, version)]
    public class FootballSphere: BaseUnityPlugin
    {
        private const string package = "essium.DSP.FootballSphere";
        private const string plugin = "FootballSphere";
        private const string version = "1.0";
        private Harmony harmony;
        private static FootballData football;

        public static int nodeProtoId;
        public static int frameProtoId;
        public static int shellProtoId;

        public static ConfigEntry<string> keyBuild;
        public static ConfigEntry<int> n;
        public static ConfigEntry<int> m;
        public static ConfigEntry<bool> drawShell;

        private void BindConfig()
        {
            keyBuild = Config.Bind("Config", "BuildShortKey", "q", "construct football sphere shortkey");
            n = Config.Bind("Config", "SegmentsInLongEdge", 6, "segment count in long edge [1 - 6]");
            m = Config.Bind("Config", "SegmentsInShortEdge", 4, "segment count in short edge [1 - 4]");
            drawShell = Config.Bind("Config", "DrawShell", true, "whether draw dyson shell");
        }

        public void Start()
        {
            BindConfig();
            harmony = new Harmony(package + ":" + plugin + ":" + version);
            football = new FootballData(n, m);
            try
            {
                harmony.PatchAll(typeof(FootballSphere));

            } catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

        public void OnDestroy()
        {
            harmony.UnpatchAll();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(UIDysonEditor), "_OnUpdate")]
        public static void Update(UIDysonEditor __instance)
        {
            nodeProtoId = __instance.nodeProtoId;
            frameProtoId = __instance.frameProtoId;
            shellProtoId = __instance.shellProtoId;
            if (Input.GetKeyDown(keyBuild.Value))
            {
                DysonSphereLayer singleSelectedLayer = __instance.selection.singleSelectedLayer;
                if (singleSelectedLayer == null)
                {
                    return;
                }
                football.Draw(singleSelectedLayer, drawShell.Value);
            }
        }

        private static void drawNode(LLtude a, DysonSphereLayer layer)
        {
            for (int i = 0; i < 5; i++)
            {
                LLtude x = new LLtude(a.phi, a.lambda + 0.4f * i * Mathf.PI);
                Vector3 p = x.Position(layer.orbitRadius);
                layer.NewDysonNode(nodeProtoId, p);
            }
        }
    }
}
