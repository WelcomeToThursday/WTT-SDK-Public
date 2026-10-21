using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace AnimationEventSystem
{
	// Token: 0x02000E35 RID: 3637
	[Serializable]
	public class AnimationEvent
	{
		// Token: 0x04005802 RID: 22530
		public const float MAX_EVENT_TIME = 1f;

		// Token: 0x04005803 RID: 22531
		public AnimationEventParameter Parameter;

		// Token: 0x04005804 RID: 22532
		[SerializeField]
		private string _functionName;

		// Token: 0x04005805 RID: 22533
		[SerializeField]
		private int _functionNameHash;

		// Token: 0x04005806 RID: 22534
		public bool Enabled = true;

		// Token: 0x04005807 RID: 22535
		[SerializeField]
		private float _time;

		// Token: 0x04005808 RID: 22536
		public List<EventCondition> EventConditions;
#if UNITY_EDITOR
		public void UpdateHash()
        {
            // Change the Hash depending on the name
            // also change param type if the name supports it

            switch (_functionName)
			{
                case "AddAmmoInChamber":
                    _functionNameHash = -508605542;
                    break;
                case "AddAmmoInMag":
                    _functionNameHash = -1583670125;
                    break;
				case "Arm":
                    _functionNameHash = 386854108;
                    break;
				case "Cook":
                    _functionNameHash = 963907342;
                    break;
                case "DelAmmoChamber":
                    _functionNameHash = -945517627;
                    break;
                case "DelAmmoFromMag":
                    _functionNameHash = 1434365820;
                    break;
                case "Disarm":
                    _functionNameHash = 1239352288;
                    break;
                case "FireEnd":
                    _functionNameHash = -1677976749;
                    break;
                case "FiringBullet":
                    _functionNameHash = -58181185;
                    break;
                case "FoldOff":
                    _functionNameHash = -1051941490;
                    break;
                case "FoldOn":
                    _functionNameHash = 1704018560;
                    break;
                case "IdleStart":
                    _functionNameHash = 198060848;
                    break;
                case "LauncherAppeared":
                    _functionNameHash = 1390148366;
                    break;
                case "LauncherDisappeared":
                    _functionNameHash = 148436330;
                    break;
                case "MagHide":
                    _functionNameHash = 798565163;
                    break;
                case "MagIn":
                    _functionNameHash = 1258896930;
                    break;
                case "MagOut":
                    _functionNameHash = 1947938901;
                    break;
                case "MagShow":
                    _functionNameHash = 1551431816;
                    break;
                case "MessageName":
                    _functionNameHash = 1724675694;
                    break;
                case "MalfunctionOff":
                    _functionNameHash = 1041383721;
                    break;
                case "ModChanged":
                    _functionNameHash = 1199378086;
                    break;
                case "OffBoltCatch":
                    _functionNameHash = 2091345647;
                    break;
                case "OnBoltCatch":
                    _functionNameHash = -1518700811;
                    break;
                case "PutMagToRig":
                    _functionNameHash = 2146162989;
                    break;
                case "RemoveShell":
                    _functionNameHash = 211630556;
                    break;
                case "ReplaceSecondMag":
                    _functionNameHash = 443236115;
                    break;
                case "ShellEject":
                    _functionNameHash = -1819682913;
                    break;
                case "ShowAmmo":
                    _functionNameHash = -1662010579;
                    break;
                case "ShowMag":
                    _functionNameHash = 903577754;
                    break;
                case "SliderOut":
                    _functionNameHash = -1349190221;
                    break;
                case "SoundAtPoint":
                    _functionNameHash = -833254918;
                    break;
                case "StartUtilityOperation":
                    _functionNameHash = 1134400241;
                    break;
                case "ThirdAction":
                    _functionNameHash = -612501071;
                    Parameter.ParamType = (EAnimationEventParamType)1;
                    break;
                case "UseProp":
                    _functionNameHash = -1376281788;
                    Parameter.ParamType = (EAnimationEventParamType)4;
                    break;
                case "UseSecondMagForReload":
                    _functionNameHash = 67499002;
                    break;
                case "WeapIn":
                    _functionNameHash = -224219248;
                    break;
                case "WeapOut":
                    _functionNameHash = 1174204865;
                    break;
                case "Sound":
                    _functionNameHash = 1554795451;
                    Parameter.ParamType = (EAnimationEventParamType)3;
                    break;
                case "OnCurrentAnimStateEnded":
                    _functionNameHash = 1051598138;
                    break;
                case "OnSetActiveObject":
                    _functionNameHash = 1350263048;
                    break;
                case "OnDeactivateObject":
                    _functionNameHash = 1537525950;
                    break;
                case "OnBackpackDrop":
                    _functionNameHash = 224242560;
                    break;
                case "BipodOpen":
                    _functionNameHash = 1527071002;
                    break;
                case "BipodClose":
                    _functionNameHash = -378724418;
                    break;
                case "MisfireOff":
                    _functionNameHash = 1225921510;
                    break;
                case "JamOff":
                    _functionNameHash = -471759195;
                    break;
                default:
					_functionNameHash = 0;
                    Enabled = true;
                    break;
            }
        }
#endif
	}
}
