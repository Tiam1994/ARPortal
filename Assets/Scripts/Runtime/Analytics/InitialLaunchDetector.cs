using UnityEngine;

namespace ARPortal.Runtime.Analytics
{
	public class InitialLaunchDetector
	{
		private bool _isFirstApplicationLaunch;

		private const string UNIQUE_IDENTIFIER_KEY = "UniqueAppIdentifier";

		public bool IsFirstApplicationLaunch { get { return _isFirstApplicationLaunch; } }

		public void InitializeUniqueIdentifier()
		{
			if (!PlayerPrefs.HasKey(UNIQUE_IDENTIFIER_KEY))
			{
				string uniqueIdentifier = System.Guid.NewGuid().ToString();

				PlayerPrefs.SetString(UNIQUE_IDENTIFIER_KEY, uniqueIdentifier);
				PlayerPrefs.Save();

				FirebaseEventManager.Instance.LogFirstApplicationLaunchEvent();
				_isFirstApplicationLaunch = true;
			}
			else
			{
				string uniqueIdentifier = PlayerPrefs.GetString(UNIQUE_IDENTIFIER_KEY);
				_isFirstApplicationLaunch = false;
			}
		}
	}
}
