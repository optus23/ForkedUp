#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used
#pragma warning disable 0429 //never used

/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace AppAdvisory.BlackMonsterPack
{
	[InitializeOnLoad]
	public class Welcome : EditorWindow 
	{
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
//		private const string ONLINE_DOC_URL = "XXXXXXX_YOUR_HTML_DOC_LINK_HERE_XXXXXXX";
		private const string RATEUS_URL = "http://u3d.as/C9L";

		private const string NAME_OF_THE_GAME = "Black Monster Mega Pack";
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		/******* TO MODIFY **********/
		private const string VERYSIMPLEAD_URL = "http://u3d.as/oWD";
		private const string VERYSIMPLELEADERBOARD_URL = "http://u3d.as/qxf";
		private const string VERYSIMPLESHARE_URL = "http://u3d.as/u3N";
		private const string VERYSIMPLEGIF_URL = "http://u3d.as/ACQ";
		private const string VERYSIMPLERATE_URL = "http://u3d.as/Dt2";

		private const string FACEBOOK_URL = "https://facebook.com/appadvisory";
		private const string REQUEST_URL = "https://appadvisory.zendesk.com/hc/en-us/requests/new";
		private const string APPADVISORY_UNITY_CATALOG_URL = "http://bit.ly/2ee6aed";//"http://u3d.as/9cs";
		private const string COMMUNITY_URL = "https://appadvisory.zendesk.com/hc/en-us/community/topics";
		private const string LINKEDIN_URL = "https://www.linkedin.com/in/ganbarouch";

		private const float width = 600;
		private const float height = 760;

		public const string PREFSHOWATSTARTUP = "AppAdvisory" + NAME_OF_THE_GAME + ".PREFSHOWATSTARTUP";

		public static bool showAtStartup;
		private static GUIStyle imgHeader;
		private static bool interfaceInitialized;
		private static Texture adsIcon;
		private static Texture leaderboardIcon;
		private static Texture shareIcon;
		private static Texture gifIcon;
		private static Texture onlineDocIcon;
		private static Texture moreGamesIcon;
		private static Texture rateIcon;
		private static Texture communityIcon;
		private static Texture topicIcon;
		private static Texture questionIcon;
		private static Texture facebookIcon;
		private static Texture vsrateIcon;
		private static Texture linkedinIcon;


		[MenuItem("Tools/APP ADVISORY/"+NAME_OF_THE_GAME+"/Welcome Screen", false, 0)]
		[MenuItem("Window/APP ADVISORY/"+NAME_OF_THE_GAME+"/Welcome Screen", false, 0)]
		public static void OpenWelcomeWindow()
		{
			GetWindow<Welcome>(true);
		}

		static Welcome(){}

		//call from Autorun
		public static void OpenPopupStartup()
		{
			showAtStartup = EditorPrefs.GetInt(PREFSHOWATSTARTUP, 1) == 1;
			OpenWelcomeWindow();
		}

		void OnEnable(){
			#if UNITY_5_3_OR_NEWER
			titleContent = new GUIContent("Welcome To " + NAME_OF_THE_GAME); 
			#endif

			maxSize = new Vector2(width, height);
			minSize = maxSize;
		}	

		public void OnGUI(){

			InitInterface();
			GUI.Box(new Rect(0, 0, width, 60), "", imgHeader);
			GUILayoutUtility.GetRect(position.width, 50);
			GUILayout.Space(30);

			GUILayout.BeginHorizontal();

			if (Button2(adsIcon)){
				Application.OpenURL(VERYSIMPLEAD_URL);
			}

			if (Button2(leaderboardIcon)){
				Application.OpenURL(VERYSIMPLELEADERBOARD_URL);
			}

			if (Button2(shareIcon)){
				Application.OpenURL(VERYSIMPLESHARE_URL);
			}

			if (Button2(gifIcon)){
				Application.OpenURL(VERYSIMPLEGIF_URL);
			}

			if (Button2(vsrateIcon)){
				Application.OpenURL(VERYSIMPLERATE_URL);
			}


//			if (Button(adsIcon,"WANT TO MONETIZE THIS ASSET?","Get 'Very Simple Ads' on the Asset Store and earn money in a minute!")){
//				Application.OpenURL(VERYSIMPLEAD_URL);
//			}
//
//			if (Button(leaderboardIcon,"WANT TO ADD A LEADERBOARD?","Get 'Very Simple Leaderboard' on the Asset Store!")){
//				Application.OpenURL(VERYSIMPLELEADERBOARD_URL);
//			}
//
//			if (Button(shareIcon,"WANT TO ADD A SOCIAL SHARING BUTTON TO SHARE LIVE GAME SCREENSHOTS?","Get 'Very Simple Share' on the Asset Store!")){
//				Application.OpenURL(VERYSIMPLESHARE_URL);
//			}
//
//			if (Button(gifIcon,"WANT TO RECORD AND SHARE ANIMATED GIF OF YOUR GAME?","Get 'Very Simple GIF' on the Asset Store!")){
//				Application.OpenURL(VERYSIMPLEGIF_URL);
//			}

			GUILayout.EndHorizontal();




			GUILayout.BeginVertical();
			if (Button(communityIcon,"Join the community and get access to direct download","Be informed of the latest updates.")){
				Application.OpenURL(COMMUNITY_URL);
			}

			if (Button(rateIcon,"Rate this asset","Write us a review on the asset store.")){
				Application.OpenURL(RATEUS_URL);
			}

			if (Button(moreGamesIcon,"More Unity assets from us","Have a look to our Unity's Asset Store catalog!")){
				Application.OpenURL(APPADVISORY_UNITY_CATALOG_URL);
			}

			if (Button(facebookIcon,"Facebook page","Follow us on Facebook.")){
				Application.OpenURL(FACEBOOK_URL);
			}

//			if (Button(onlineDocIcon,"Online documentation","Read the full documentation.")){
//				Application.OpenURL(ONLINE_DOC_URL);
//			}

			if (Button(questionIcon,"A request?","Don't hesitate to contact us.")){
				Application.OpenURL(REQUEST_URL);
			}

			if (Button(linkedinIcon,"My Linkedin","For professional purpose only")){
				Application.OpenURL(LINKEDIN_URL);
			}

			GUILayout.Space(3);

			bool show = GUILayout.Toggle(showAtStartup, "Show at startup");
			if (show != showAtStartup){
				showAtStartup = show;
				int i = GetInt(showAtStartup);
				Debug.Log("toggle i = " + i);
				EditorPrefs.SetInt(PREFSHOWATSTARTUP, i);
			}

			GUILayout.EndVertical();

		}

		int GetInt(bool b)
		{
			if(b)
				return 1;
			else
				return 0;
		}

		void InitInterface(){

			if (!interfaceInitialized){
				imgHeader = new GUIStyle();
				imgHeader.normal.background = (Texture2D)Resources.Load("appadvisoryBanner");
				imgHeader.normal.textColor = Color.white;

				adsIcon = (Texture)Resources.Load("btn_monetization") as Texture;
				leaderboardIcon = (Texture)Resources.Load("btn_leaderboard") as Texture;
				shareIcon = (Texture)Resources.Load("btn_share") as Texture;
				gifIcon = (Texture)Resources.Load("btn_gif") as Texture;
				vsrateIcon = (Texture)Resources.Load("btn_vsrate") as Texture;
				onlineDocIcon = (Texture)Resources.Load("btn_onlinedoc") as Texture;
				communityIcon = (Texture)Resources.Load("btn_community") as Texture;
				moreGamesIcon = (Texture)Resources.Load("btn_moregames") as Texture;
				rateIcon = (Texture)Resources.Load("btn_rate") as Texture;
				questionIcon = (Texture)Resources.Load("btn_question") as Texture;
				facebookIcon = (Texture)Resources.Load("btn_facebook") as Texture;
				linkedinIcon = (Texture)Resources.Load("btn_linkedin") as Texture;

				interfaceInitialized = true;
			}
		}

		bool Button(Texture texture, string heading, string body, int space=0){

			GUILayout.BeginHorizontal();

			GUILayout.Space(54);
			GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(45));
			GUILayout.Space(10);

			GUILayout.BeginVertical();
			GUILayout.Space(1);
			GUILayout.Label(heading, EditorStyles.boldLabel);
			GUILayout.Label(body);
			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

			var rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

			bool returnValue = false;
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)){
				returnValue = true;
			}

			GUILayout.Space(space);

			return returnValue;
		}

		bool Button2(Texture texture){

			GUILayout.BeginHorizontal();

			float size = 100f; //UnityEngine.Random.Range(60f,100f);

			GUILayout.Space(5);
			GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(size));
			GUILayout.Space(10);


			GUILayout.EndHorizontal();

			var rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

			bool returnValue = false;
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)){
				returnValue = true;
			}


			return returnValue;
		}
	}
}