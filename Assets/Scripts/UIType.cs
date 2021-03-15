using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType 
{
    NULL = -1,
    GAMEUI = 0,
    MAINMENU = 1,
    LOADMENU = 2,
    OPTIONSMENU = 3,
    PAUSEMENU = 4,
    WINMENU = 5,
}

public enum UIChange
{
    SWITCH,
    DEACTIVATE,
    PREVIOUS
}
