//----------------------------------------------------------------------------------------------
// Simple example script to handle character animation
// Please update it or create new according to requirements of your project
//----------------------------------------------------------------------------------------------


#pragma strict


var waypointMover: WaypointMover;

var idleAnimation: AnimationClip;
var moveAnimation: AnimationClip;
var jumpAnimation: AnimationClip;
var jumpForce: Vector3;

private var inSpecialAction: boolean = false;

//----------------------------------------------------------------------------------
function Update () 
{
  if(!inSpecialAction)
     if (waypointMover.isMoving()) Move(); else Idle();
   else
     if (!GetComponent.<Animation>().isPlaying) inSpecialAction = false;
}

//----------------------------------------------------------------------------------
function Idle () 
{
 if (!GetComponent.<Animation>().isPlaying || GetComponent.<Animation>().IsPlaying(moveAnimation.name)) GetComponent.<Animation>().Play(idleAnimation.name);
}



function Move () 
{
 if (GetComponent.<Animation>().isPlaying) GetComponent.<Animation>().Play(moveAnimation.name);
}



function Jump() 
{
 inSpecialAction = true;
 GetComponent.<Animation>().Play(jumpAnimation.name);
 GetComponent.<Rigidbody>().AddRelativeForce(jumpForce, ForceMode.Impulse);
}
//----------------------------------------------------------------------------------