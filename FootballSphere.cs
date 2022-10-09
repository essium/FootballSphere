using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using FootballSphere.Geometry;
using BepInEx.Logging;

namespace FootballSphere
{
    [BepInPlugin(package, plugin, version)]
    public class FootballSphere : BaseUnityPlugin
    {
        private const string package = "essium.DSP.FootballSphere";
        private const string plugin = "FootballSphere";
        private const string version = "1.0.2";
        private Harmony harmony;
        private static FootballData football;
        private static ManualLogSource logger;

        public static int nodeProtoId;
        public static int frameProtoId;
        public static int shellProtoId;

        private static ConfigEntry<bool> cheatMode;
        private static ConfigEntry<float> angle;

        private void BindConfig()
        {
            cheatMode = Config.Bind("Config", "CheatMode", false, "cheat mode");
            angle = Config.Bind("Config", "Angle", 4f, "angle between two nodes");
        }

        public void Start()
        {
            BindConfig();
            harmony = new Harmony(package + ":" + plugin + ":" + version);
            football = new FootballData(angle.Value, cheatMode.Value);
            try
            {
                harmony.PatchAll(typeof(FootballSphere));

            } catch (Exception e)
            {
                LogInfo(e.Message);
            }
        }

        public void OnDestroy()
        {
            BepInEx.Logging.Logger.Sources.Remove(logger);
            harmony.UnpatchAll();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(UIDysonEditor), "_OnUpdate")]
        public static void Update(UIDysonEditor __instance)
        {
            nodeProtoId = __instance.nodeProtoId;
            frameProtoId = __instance.frameProtoId;
            shellProtoId = __instance.shellProtoId;
            DysonSphereLayer singleSelectedLayer = __instance.selection.singleSelectedLayer;
            if (Input.GetKeyDown(KeyCode.N))
            {
                football.Draw(singleSelectedLayer, DrawType.NODE);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                football.Draw(singleSelectedLayer, DrawType.FRAME);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                football.Draw(singleSelectedLayer, DrawType.SHELL);
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

        static FootballSphere()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(plugin);
        }

        public static void LogInfo(string msg)
        {
            logger.LogInfo(msg);
        }
    }
}
