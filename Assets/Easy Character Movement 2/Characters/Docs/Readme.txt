Easy Character Movement 2
-------------------------

VERSION 1.1.5
 
	* W A R N I N G !
 
	  Please remove the ECM2 folder from your unity project before importing a new package.
  
	- New Folders structure. This lets you import CharacterMovement package only or full Easy Character Movement 2 package.
 
	- Added Assembly Definitions for CharacterMovement and ECM2 packages.
	
	- Minor bug fixes.
 
	- Fixed. CharacterMovement.detectCollisions not re-enabling collider.
 
	- Fixed. A case where fast moving characters keep stuck on swimming movement mode when leaving a PhysicsVolume.



VERSION 1.1.3

	- Minor bug fixes.



VERSION 1.1.1

	Character:
	----------

	- Fixed a bug causing some flags not being applied on release build.


	Character Movement:
	-------------------

	- Updated HitLocation ids to be on par with Collisionflags.

	- Removed deltaTime propety, now its handled as optional parameter, if no used it defaults to Time.deltatime.

	- Move methods now returns CollisionFlags to be on par with Unity's Character Controller.

	- Added CharacterMovement component performance test.

	- Fixed some minor bugs.



VERSION 1.1.0

	* W A R N I N G !

	Starting with this version, ECM2 is now a fully kinematic character controller and as a such it present significative changes, so:

	PLEASE BACKUP BEFORE UPDATE!


  How to update ?
  ===============

  	- Its recomended to completeley remove the ECM2 folder from your unity project before import new package from store.

  	- Once removed, import the package from asset store as regular.

  	- Please refer to changes document for a detailed list of removed / deprecated files and data structures and its new counterparts.
  	


VERSION 1.0

	- Initial release.


DISCLAIMER & LEGAL INFORMATION

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.

YOU MAY NOT REDISTRIBUTE THIS SOURCE CODE IN WHOLE OR IN PART
WITHOUT WRITTEN CONSENT FROM THE CONTENT AUTHOR OR COPYRIGHT HOLDER.