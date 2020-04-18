using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    bool TryAction(float workRate);
    MouseHoverCategories GetClickableCategory();
}
