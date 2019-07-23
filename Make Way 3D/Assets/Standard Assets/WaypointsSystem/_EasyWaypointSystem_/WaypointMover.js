//----------------------------------------------------------------------------------
// The most important script. 
// It manages waypointed path from waypointsHolder and move object along it.
// Script also allows to setup different following and looping types for  movement.
//----------------------------------------------------------------------------------

#pragma strict

// List of following types
public enum FollowType 
 { 
   Simple,		 // Just move object as it is (without any rotation or dumping)
   Facing,		 // Roughly face object on current waypoint
   SmoothFacing  // Face object on current waypoint and adapt path smoothly 
 }

// List of looping types
public enum LoopType 
 { 
   Once,		 // Only one cycle
   Cycled,		 // Infinite amounts of cycles
   PingPong,	 // Move object in another direction when it gets first/last point of path
   SeveralTimes  // Repeat loop several times (specified in numberOfLoops)
 }

// List of axes waypoint position along which should be ignored
class UsedAxis
{
  var x: boolean = false;
  var y: boolean = false;
  var z: boolean = false;
}
 
 
 
var waypointsHolder: WaypointsHolder;			// Move along the path holded in this WaypointsHolder
var followingType: FollowType;					// Choose one of following type to use
var loopingType: LoopType;						// Choose one of looping type to use
var MoveOnWayImmediately: boolean = false;		// Move object immediately to the first waypoint at start
var ignorePositionAtAxis: UsedAxis;				// Ignore waypoint position along those axis
var damping = 3.0;								// Smooth facing/movement value
var movementSpeed: float = 5.0;					// Speed of object movement along the path
var waypointActivationDistance: float = 1.0;	// How far should object be to waypoint for its activation and choosing new
var numberOfLoops: int = 0;						// How much loops should be performed before stop. Use this parameter if loopingType=SeveralTimes
var preventCollisionDistance: float;            // Object suspend movement if there is any obstacle on this distance (in front of him)

// Usefull internal variables, please don't change them blindly
private var currentWaypoint: int = 0;
private var direction: int = 1;
private var velocity = Vector3.zero;
private var targetPosition : Vector3;
private var delayTillTime: float;
private var loopNumber: int = 1;
private var inMove: boolean = false;
private var suspended: boolean = false;
private var previousWaypoint: int = 0;


//=============================================================================================================
// Setup initial data according to specified parameters
function Start () 
{
	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>()) GetComponent.<Rigidbody>().freezeRotation = true;
   	
   	yield WaitForSeconds(0.01);
   	if(MoveOnWayImmediately) transform.position = waypointsHolder.waypoints[0].gameObject.transform.position;
   	
   	targetPosition = waypointsHolder.waypoints[0].gameObject.transform.position;
   	 if (ignorePositionAtAxis.x) targetPosition.x = transform.position.x;
  	 if (ignorePositionAtAxis.y) targetPosition.y = transform.position.y;
  	 if (ignorePositionAtAxis.z) targetPosition.z = transform.position.z;
}

