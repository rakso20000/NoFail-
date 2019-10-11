using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using MethodInfo = System.Reflection.MethodInfo;
using BS_Utils.Gameplay;
using CustomUI.GameplaySettings;
using Harmony;

namespace NoFailPlus {
	public class Plugin : IBeatSaberPlugin {
		
		internal static Ref<PluginConfig> config;
		internal static IConfigProvider configProvider;
		
		public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider) {
			
			Logger.log = logger;
			configProvider = cfgProvider;
			
			config = cfgProvider.MakeLink<PluginConfig>((p, v) => {
				
				if (v.Value == null || v.Value.RegenerateConfig)
					p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
				
				config = v;
				
			});
			
			InitHarmony();
			
		}
		
		public void InitHarmony() {
			
			HarmonyInstance harmony = HarmonyInstance.Create("com.github.rakso20000.nofailplus");
			
			MethodInfo patched = typeof(StandardLevelFailedController).GetMethod("StartLevelFailed");
			MethodInfo patch = typeof(HarmonyPatch).GetMethod("PreLevelFailed");
			
			harmony.Patch(patched, new HarmonyMethod(patch));

		}
		
		public void OnApplicationStart() {
			
			Logger.log.Debug("OnApplicationStart");
			
		}
		
		public void OnApplicationQuit() {
			
			Logger.log.Debug("OnApplicationQuit");
			
		}
		
		public void OnFixedUpdate() {
			
		}
		
		public void OnUpdate() {
			
		}
		
		public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) {
			
		}
		
		public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
			
			if (scene.name == "MenuCore") {
				
				Gamemode.Init();
				
				ToggleOption enableToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersRight, "No Fail+", "Activates No Fail in party mode with no effect on score", null, 0);
				enableToggle.GetValue = config.Value.Enable;
				enableToggle.OnToggle += (bool enabled) => {
					
					config.Value.Enable = enabled;
					
				};
				
			}
			
		}
		
		public void OnSceneUnloaded(Scene scene) {
			
		}
		
	}
}