using ARPortal.Constants;
using UnityEngine.UI;
using UnityEngine;

namespace ARPortal.Runtime.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private TooltipPanel _tooltipPanel;
		[SerializeField] private Image _tapImage;

		public void ShowTooltipForRoomPlacement()
		{
			_tooltipPanel.ShowTooltipWhitoutImage(TooltipMessages.ROOM_PLACEMENT_TOOLTIP);
		}

		public void ShowTooltipToInteractionObjects(string interactableObjectTag)
		{
			switch (interactableObjectTag)
			{
				case TagConstants.DOOR_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltipWithImage(TooltipMessages.DOOR_INTERACTION_TOOLTIP, _tapImage, true);
					break;
				case TagConstants.CUPBOARD_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltipWithImage(TooltipMessages.CUPBOARD_INTERACTION_TOOLTIP, _tapImage, true);
					break;
				case TagConstants.STANDING_LAMP_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltipWithImage(TooltipMessages.STANDING_LAMP_INTERACTION_TOOLTIP, _tapImage, true);
					break;
				case TagConstants.TV_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltipWithImage(TooltipMessages.TV_INTERACTION_TOOLTIP, _tapImage, true);
					break;
			}
		}
	}
}
