using ARPortal.Patterns.Singleton;
using Firebase.Analytics;
using UnityEngine;
using Firebase;

namespace ARPortal.Runtime.Analytics
{
	public class FirebaseEventManager : DontDestroySingleton<FirebaseEventManager>
	{
		private const string UNIQUE_IDENTIFIER_KEY = "UniqueAppIdentifier";

		private float _sessionStartTime;

		public void LogInteractionEvent()
		{
			FirebaseAnalytics.LogEvent("object_interaction");
		}

		public void Initialize()
		{
			FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
			{
				FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
				InitializeUniqueIdentifier();
				StartSession();
			});
		}

		private void InitializeUniqueIdentifier()
		{
			if (!PlayerPrefs.HasKey(UNIQUE_IDENTIFIER_KEY))
			{
				string uniqueIdentifier = System.Guid.NewGuid().ToString();

				PlayerPrefs.SetString(UNIQUE_IDENTIFIER_KEY, uniqueIdentifier);
				PlayerPrefs.Save();

				FirebaseAnalytics.LogEvent("first_open_app_event");

				Debug.Log("First run of the application. A unique identifier is created and saved: " + uniqueIdentifier);
			}
			else
			{
				string uniqueIdentifier = PlayerPrefs.GetString(UNIQUE_IDENTIFIER_KEY);

				Debug.Log("The application has been launched before. Unique identifier: " + uniqueIdentifier);
			}
		}

		private void StartSession()
		{
			_sessionStartTime = Time.time;
			FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
		}

		public void EndSession()
		{
			float timeSpent = Time.time - _sessionStartTime;
			FirebaseAnalytics.LogEvent("time_spent_in_game", new Parameter("time_spent_seconds", timeSpent));
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				EndSession();
			}
			else
			{
				StartSession();
			}
		}
	}
}
