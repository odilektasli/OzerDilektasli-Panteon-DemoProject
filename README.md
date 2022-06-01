# Platform Runner game for 'Panton' developed by Özer DİLEKTAŞLI 

The windows build can be find in the ProjectBuild folder.

You are the player and you need to reach to finish point by avoiding obstacles.

#You can control the character by hold pressing left mouse click and moving the mouse left or right. 
Hint: For rotating platform you need to move mouse while presisng, opposite way of the platform where it rotates.

It is aimed to creat generic and readable code. In order to do that a manager scriptable object is constructed and used C# events to provide communication between components. In this way observer pattern was tried to implement. (Also called middleman as far as I know)

Because this is a hypercasual game, I wanted feel player more satisfied so I used particle effects on hits. To particle effects EpicToonFx asset is used.

Instatiation and Destroying of this particle effects my cause performance problems. Hence an ObjectPooling mechanism is constructed.

Once the objects that the developer want to be in pool is assigned to game manager script(Inside of the MainCamera object in the scene) pool is ready to use.

![resim](https://user-images.githubusercontent.com/61044813/171409539-37c73a1c-5d5e-4376-b08b-eb8ff0708c01.png)

After this assigning game manager constructs ObjectPoolHandler class with the number of PoolingObjectList count and instantiates objects in seperate classes.

The generalized function can be used by giving a position vector where the object you want to be and its index from the pooling object list the.

Example usage of the pooling mechanism:

![resim](https://user-images.githubusercontent.com/61044813/171411111-23304e1b-6277-444d-9715-8ae382fe241e.png)

 So in index 1 there is "DustDirtyPoof" particle is exist, it will be on the given position.
 
So in this way almost totally generic pooling is constructed.

For the ai that avoids obstacles, navmesh agent and navmesh obstacle is used. Thus its performance it is not perfect for moving object, I wanted to make them stronger by tracing ray. They have a calculation mechanism. According to that calculation they can change their location by suddenly sliding.

![resim](https://user-images.githubusercontent.com/61044813/171413705-d0b64cf0-9998-44da-bbfa-cf4cf9c6c320.png)

For the rotating platform tehere is also another mechanism that fixes agent's rotation by time. So the don't straigtly walking on platform. Their rotation is almost perfectly fixes.

RotationStick obstacle is added but interaction is not implemented due to unpredicted technical issues. Its script exist that adds force but its commented out.


It was a really fun and challanging assignment. I hope I have an opportunity to meet with dear Panteon team more closely.

Kind regards,

Özer DİLEKTAŞLI




 