//----------------------------------------------------------------------------------
//Main loop
function Update () 
{
     
    var suspend: boolean = false;
    
    var hit : RaycastHit;
    var p1 : Vector3 = transform.position;
    var p2 : Vector3 = p1;
    
    // Cast character controller shape preventCollisionDistance meters forward, to see if it is about to hit anything
    if (preventCollisionDistance > 0)
	    if (Physics.CapsuleCast (p1, p2, 0.5, transform.forward, hit, preventCollisionDistance)) 
	      {
	        suspend = true;
	      }
    
    
 // Process movement if waypoint exists and there is no delay assigned to it
 if (!suspend && currentWaypoint >= 0 && delayTillTime<Time.time  && !suspended) 
  {
  	
  	inMove = true; 
  	
   // Activate waypoint when object is closer than waypointActivationDistance
   if(Vector3.Distance(transform.position, targetPosition) < waypointActivationDistance) 
    {
      // Init delay if it's specified in waypoint
      if (waypointsHolder.waypoints[currentWaypoint].delay>0) delayTillTime = Time.time + waypointsHolder.waypoints[currentWaypoint].delay;
      // Try to call function in object if there is any function name cpecified in waypoint "callFunction" parameter
      if (waypointsHolder.waypoints[currentWaypoint].callFunction != "") SendMessage (waypointsHolder.waypoints[currentWaypoint].callFunction, SendMessageOptions.DontRequireReceiver);
      
      // If waypoint have specified newMoverSpeed bigger than 0 -  change current WaipointMover speed 
      if(waypointsHolder.waypoints[currentWaypoint].newMoverSpeed > 0) ChangeWaypointMoverSpeed(waypointsHolder.waypoints[currentWaypoint].newMoverSpeed);
      
      // Select next waypoint according to direction 
      previousWaypoint = currentWaypoint;
      currentWaypoint += direction;
       
      // Choose next waypoint/actions according to loopingType, if object reaches first or last  waypoint
      if(currentWaypoint > waypointsHolder.waypoints.Length-1 || currentWaypoint<0)
  	    switch (loopingType)
  		   {
			  case LoopType.Once: 
			    currentWaypoint = -1;
			  break;
			  
			  case LoopType.Cycled:
			  	currentWaypoint = 0;
			  break;
			  
			  case LoopType.PingPong:
			  	 direction = -direction;
			  	 currentWaypoint += direction;
			  break;
			  
			  case LoopType.SeveralTimes:
			    if (loopNumber < numberOfLoops) 
			       {
			        currentWaypoint = 0;
			        loopNumber++;
			       }
			        else
			         currentWaypoint = -1;
			  break;
   		   }
   		   

   	  // Get/update next waypoint XYZ position in World coordinates
	   if(currentWaypoint>=0 && waypointsHolder.waypoints[currentWaypoint])
	   	    {  	 
		   	  targetPosition = waypointsHolder.waypoints[currentWaypoint].gameObject.transform.position;
		   	 
		   	  if (ignorePositionAtAxis.x) targetPosition.x = transform.position.x;
		  	  if (ignorePositionAtAxis.y) targetPosition.y = transform.position.y;
		  	  if (ignorePositionAtAxis.z) targetPosition.z = transform.position.z;
	  	    }
	  	   else
		     if (currentWaypoint < waypointsHolder.waypoints.Length && currentWaypoint>=0)
		          { 
			        currentWaypoint -= direction;
			        Debug.LogWarning("Waypoint is missed in " + waypointsHolder.gameObject.name);
			      }
  			   	
   }
    
             
    
// Choose or update rotation/facing according to facingType
	
	 // Look at and dampen the rotation
	  if (followingType == FollowType.SmoothFacing)
	   {
		 var rotation = Quaternion.LookRotation(targetPosition - transform.position);
		 transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			
		 transform.Translate(Vector3.forward*movementSpeed*Time.deltaTime);
	   }
		
	 // Just Look at	
	  if (followingType == FollowType.Facing)
	   {
		 transform.LookAt(targetPosition);
		 transform.Translate(Vector3.forward*movementSpeed*Time.deltaTime);
	   }
		

	 // Move without rotation
	  if (followingType == FollowType.Simple)  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, velocity, movementSpeed);
	
    
	
 }
 else 
   inMove = false;  


  
}

//----------------------------------------------------------------------------------
// Return object to position of previous waypoint
function ReturnToPreviousWaypoint () 
{
	  currentWaypoint = previousWaypoint;
	  transform.position = waypointsHolder.waypoints[previousWaypoint].gameObject.transform.position;
	  targetPosition = waypointsHolder.waypoints[previousWaypoint].gameObject.transform.position;

}

//----------------------------------------------------------------------------------
// Return true if object is moving now
function isMoving (): boolean 
{
	return inMove;
}

//----------------------------------------------------------------------------------
// Fully suspend waypoint-controlled movement
function Suspend (state: boolean) 
{
  suspended = state;
}


//----------------------------------------------------------------------------------
// Return true if object is moving now
function ChangeWaypointMoverSpeed (newSpeed:float) 
{
	movementSpeed = newSpeed;
}

//----------------------------------------------------------------------------------