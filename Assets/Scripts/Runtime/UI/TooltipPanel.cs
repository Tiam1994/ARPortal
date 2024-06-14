using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace ARPortal.Runtime.UI
{
	public class TooltipPanel : MonoBehaviour
	{
		[SerializeField] private GameObject _content;
		[SerializeField] private TMP_Text _tooltipMessage;
		[SerializeField] private Image _tooltipImage;
		[SerializeField] private Animator _animator;

		private readonly int _imageScalingParametrHash = Animator.StringToHash("IsImageScaling");

		public void ShowTooltipWhitoutImage(string tooltipMessage)
		{
			_content.SetActive(true);
			_tooltipImage.gameObject.SetActive(false);

			_tooltipMessage.text = tooltipMessage;
		}

		public void ShowTooltipWithImage(string tooltipMessage, Image tooltipImage, bool imageAnimationEnabled)
		{
			_content.SetActive(true);
			_tooltipImage.gameObject.SetActive(true);

			_tooltipMessage.text = tooltipMessage;
			_tooltipImage = tooltipImage;

			ImageAnimationSwitcher(imageAnimationEnabled);
		}

		public void HideTooltip()
		{
			_content.SetActive(false);
		}

		private void ImageAnimationSwitcher(bool isEnabled)
		{
			_animator.SetBool(_imageScalingParametrHash, isEnabled);
		}
	}
}
