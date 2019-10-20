using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoClass : MonoBehaviour {
	public static DemoClass Instance;

	public GameObject Functionality_Panel;
	public GameObject MainMenu_Panel;
	public InputField PIN;
	public Button SubmitPIN;

	public string CONSUMER_KEY;
	public string CONSUMER_SECRET;

	string UserID="USERID";
	string ScreenName="SCREENNAME";
	string Token="TOKEN";
	string TokenSecret="TOKENSECRET";

	Twitter.RequestTokenResponse m_RequestTokenResponse;
	Twitter.AccessTokenResponse m_AccessTokenResponse;


	public Image UserProfilePIC;
	public Text Username;
	public Text FollowersCount;
	public Text FollowingCount;

	public GameObject TweetPost_Panel;
	public GameObject SearchUserPanel;
	public GameObject DMPanel;
	public GameObject SearchHashtagPanel;
	public InputField Tweet;
	public Text TweetPostingCallback;

	//user search data
	public InputField UserSearchText;
	public Image SearchedUserPic;
	public Text SearchedUserName;
	public Text SearchUserFollowersCount;
	public Text SearchUserFollowingCount;
	public Text SearchCallBackText;
	public Text SearchedUserID;

	//send direct message data
	public InputField DirectMessageText;
	public InputField DirectMessageUserID;
	public Text DMCallBackText;

	//search hashtag data
	public InputField SearchHastTagText;
	public InputField SearchHashTagTimes;
	public Text SearchHashTagCallBackText;
	public GameObject TextToInstantiate;
	public GameObject ContentParent;


	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		Instance = this; 
		LoadTwitterUserInfo (); //load twitter user info if already registered
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SearchHashtagButton()
	{
		if (SearchHastTagText.text != null && SearchHashTagTimes.text != "") {
			StartCoroutine (Twitter.API.GetHashtag (SearchHastTagText.text,SearchHashTagTimes.text, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
				new Twitter.HashTagSearchCallback (this.OnHashTagSearched)));
		} else {
			Debug.Log ("Enter Search for HashTag");
			SearchHashTagCallBackText.text = "Enter Search/Number for HashTag";
			Invoke ("EmptyString", 2f);
		}
	}

	public void SearchUserOnTweeter()
	{
		if (UserSearchText.text != null && UserSearchText.text != "") {
			StartCoroutine (Twitter.API.SearchUser (UserSearchText.text, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
				new Twitter.UserSearchCallback (this.OnUserSearched)));
		} else {
			Debug.Log ("Enter Search");
			SearchCallBackText.text = "Enter Search";
			Invoke ("EmptyString", 2f);
		}
	}


	public void PostTweet()
	{
		if (Tweet.text != null && Tweet.text != "") {
			StartCoroutine (Twitter.API.PostTweet (Tweet.text, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
				new Twitter.PostTweetCallback (this.OnPostTweet)));
		} else {
			Debug.Log ("Enter Tweet");
			TweetPostingCallback.text = "Enter Tweet";
			Invoke ("EmptyString", 2f);
		}
	}


	public void SearchHashTagPnl(string task)
	{
		if (task == "IN") {
			SearchHashtagPanel.SetActive (true);
			Functionality_Panel.SetActive (false);
		} else if (task == "OUT") {
			SearchHashtagPanel.SetActive (false);
			Functionality_Panel.SetActive (true);
			SearchHastTagText.text = "";
			SearchHashTagTimes.text = "";
			GetProfile ();
		}
	}


	public void PostTweetPanel(string task)
	{
		if (task == "IN") {
			TweetPost_Panel.SetActive (true);
			Functionality_Panel.SetActive (false);
		} else if (task == "OUT") {
			TweetPost_Panel.SetActive (false);
			Functionality_Panel.SetActive (true);
			GetProfile ();
		}
	}

	public void SearchUserPanelButton(string task)
	{
		if (task == "IN") {
			SearchUserPanel.SetActive (true);
			Functionality_Panel.SetActive (false);
		} else if (task == "OUT") {
			SearchUserPanel.SetActive (false);
			Functionality_Panel.SetActive (true);
			SearchedUserPic.sprite = null;
			SearchedUserName.text = "";
			SearchUserFollowersCount.text = "";
			SearchUserFollowingCount.text = "";
			SearchedUserID.text = "";
			GetProfile ();
		}
	}

	public void DirectMessagePanel(string task)
	{
		if (task == "IN") {
			DMPanel.SetActive (true);
			Functionality_Panel.SetActive (false);
		} else if (task == "OUT") {
			DMPanel.SetActive (false);
			Functionality_Panel.SetActive (true);
			GetProfile ();
		}
	}


	public void EmptyString()
	{
		TweetPostingCallback.text = "";
		SearchCallBackText.text = "";
		DMCallBackText.text = "";
		SearchHashTagCallBackText.text = "";
	}

	void OnPostTweet(bool success)
	{
		print("OnMessageSend - " + (success ? "succedded." : "failed."));
		if (success) {
			TweetPostingCallback.text = "Tweet Posted Successfully!";
			Tweet.text = "";
			Invoke ("EmptyString", 2f);
		} else {
			TweetPostingCallback.text = "Posting Tweet Failed!";
			Tweet.text = "";
			Invoke ("EmptyString", 2f);
		}
	}

	void OnHashTagSearched(bool success)
	{
		print("Search HashTag - " + (success ? "succedded." : "failed."));
		if (success) {
			SearchHashTagCallBackText.text = "HashTag Searched Successfully!";
			SearchHastTagText.text = "";
			SearchHashTagTimes.text = "";
			Invoke ("EmptyString", 2f);
		} else {
			SearchHashTagCallBackText.text = "Searching HashTag Failed!";
			SearchHastTagText.text = "";
			SearchHashTagTimes.text = "";
			Invoke ("EmptyString", 2f);
		}
	}

	void OnUserSearched(bool success)
	{
		print("User Search - " + (success ? "succedded." : "failed."));
		if (success) {
			SearchCallBackText.text = "User Searched Successfully!";
			UserSearchText.text = "";
			Invoke ("EmptyString", 2f);
		} else {
			SearchCallBackText.text = "Searching User Failed!";
			UserSearchText.text = "";
			Invoke ("EmptyString", 2f);
		}
	}

	public void CallLater()
	{
		DMCallBackText.text = "Message sent to " +Twitter.API.NameOfRecipent+ " Successfully!";
//		Debug.Log ("inhere");
	}
	void OnMessageSent(bool success)
	{
		print("Message sending - " + (success ? "succedded." : "failed."));
		if (success) {
			Invoke ("CallLater", 1.5f);
			DirectMessageText.text = "";
			DirectMessageUserID.text = "";
			Invoke ("EmptyString", 5f);
		} else {
			DMCallBackText.text = "Message Sending Failed!";
			DirectMessageText.text = "";
			DirectMessageUserID.text = "";
			Invoke ("EmptyString", 2f);
		}
	}

	public void OnGettingProfile(bool Success)
	{
		print("Get Profile - " + (Success ? "succedded." : "failed."));
	}


	public void SendDirectMessage()
	{
		if (DirectMessageText.text != "" && DirectMessageUserID.text != "") {
			StartCoroutine (Twitter.API.SendDirectMessage (DirectMessageText.text,DirectMessageUserID.text, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
				new Twitter.PostDMCallback (this.OnMessageSent)));
		} else {
			Debug.Log ("Message or user ID empty");
			DMCallBackText.text="Message or user ID empty";
			Invoke ("EmptyString", 2f);
		}
	}
	public void GetProfile ()
	{
		StartCoroutine(Twitter.API.GetProfileInfo(PlayerPrefs.GetString(ScreenName),PlayerPrefs.GetString(UserID), CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
			new Twitter.PostGetProfileCallback(this.OnGettingProfile)));
	}

	void LoadTwitterUserInfo()
	{
		m_AccessTokenResponse = new Twitter.AccessTokenResponse();

		m_AccessTokenResponse.UserId        = PlayerPrefs.GetString(UserID);
		m_AccessTokenResponse.ScreenName    = PlayerPrefs.GetString(ScreenName);
		m_AccessTokenResponse.Token         = PlayerPrefs.GetString(Token);
		m_AccessTokenResponse.TokenSecret   = PlayerPrefs.GetString(TokenSecret);

		if (!string.IsNullOrEmpty (m_AccessTokenResponse.Token) &&
		    !string.IsNullOrEmpty (m_AccessTokenResponse.ScreenName) &&
		    !string.IsNullOrEmpty (m_AccessTokenResponse.Token) &&
		    !string.IsNullOrEmpty (m_AccessTokenResponse.TokenSecret)) {
			string log = "LoadTwitterUserInfo - succeeded";
			log += "\n    UserId : " + m_AccessTokenResponse.UserId;
			log += "\n    ScreenName : " + m_AccessTokenResponse.ScreenName;
			log += "\n    Token : " + m_AccessTokenResponse.Token;
			log += "\n    TokenSecret : " + m_AccessTokenResponse.TokenSecret;
			Debug.Log (log);

			OpenFunctionalityPanel ();
			GetProfile ();
		} else {
			Debug.Log ("User not Registered on Twitter");
			OpenMainMenuPanel ();
		}
	}

	public void SUBMITPIN()
	{
		if (PIN.text != null && PIN.text != "" && m_RequestTokenResponse!=null) {
			StartCoroutine (Twitter.API.GetAccessToken (CONSUMER_KEY, CONSUMER_SECRET, m_RequestTokenResponse.Token, PIN.text,
				new Twitter.AccessTokenCallback (this.OnAccessTokenCallback)));
		} else {
			Debug.Log ("PIN is empty or Invalid, kindly enter a valid PIN");
		}
	}

	void OnAccessTokenCallback(bool success, Twitter.AccessTokenResponse response)
	{
		if (success)
		{
			string log = "OnAccessTokenCallback - succeeded";
			log += "\n    UserId : " + response.UserId;
			log += "\n    ScreenName : " + response.ScreenName;
			log += "\n    Token : " + response.Token;
			log += "\n    TokenSecret : " + response.TokenSecret;
			Debug.Log(log);
			Debug.Log ("Name : "+response.ScreenName);
			m_AccessTokenResponse = response;

			//save values for next session
			PlayerPrefs.SetString(UserID, response.UserId);
			PlayerPrefs.SetString(ScreenName, response.ScreenName);
			PlayerPrefs.SetString(Token, response.Token);
			PlayerPrefs.SetString(TokenSecret, response.TokenSecret);

			OpenFunctionalityPanel ();
			GetProfile ();

		}
		else
		{
			Debug.Log("OnAccessTokenCallback - failed.");
		}
	}

	public void OpenFunctionalityPanel()
	{
		MainMenu_Panel.SetActive (false);
		Functionality_Panel.SetActive (true);
	}

	public void OpenMainMenuPanel()
	{
		PlayerPrefs.DeleteAll ();
		MainMenu_Panel.SetActive (true);
		Functionality_Panel.SetActive (false);
	}

	public void AuthenticateTwitter()
	{
		if (string.IsNullOrEmpty (CONSUMER_KEY) || string.IsNullOrEmpty (CONSUMER_SECRET)) {  //consumer key and secret are not placed
			string text = "You need to register your game or application first.\n Click on below link, register and fill CONSUMER_KEY and CONSUMER_SECRET of Demo game object. \n http://dev.twitter.com/apps/new";
			Debug.LogError (text);
		} else {
			StartCoroutine (Twitter.API.GetRequestToken (CONSUMER_KEY, CONSUMER_SECRET,  //Start autherization if consumer key and secret are filled
			new Twitter.RequestTokenCallback (this.OnRequestTokenCallback)));
		}
	}

	void OnRequestTokenCallback(bool success, Twitter.RequestTokenResponse response)
	{
		if (success)
		{
			string log = "OnRequestTokenCallback - succeeded";
			log += "\n    Token : " + response.Token;
			log += "\n    TokenSecret : " + response.TokenSecret;
			Debug.Log(log);

			m_RequestTokenResponse = response;

			Twitter.API.OpenAuthorizationPage(response.Token);
		}
		else
		{
			Debug.Log("OnRequestTokenCallback - failed.");
		}
	}



}
