using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace NoFailPlus {
	class HarmonyPatch {
		
		[HarmonyPrefix]
		public static bool PreLevelFailed() {
			
			return false;
			
		}
		
	}
}