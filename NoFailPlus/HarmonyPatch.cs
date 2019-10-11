using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using BS_Utils.Gameplay;

namespace NoFailPlus {
	class HarmonyPatch {
		
		[HarmonyPrefix]
		public static bool PreLevelFailed() {
			
			if (!Gamemode.IsPartyActive)
				ScoreSubmission.DisableSubmission("NoFailPlus");
			
			return !Plugin.config.Value.Enable;
			
		}
		
	}
}