using ARPortal.Patterns.Singleton;
using System.Threading.Tasks;
using Firebase.Analytics;
using Firebase;

namespace ARPortal.Runtime.Analytics
{
	public class FirebaseEventManager : DontDestroySingleton<FirebaseEventManager>
	{
		public async Task InitializeAsync()
		{
			await FirebaseApp.CheckAndFixDependenciesAsync();
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		}

		public void LogInteractionEvent()
		{
			FirebaseAnalytics.LogEvent("object_interaction");
		}

		public void LogFirstApplicationLaunchEvent()
		{
			FirebaseAnalytics.LogEvent("first_open_app_event");
		}

		public void LogStartSessionEvent()
		{
			FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
		}

		public void LogEndSessionEvent(float timeSpent)
		{
			FirebaseAnalytics.LogEvent("time_spent_in_game", new Parameter("time_spent_seconds", timeSpent));
		}
	}
}
