using System.Reflection;
using AILimit;
using SPT.Reflection.Patching;
using BepInEx;
using BepInEx.Configuration;
using dvize.AILimit;
using EFT;

namespace AIlimit
{
    [BepInPlugin("com.dvize.AILimit", "dvize.AILimit", "1.8.6")]
    public class AILimitPlugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> PluginEnabled;
        public static ConfigEntry<int> BotLimit;
        //public static ConfigEntry<float> MaxBotsMultiplier;
        public static ConfigEntry<float> BotDistance;
        public static ConfigEntry<float> TimeAfterSpawn;
        public static ConfigEntry<int> FramesToCheck;

        public static ConfigEntry<float> factoryDistance;
        public static ConfigEntry<float> interchangeDistance;
        public static ConfigEntry<float> laboratoryDistance;
        public static ConfigEntry<float> lighthouseDistance;
        public static ConfigEntry<float> reserveDistance;
        public static ConfigEntry<float> shorelineDistance;
        public static ConfigEntry<float> woodsDistance;
        public static ConfigEntry<float> customsDistance;
        public static ConfigEntry<float> tarkovstreetsDistance;
        public static ConfigEntry<float> groundZeroDistance;
        private void Awake()
        {
            PluginEnabled = Config.Bind(
                "Main Settings",
                "1. Plugin on/off",
                true,
                "");

            //MaxBotsMultiplier = Config.Bind(
            //    "Main Settings",
            //    "2. Map Max Bots Multiplier",
            //    1f,
            //    new ConfigDescription("", new AcceptableValueRange<float>(.2f, 3f)));

            BotLimit = Config.Bind(
                "Main Settings",
                "3. Bot Limit (At Distance)",
                10,
                new ConfigDescription("Based on your distance selected, limits up to this many # of bots moving at one time", new AcceptableValueRange<int>(4, 20))
                );

            TimeAfterSpawn = Config.Bind(
                "Main Settings",
                "4. Time After Spawn",
                10f,
                "Time (sec) to wait before disabling");

            FramesToCheck = Config.Bind(
                "Main Settings",
                "5. Delay frames before recheck bots",
                300,
                "Time (frames) to wait before rechecking bots");

            factoryDistance = Config.Bind(
                "Map Related",
                "1. Factory",
                80.0f,
                "Distance after which bots are disabled.");

            laboratoryDistance = Config.Bind(
                "Map Related",
                "2. Laboratory",
                250.0f,
                "Distance after which bots are disabled.");

            groundZeroDistance = Config.Bind(
                "Map Related",
                "3. Ground Zero",
                400.0f,
                "Distance after which bots are disabled.");

            customsDistance = Config.Bind(
                "Map Related",
                "4. Customs",
                400.0f,
                "Distance after which bots are disabled.");

            reserveDistance = Config.Bind(
                "Map Related",
                "5. Reserve base",
                400.0f,
                "Distance after which bots are disabled.");

            interchangeDistance = Config.Bind(
                "Map Related",
                "6. Interchange",
                400.0f,
                "Distance after which bots are disabled.");

            tarkovstreetsDistance = Config.Bind(
                "Map Related",
                "7. Streets",
                400.0f,
                "Distance after which bots are disabled.");

            woodsDistance = Config.Bind(
                "Map Related",
                "8. Woods",
                400.0f,
                "Distance after which bots are disabled.");

            shorelineDistance = Config.Bind(
                "Map Related",
                "9. Shoreline",
                400.0f,
                "Distance after which bots are disabled.");

            lighthouseDistance = Config.Bind(
                "Map Related",
                "A. Lighthouse",
                400.0f,
                "Distance after which bots are disabled.");

            ConfigManager.Initialize();
            new NewGamePatch().Enable();
        }
    }

    //re-initializes each new game
    internal class NewGamePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));

        [PatchPrefix]
        public static void PatchPrefix()
        {
            AILimitComponent.Enable();
        }
    }
}
