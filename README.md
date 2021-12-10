# CarGoVrooom

Martin Blomquist Rendahl
This project is for my Design Patterns course in school.

This is a Prototype for a Car game. It doesnt have a complete gameloop, The focus of the project is on the Systems that was created for learning and implementing in other projects.

Patterns: 
State pattern: in the Update() method in 'TankVehicle.cs' and 'BlueVehicle.cs'. The movement of the car is based on a state design pattern, by pressing two diffirent buttons you can increase and decrease the movement speed of the car. The state variable is an int that can be -1(reverse), 0(Not moving), and 1-4(moving forward at diffirent speeds).

Composition pattern: in the 'TankVehicle.cs' and 'BlueVehicle.cs', both inheriting from the 'IVehicle.cs'. The methods that are inherited from the Interface are called in the 'PlayerCharacter.cs' Update() method. Iwanted the Tank and blue car to have some differences, so i couldn't use the same movement script. therfore i used the interface to make sure that the PlayerCharacter.cs can work with both.

Observer pattern: I created a Event delegate in the 'Obstacle.cs' which both 'UI.cs' and 'AchievmentManager.cs' are subscribed to. When the Destroyed() method in the 'Obstacle.cs' gets called the AddScore() method in both 'UI.cs' and 'AchievmentManager.cs' are called which increments the score variable by 1.

Assets are from a Unity course from https://learn.unity.com/
