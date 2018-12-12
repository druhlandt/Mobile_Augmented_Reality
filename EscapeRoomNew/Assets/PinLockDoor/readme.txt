Pin Lock Door
*************

Requirements
------------
> Script "PinCodeControl" veryfies an input string the a code string (including numbers)
> Script "ILockable" is an interface to be implemented by your gameobject that is lock/unlockable
> PanelDisplay (any standard UI->Text as output)
> InputPanel (Either standard UI buttons or gameobjects with touch functionality as provided in prefabs)
> An object to Lock / Unlock that implements Interface "ILockable" in an attached script

Setup
-----
1) place your door and implement Interface (ILockable) for lock() and unlock() calls
	alternatively you can use the demo "Door" prefab.
2) place your display and ensure to have a Text object to adress to (UI->Text)
	alternatively you can use the demo "PinLockDisplay" prefab.
	NOTE: the prefab does not contain an Eventsystem. Be sure to have one in your scene! (UI->Eventsystem)
3) place your pincode and attach the "PinCodeControl" Script
	you can also use the demo "PinLockPanel" or "PinLockPanelDetailed" prefab.
4) maintain public parameters of the PinLockControl Script. Be sure to link:
	Game Obj Display Text -> Your previously placed UI-Text
	Game Obj Door -> Your previously placed Door that has the Interface Implementation attached
	Audio* Sources -> for granted and denied warnings.
 	Maintain additional parameters to fit your needs.
 
Parameters
----------
lockCode	The current passcode that is set
maxAttempts	The amount of attempts allowed befor alerting and lockdown
controlCode	The code that needs to be entered after unlocking the door to change the passcode
LockDownTime	The amount of secondds that the door will remain in lockdown after failed attempts
Display*	The text's that appear on screen in a certain state

Note:
"SUBMIT" and "CLEAR" are dedicated strings that are sent to the panelcontrol by "command buttons".
If you intend to build a character keyboard keep that in mind and possibly change them.

The demo scene contains a complete scene to showcase the script.