using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class PlatformTools : MonoBehaviour {

	// Use this for initialization
	void Start () {

		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Switch, "lol");






		foreach (BuildTargetGroup target in GetValues<BuildTargetGroup>()) {
			if (IsPlatformSupported (target)) {
				Debug.Log (target);
				Debug.Log(IsPlatformSupported(target));
				PlayerSettings.SetScriptingDefineSymbolsForGroup(target, "Sa marche ?");
			}

			continue;

			try {
				PlayerSettings.SetScriptingDefineSymbolsForGroup(target, "Sa marche ?");
			} catch(Exception e) {
				Debug.Log (e);
			}
		}


	}

	private bool IsPlatformSupported(BuildTargetGroup target) {
		var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
		var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
		var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTargetGroup", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
		return (bool)isPlatformSupportLoaded.Invoke(null,new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {target})});

	}

	public static IEnumerable<T> GetValues<T>() {
		return Enum.GetValues(typeof(T)).Cast<T>();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
