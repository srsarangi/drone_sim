# *Project Entry Points*  

**Experiment**    
Never fly drone alone  
Do calibrations before the first flight    
 

**Project Entry points in Simulator**      
You have to learn Unity to work with it just basic programming in C# will not work.      
This is because unity is based on class and inheritance concepts.      
All functionalities are implemented as a class.    
Please refer Unity manual of the correct version of the unity   
**Basic knowledge of how to make a unity project**  

***Refer to the project narrated slides to understand the concept used***    

***Brackeys Tutorial***  
Very good to start with videos 1-10     
https://www.youtube.com/watch?v=j48LtUkZRjU&list=PLPV2KyIb3jR5QFsefuO2RlAgWEz6EvVi6    

Car AI tutorial series for sensors and path  
https://www.youtube.com/watch?v=o1XOUkYUDZU    

Unity NavMesh Tutorial series   
https://www.youtube.com/watch?v=CHV1ymlw-P8    

How to move objects in unity    
https://www.youtube.com/watch?v=-thhMXmTM7Q  




***Structure of Simulator Project***    
	Project organised into different folders   
	Scripts folder holds all the scripts  

**DroneMovementscript** is responsible for manual flying of the drone   
https://www.youtube.com/watch?v=3R_V4gqTs_I  

**Drone waypoint movement**   
A drone cannot turn like cars and no standard way for drone movement is given  
I applied constant forward motion to move the drone   
This is the best way to move the drone   
I am using lookat API to make the drone look in the direction of the next node  
So move forward + look in the direction of the next node  makes the drone move   
FOr obstacle avoidance  

o MovementUpDown(); // for vertical movement of drone   
o MovementForward(); // for forward movement of drone   
o Swerwing(); // for sideways movement of drone   
o Rotation(); // For rotation of drone about its axis   
o DroneSound(); // to implement drone sound   
o ClampingSpeedValues(); // to clamp the max speed value   

**Expand Project**  

Make scripts per task, to make them run attach them to the relevant game object.   
The script should be modular so that the same functions can be used by multiple scripts.     
Use function definition (refer unity API )to expand it for numerous behaviour.  
