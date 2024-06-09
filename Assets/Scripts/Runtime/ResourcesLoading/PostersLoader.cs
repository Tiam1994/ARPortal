using System.Collections.Generic;
using UnityEngine.Networking;
using ARPortal.Extensions;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;

namespace ARPortal.Runtime.ResourcesLoading
{
	public class PostersLoader : MonoBehaviour
	{
		[SerializeField] private string _url;
		[SerializeField] private List<Image> _images;

		private Coroutine _loadDataCoroutine;
		private Coroutine _loadImagesCoroutine;

		public void DownloadImages()
		{
			this.KillCoroutine(ref _loadDataCoroutine);
			this.KillCoroutine(ref _loadImagesCoroutine);

			_loadDataCoroutine = StartCoroutine(LoadImagesFromURL(_url));
		}

		private IEnumerator LoadImagesFromURL(string url)
		{
			UnityWebRequest request = UnityWebRequest.Get(url);
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning("URL loading error");
			}
			else
			{
				var json = JSON.Parse(request.downloadHandler.text);
				List<string> imageURLs = new List<string>();

				foreach (var item in json)
				{
					string fileName = item.Value["name"].ToString().Trim('"');

					if (item.Value["type"] == "file" && (fileName.EndsWith(".png") || fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg")))
					{
						string downloadUrl = item.Value["download_url"];
						imageURLs.Add(downloadUrl);
					}
				}

				for (int i = 0; i < _images.Count; i++)
				{
					if (i < _images.Count)
					{
						_loadImagesCoroutine = StartCoroutine(DownloadImage(imageURLs[i], _images[i]));
					}
					else
					{
						_images[i].sprite = null;
					}
				}
			}
		}

		private IEnumerator DownloadImage(string url, Image uiImage)
		{
			UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(request.error);
			}
			else
			{
				Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
				uiImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
		}
	}
}
