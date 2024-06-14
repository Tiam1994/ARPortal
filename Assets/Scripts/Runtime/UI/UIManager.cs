using ARPortal.Constants;
using UnityEngine;

namespace ARPortal.Runtime.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private TooltipPanel _tooltipPanel;
		[SerializeField] private Sprite _tapSprite;

		public void ActivateTooltips()
		{
			ShowTooltipForRoomPlacement();
		}

		public void ShowTooltipForRoomPlacement()
		{
			_tooltipPanel.ShowTooltip(TooltipMessages.ROOM_PLACEMENT_TOOLTIP, _tapSprite, true);
		}

		public void ShowTooltipForInteractionObjects(string interactableObjectTag)
		{
			switch (interactableObjectTag)
			{
				case TagConstants.DOOR_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltip(TooltipMessages.DOOR_INTERACTION_TOOLTIP, _tapSprite, true);
					break;
				case TagConstants.CUPBOARD_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltip(TooltipMessages.CUPBOARD_INTERACTION_TOOLTIP, _tapSprite, true);
					break;
				case TagConstants.STANDING_LAMP_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltip(TooltipMessages.STANDING_LAMP_INTERACTION_TOOLTIP, _tapSprite, true);
					break;
				case TagConstants.TV_INTERACTABLE_COLLIDER:
					_tooltipPanel.ShowTooltip(TooltipMessages.TV_INTERACTION_TOOLTIP, _tapSprite, true);
					break;
				default:
					break;
			}
		}

		public void HideTooltips()
		{
			_tooltipPanel.HideTooltip();
		}
	}
}
