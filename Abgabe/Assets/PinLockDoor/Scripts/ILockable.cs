using UnityEngine;
using System.Collections;

// Implement this Interface in your classes to enalbe lock/unlockability by the pin code panel
public interface ILockable
{
	void Lock();
	void Unlock();
}