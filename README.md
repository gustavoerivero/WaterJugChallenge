# WaterJugChallenge

## Challenge Description

The water jugs challenge is a classic mathematical puzzle that challenges reasoning and planning skills. It comes in different versions, but the essence is the same:

You have two jugs of water with no measuring marks, but with different capacities. The goal is to obtain a specific amount of water in one of the jugs by performing a series of basic operations:

- Fill one jug completely.
- Emptying a jug completely.
- Transferring the contents of one jug into the other until one of them is full or the other is empty.


You are not allowed to measure the amount of water in any way, you can only use the above operations. The key to solving the problem is to carefully plan a sequence of steps that will lead you to the final goal.

## API Description

This REST API is intended to return a JSON containing two key values;

```json
{
	"message": string, // Indicates if the challenge could be solved, has no solution or, some validation message.
	"data": null | JSON // If there is no solution, it returns a null value. Otherwise, it returns an array with the steps to solve the challenge with the passed values and reach the desired amount.
}
```

The array that returns data in case of solution, contains a JSON with the following values:

```json
{ 
	"jugXAmount": integer, // Amount of water in jug X.
	"jugYAmount": integer, // Amount of water in jug Y.
	"action": string // Description of the action to perform. 
}
```

It should be noted that the possible actions are: ```Fill```, ```Empty``` and ```Transfer```, followed by an indication of which jug performs the action and the values of the jugs.

Also, the REST API handles a single endpoint, this being a POST method to api/WaterJug.

```http
  POST /api/WaterJug
```

The endpoint receives the following payload in its body:

```json
{ 
	"xCapacity": integer, // Capacity of jug X 
	"yCapacity": integer, // Capacity of jug Y 
	"zTarget": integer // Target amount of water in jug Z 
}
```

### Algorithmic Approach

In order for the implemented algorithm to be executed, it must first meet certain conditions:

Each of the requested payload values are required and must be integers greater than zero. If any of them are not specified, a code 400 error will be returned indicating that one or more values are missing. In case the number indicated is less than or equal to zero, it will indicate that all requested values must be greater than zero. 

On the other hand, other validations are also fulfilled. The value of ```zTarget``` must be less than or equal to at least one of the two jars, otherwise, the returned message will be ```No solution```.

In addition, if the above validation is satisfied, the remainder between ```zTarget``` and the Greatest Common Divisor between the capacity of jug X and the capacity of jug Y must be equal to zero, otherwise, the returned message will be ```No solution```.




## Getting Started 🚀

Now, in order to execute the REST API, it is necessary to follow the following steps:

### Prerequisites 📋

To run the REST API on a local computer, the following is required:

- [.NET 7.0.16](https://dotnet.microsoft.com/es-es/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/downloads/)

### Installation 🔧

To begin with, we proceed with the download of the repository. To do this, open the console, go to the folder where you want to save the project and execute the following command:

```
git clone https://github.com/gustavoerivero/WaterJugChallenge.git
```

Now, the project is opened using the IDE used, which was Visual Studio 2022. Now, the project is opened using the IDE used, which was Visual Studio 2022. To do this, start the IDE and select the option ```Open a project or a solution```, where a small window will open where the user will have to select the .sln format file located in the folder of the recently cloned project.

After this, the project will be displayed in the IDE.

Now, it is necessary to open the Visual Studio console. To do this, in the options bar located at the top edge of the IDE window, go to the ```Tools``` tab, go to ```NuGet Package Manager``` and go to the ```Package Manager Console``` option. This will open a console in the bottom section of the IDE. 

Once the console is open, the command is displayed:

```
dotnet restore
```

This command restores the dependencies of the project, which will allow its execution.

## Deployment 📦 

To run the REST API, you need to go to the top toolbar of the IDE, go to the option labeled ```https``` or the one with a light green "play" symbol, and select the ```https``` option and then run by pressing this button.

The REST API will start running and a tab will open in the browser with the following address: ```https://localhost:7274/swagger/index.html```, providing the user with the facility to test the endpoints contained in the REST API.

Once the user is in the Swagger tab of the API, he will have to deploy the ```WaterJug``` list and then, deploy the ```POST``` list of the ```WaterJug``` method. Here, the user will be able to run the endpoint with the desired values and get the responses generated by the REST API.

## Test Cases

---
⌨️ made with ❤️ by [gustavoerivero](https://github.com/gustavoerivero)