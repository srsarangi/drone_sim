# *Drone Simulator*

**Control Of Drone Swarm from Single Control Station**

![Drone Swarm](https://github.com/srsarangi/drone_sim/blob/main/Simulator/Assets/Images/MainImage.png)  

Drones have developed in recent years, especially small drones. Drones are very effective in modern day as they can be remotely controlled and are able to deliver the payload effectively.

Multiple drones when flown in coordination with each other constitute the drone swarm. This swarming of drones enhances the capabilities of flight packages and these could be used for defensive and offensive action based on the configuration of the package.

**Drone Project**

***Drone Flying.*** Project involved the practical part where the drone had to be flown  in different scenarios with varying velocity, angles,duration and paths.

***Simulator.*** The second part of the project was to build a simulator where different scenarios of drone flights are simulated. In this realistic drone from unity assets store Annanas proj was used as a base project to provide an environment to build upon.
https://assetstore.unity.com/packages/3d/vehicles/air/realistic-drone-66698

Drone model
https://assetstore.unity.com/packages/3d/props/red-drone-142071?free=true&q=drone&orderBy=1

The different scenarios simulated are as follows :-

1. **Manual flight.** Drones move using cursor movement keys and WASD keys, for rotation and Up down movement JLIK keys are used.
Tutorial Followed https://www.youtube.com/watch?v=3R_V4gqTs_I

2. **Autonomous Flight.** In this a circuit is made and a drone is made to follow the circuit. In this case the flight is totally autonomous.
    Tutorial Followed
    Path Creation
    https://www.youtube.com/watch?v=PiYffouHvuk  

    Sensor part rest modified to make drone fly and move from node to node
    https://www.youtube.com/watch?v=PiYffouHvuk


3.    **Follow the Leader.** IN this a drone is made the leader drone which will keep track of the nodes and the path information. Other drones just follow the leader drone with a certain distance in offset.

4. **Obstacle Avoidance.** Laser Sensors are simulated using raycast. When Object is in front the drone detects the object and stops. Further modifications were done and the drone was made to maneuver around the obstacle.
    
    1.     Drone detects objects using sensors.  

    2.     If an object is in front then stop and move right or top-right until the obstacle is clear.

    3.     When there is no obstacle in front then move to the next node in sequence.

5.    **Path Finding using Navmesh.** Unity NavMesh AI is used for auto path finding. Nav mesh uses A* algorithm for path finding. Further details can be viewed from
https://docs.unity3d.com/ScriptReference/AI.NavMesh.html

	1.	**Drone Swarming.** Single path in combination with follow the leader is used).

	2.	**Independent Drones.** Independent path for each drone implemented. The target for the drones is kept the same. Drones located at different locations of the scene take different paths for reaching their destination.


Credits :-

Contributors of Links as mentioned above against each work

Models of UTS pro


![Indian Institute of Technology Delhi Logo](https://github.com/srsarangi/drone_sim/blob/main/Simulator/Assets/Images/creditsImage.jpg)  
