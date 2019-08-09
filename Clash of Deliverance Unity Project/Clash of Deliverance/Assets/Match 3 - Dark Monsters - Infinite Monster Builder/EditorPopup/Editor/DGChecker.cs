#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used
#pragma warning disable 0429 //never used

/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   
 * Facebook: https://facebook.com/appadvisory															   
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           
 ***********************************************************************************************************/




using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppAdvisory.BlackMonsterPack
{
	[InitializeOnLoad]
	public class DGChecker : EditorWindow 
	{
		private const string AADotween = "AADOTWEEN";

		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		public const bool needDotween = false;
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/

		public static void OpenWelcomeWindow()
		{
			GetWindow<DGChecker>(true);
		}

		static DGChecker()
		{
		}



		//call from Autorun
		public static void OpenPopupDGCHECKERStartup()
		{
			if(needDotween)
				EditorApplication.update += CheckItNow;
		}

		protected static List<string> GetFilesDOTweenDLL()
		{
			List<string> fileList = new List<string>();

			string path = Application.dataPath;

			if (Directory.Exists (path + "/Demigiant/DOTween")) {


			try
			{
			

				IEnumerable<string> files = Directory.GetFiles(path + "/Demigiant/DOTween", "DOTween.dll", SearchOption.AllDirectories);
//					.Where(s => s.CompareTo("DOtween.dll") == 0);

				//					.Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".aif") || s.EndsWith(".ogg"));

				foreach (string f in files)
				{
					fileList.Add(f);
				}


				return fileList;
			}
			catch (UnauthorizedAccessException UAEx)
			{
//				Console.WriteLine(UAEx.Message);
				UnsetScriptingDefineSymbols ();

			}
			catch (PathTooLongException PathEx)
			{
//				Console.WriteLine(PathEx.Message);
				UnsetScriptingDefineSymbols ();

			}

				return null;
			}

			return new List<string>();
		} 

		protected static List<string> GetFilesDOTweenSettings()
		{
			List<string> fileList = new List<string>();

			string path = Application.dataPath; 

			if (Directory.Exists (path + "/Resources")) {
				try {

					IEnumerable<string> files = Directory.GetFiles (path + "/Resources", "*.asset", SearchOption.AllDirectories);
//					.Where(s => s.CompareTo("DOTweenSettings") == 0);

					//					.Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".aif") || s.EndsWith(".ogg"));

					foreach (string f in files) {
						fileList.Add (f);
					}


					return fileList;
				} catch (UnauthorizedAccessException UAEx) {
//				Console.WriteLine(UAEx.Message);
					UnsetScriptingDefineSymbols ();

				} catch (PathTooLongException PathEx) {
//				Console.WriteLine(PathEx.Message);
					UnsetScriptingDefineSymbols ();

				}

				return null;
			}

			return new List<string>();
		}

		public static void CheckItNow()
		{
			if(!needDotween)
				return;

			EditorApplication.update -= CheckItNow;


			if (GetFilesDOTweenDLL ().Count > 0 && GetFilesDOTweenSettings ().Count > 0)
			{
				SetScriptingDefineSymbols ();

				GetWindow<DGChecker>(true).Close(); 


				return;
			} 
			else 
			{ 
				UnsetScriptingDefineSymbols ();


				if (GetFilesDOTweenDLL ().Count <= 0) {
					OpenWelcomeWindow();
					Debug.LogError ("Please DOWNLOAD AND INSTALL Dotween to use this asset : http://u3d.as/aZ1 ");
				} else if (GetFilesDOTweenSettings ().Count <= 0) {
					Debug.LogError ("Please SETUP Dotween to use this asset : TOOLS/Demigiant/DOTween Utility panel, then SETUP");
					GetWindow<DGChecker>(true).Close(); 
				} else {

				}
			}
		}

		static void SetScriptingDefineSymbols () 
		{

			foreach (BuildTargetGroup target in GetValues<BuildTargetGroup>()) 
			{
//				Debug.Log (target);
//				Debug.Log(IsPlatformSupported(target));
//
				if (IsPlatformSupported (target)) 
				{

					SetSymbolsForTarget (target, AADotween);
					//					SetSymbolsForTarget (target, "Works");
				}

			}


//			//			if (Directory.Exists ("Assets/Demigiant"))
//			//			{
//			SetSymbolsForTarget (BuildTargetGroup.Android, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.iOS, AADotween); 
//			SetSymbolsForTarget (BuildTargetGroup.WSA, AADotween);
//			#if !UNITY_5_5_OR_NEWER
//			#if !UNITY5_0 && !UNITY_5_1
//			SetSymbolsForTarget (BuildTargetGroup.Nintendo3DS, AADotween);
//			#endif
//			SetSymbolsForTarget (BuildTargetGroup.PS3, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.XBOX360, AADotween);
//			#endif
//			SetSymbolsForTarget (BuildTargetGroup.PS4, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.PSM, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.PSP2, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.SamsungTV, AADotween); 
//			SetSymbolsForTarget (BuildTargetGroup.Standalone, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.Tizen, AADotween);
//			#if !UNITY5_0 && !UNITY_5_1
//			SetSymbolsForTarget (BuildTargetGroup.tvOS, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.WiiU, AADotween);
//			#endif
//			SetSymbolsForTarget (BuildTargetGroup.WebGL, AADotween);
//			SetSymbolsForTarget (BuildTargetGroup.XboxOne, AADotween);
//
//			#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3
//			SetSymbolsForTarget (BuildTargetGroup.WebPlayer, AADotween);
//			#endif
		}


		static void SetSymbolsForTarget(BuildTargetGroup target, string scriptingSymbol)
		{
			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

			string sTemp = scriptingSymbol;

			if(!s.Contains(sTemp))
			{

				s = s.Replace(scriptingSymbol + ";","");

				s = s.Replace(scriptingSymbol,"");  

				s = scriptingSymbol + ";" + s;

				PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);
			}
		}
			

		private static bool IsPlatformSupported(BuildTargetGroup target) {
			var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
			var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTargetGroup", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			return (bool)isPlatformSupportLoaded.Invoke(null,new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {target})});

		}

		public static IEnumerable<T> GetValues<T>() {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		static void UnsetScriptingDefineSymbols () 
		{

			foreach (BuildTargetGroup target in GetValues<BuildTargetGroup>()) 
			{
//				Debug.Log (target);
//				Debug.Log(IsPlatformSupported(target));

				if (IsPlatformSupported (target)) 
				{
					
					UnsetSymbolsForTarget (target, AADotween);
//					SetSymbolsForTarget (target, "Works");
				}
						
			}
//
//
//			//			if (Directory.Exists ("Assets/Demigiant"))
//			//			{
//			UnsetSymbolsForTarget (BuildTargetGroup.Android, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.iOS, AADotween); 
//			UnsetSymbolsForTarget (BuildTargetGroup.WSA, AADotween);
//			#if !UNITY_5_5_OR_NEWER
//			#if !UNITY5_0 && !UNITY_5_1
//			UnsetSymbolsForTarget (BuildTargetGroup.Nintendo3DS, AADotween);
//			#endif
//			UnsetSymbolsForTarget (BuildTargetGroup.PS3, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.XBOX360, AADotween);
//			#endif
//			UnsetSymbolsForTarget (BuildTargetGroup.PS4, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.PSM, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.PSP2, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.SamsungTV, AADotween); 
//			UnsetSymbolsForTarget (BuildTargetGroup.Standalone, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.Tizen, AADotween);
//			#if !UNITY5_0 && !UNITY_5_1
//			UnsetSymbolsForTarget (BuildTargetGroup.tvOS, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.WiiU, AADotween);
//			#endif
//			UnsetSymbolsForTarget (BuildTargetGroup.WebGL, AADotween);
//			UnsetSymbolsForTarget (BuildTargetGroup.XboxOne, AADotween);
//
//			#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3
//			UnsetSymbolsForTarget (BuildTargetGroup.WebPlayer, AADotween);
//			#endif
		}
			

		static void UnsetSymbolsForTarget(BuildTargetGroup target, string scriptingSymbol)
		{
			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

			string sTemp = scriptingSymbol;

			if(s.Contains(sTemp))
			{
				s = s.Replace(scriptingSymbol + ";","");

				s = s.Replace(scriptingSymbol,"");  

				PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);
			}
		}

		void OnEnable()
		{
			#if UNITY_5_3_OR_NEWER
			titleContent = new GUIContent("Please install Dotween to use this asset"); 
			#endif
		}

		public void OnGUI()
		{
			GUILayoutUtility.GetRect(position.width, 50);
			GUILayout.Space(10);
			GUILayout.BeginVertical();

			if(GUILayout.Button("GET\nDOTWEEN\n(it's free)",  GUILayout.Width(100), GUILayout.Height(50)))
			{
				Application.OpenURL("http://u3d.as/aZ1");
			}
		}

	}
}   