# DisorderPickers
Unity Jr. Programmer project
I created this game prototype using the base files from a Unity tutorial project. The original project contains a sandbox level where you can move the productivity unit (Manager) around 
and task the forklifts to gather resources and take them to the truck.

I came up with the idea of turning it into a small resource management game about picking orders from my personal experiences working as an order picker.
I designed the whole game loop and added the extra logic needed to make it work such as a fail and win state and tracking the state of the current goal 
(picking an order). I also added the UI needed to inform the players about their goal and keep track of it.

Due to the extensive amount of preexisting code, I couldn't figure out how to make the order requirements and the dropoff point's inventory be on display at all times,
so I had to leave it the way it is now, players have to click on each to see the status, which is not the UX I would have liked.

I also added a new unit, the Slacker, which greatly slows down resource productivity and can physically block forklifts from moving. This greatly improved gameplay,
players have to be more involved with the moment-to-moment state of the game instead of sitting back and letting the automated forklifts to everything.
Later I replaced manual goal creation with a randomized system, but chose not to implement scaling difficulty as it would be out of scope.

The order numbers are not entirely random since order numbers in actual warehouses do follow a logic, I made them use the current date as the starting digits. 


As visual flourishes I created the logo art myself, sourced a starting menu background and music. I chose to go with Legacy UI since the original project was created with it already.
I realized that when the Main scene is reloaded after completing an order, user input sometimes doesn't work, but couldn't figure out how to solve the issue and the prototype
already does what I designed it for, which was showing a working game loop and entry-level professional work with Unity/C# scripting.
