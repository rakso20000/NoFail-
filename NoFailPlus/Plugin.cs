using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using MethodInfo = System.Reflection.MethodInfo;
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
			
		}
		
		public void OnSceneUnloaded(Scene scene) {
			
		}
		
	}
}