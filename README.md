# *Drone Simulator*

**Control Of Drone Swarm from Single Control Station**

![Drone Swarm](https://github.com/srsarangi/drone_sim/blob/main/Simulator/Assets/Images/MainImage.png)  

Drones have developed in recent years, especially small drones. Drones are very effective in modern day as they can be remotely controlled and are able to deliver the payload effectively.

Multiple drones when flown in coordination with each other constitute the drone swarm. This swarming of drones enhances the capabilities of flight packages and these could be used for defensive and offensive action based on the configuration of the package.

### Also Read https://github.com/srsarangi/drone_sim/blob/48100867fe6beff3c7244b775a7828a5dcc4b4b4/Simulator/ProjectEntryPoints.md to get started on this project.

**Drone Project**
## Version 1(START)
***System Configuration for Simulator***  
Tested on   
Unity  Editor Version            OS  
Unity 2018.4.18f1                Windows 10  
Unity 2018.4.31f1                Ubuntu 18.04  

***System Requirements for Simulator***  
Unity editor 2018.4.x.x windows / ubuntu OS  
Text editor for C#   

***How to run Simulator***  
Install Unity in OS and open the simulator folder  
Select Welcome screen press play  
Navigate to required scene from welcome screen  

***Structure of Simulator Project***  
	Scripts are placed in Scripts folder  
	Scene are placed in Drone Project/Simulator/Assets/Realistic Drone/scenes folder  
	Old Annanas Proj scene Drone Project/Simulator/Assets/Realistic Drone/scenes/Old RealisticDrone Scene  
	car and People models are placed in Drone Project/Simulator/Assets/UTS_PRO/Models  
	RawImages folder contain raw images for drone camera project  
	Prefabs are placed in prefab folder  
	Abandoned buildings prefabs and scene placed in Drone Project/Simulator/Assets/Abandoned buildings  
	Materials and Shaders are placed in their respective folder  
 

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


3.    **Follow the Leader.** In this a drone is made the leader drone which will keep track of the nodes and the path information. Other drones just follow the leader drone with a certain distance in offset.

4. **Obstacle Avoidance.** Laser Sensors are simulated using raycast. When Object is in front the drone detects the object and stops. Further modifications were done and the drone was made to maneuver around the obstacle.
    
    1.     Drone detects objects using sensors.  

    2.     If an object is in front then stop and move right or top-right until the obstacle is clear.

    3.     When there is no obstacle in front then move to the next node in sequence.

5.    **Path Finding using Navmesh.** Unity NavMesh AI is used for auto path finding. Nav mesh uses A* algorithm for path finding. Further details can be viewed from
https://docs.unity3d.com/ScriptReference/AI.NavMesh.html  

		1.	**Drone Swarming.** Single path in combination with follow the leader is used).

		2.	**Independent Drones.** Independent path for each drone implemented. The target for the drones is kept the same. Drones located at different locations of the scene take different paths for reaching their destination.
## Version 1(END)


## Version 2(START)
***System Configuration for Simulator***  
Tested on   
Unity  Editor Version            OS  
Unity 2020.3.33                Windows 10  
Unity 2020.3.33                Ubuntu 18.04  

***System Requirements for Simulator***  
Unity editor 2020.3.33 windows / ubuntu OS  
Text editor for C#   

***How to run Simulator***  
Install Unity in OS and open the simulator v2 folder  
Navigate to Asset folder and then to Scene folder and then to new scene
Select the scene which you want to open.

***Structure of Simulator Project***  
	Scripts are placed in Scripts folder  
	Scene are placed in Drone Project/Simulator/Assets/scenes/new scenes folder    
	car and People models are placed in Drone Project/Simulator/Assets 
	RawImages folder contain raw images for drone camera project  
	Prefabs are placed in prefab folder  
	Abandoned buildings prefabs and scene placed in Drone Project/Simulator/Assets/Abandoned buildings  
	Materials and Shaders are placed in their respective folder

## Version 2(END)


## Version 3(START)
***System Configuration for Simulator***  
Tested on   
Unity  Editor Version            OS  
Unity 2020.3.40                Windows 10


***How to run Simulator***  
Install Unity in OS and open the simulator v2 folder  
Navigate to Asset folder and then to Scene folder and then to new scene
Select the scene which you want to open.


***For running C++ plugin***  
If you want to runn C++ plugin for Depthmap calculation. Code is provided for c++. Make .dll plugin with that before opening project in unity. Then, save that DLL file along with standard opencv dll into Assets/plugin folder

***Structure of Simulator Project***  
	Scripts are placed in Scripts folder  
	Scene are placed in Drone Project/Simulator/Assets/scenes/new scenes folder    
	Their are final four Scene in four different environment. Check Folder Assets/scenes/new scenes/GPS-abled and Assets/scenes/new scenes/GPS-denied. All the scripts associated with them are final scripts.


## Version 3(END)

Credits :-

Contributors of Links as mentioned above against each work

Models of UTS pro - for testing to be replaced before release
  
Nikita Bhamu Student B.Tech+M.Tech  CSE IITD  
Harshit Verma Student M.Tech CSE IITD

Kishore Yadav Student M Tech CSE IITD  
Akanksha Dixit Student PHD CSE IITD
Diksha Moolchandani Student PHD  CSE IITD  
Prof. SR Sarangi CSE IITD   

