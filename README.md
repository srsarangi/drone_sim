# *Drone Simulator*

**Control Of Drone Swarm**

Drones have developed in recent years specially the small size drones. Drones are very effective in modern day as they can be remotely controlled and are able to deliver the payload effectively. 

Multiple drones when flown in coordination with each other constitute the drone swarm. This swarming of drones enhances the capabilities of flight packages and these could be used for defensive and offensive action based on the configuration of the package. 

*Drone Project* 

Project involved the practical part were in the drone had to be flown  in different scenarios with varying velocity, angles,deruation and paths.

The second part of the project was to build a simulator where 
different scenarios of drone flights are simulated.

The different scenarios simulated are as follows :- 
1. **Manual flight.** Drones moves using curser movement keys and WASD keys, for rotation and Up down movement JLIK keys are used.
Tutorial Followed https://www.youtube.com/watch?v=3R_V4gqTs_I

2. **Autonomous Flight.** In this a circuit is made and drone is made to follow the circuit. In this case the flight is totally autonomous.
	Tutorial Followed 
	Path Creation 
	https://www.youtube.com/watch?v=PiYffouHvuk  

	Sensor part rest modified to make drone fly and move from node to node
	https://www.youtube.com/watch?v=PiYffouHvuk


3.	**Follow the Leader.** IN this a drone is made the leader drone which will keep track of the nodes and the path information. Other drones jaust follow thw leader drone with a certain distance in offset.

4. **Obstacle Avoidance.** Lasser Sensors are simulated using raycast. When Object is in front the drone detects the object and stops. Further modifications were done and drone was made to manuver around the obstacle. 
	
	1.	 Drone detects object using sensors.  

	2.	 If object is in front then stop and move right or top-right until the obstacle is clear.

	3.	 When  no obstacle in front then move to next node in sequence.

5.	**Path Finding using Navmesh.** Unity NavMesh AI is used for auto path finding. Nav mesh uses A* algorithm for path finding. Further details can be viewed from 
https://docs.unity3d.com/ScriptReference/AI.NavMesh.html
	1.	**Drone Swarming.** Single path in combination with follow the leader is used).
	2. 	**Indipendent Drones.** Indipendent path for each drones implemented. The target for the drones are kept same. Drones located at different locations of the seen take different paths for reaching destination.


