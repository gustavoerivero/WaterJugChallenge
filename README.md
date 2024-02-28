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

```
{
	"message": string, // Indicates if the challenge could be solved, has no solution or, some validation message.
	"data": null | object // If there is no solution, it returns a null value. Otherwise, it returns an array with the steps to solve the challenge with the passed values and reach the desired amount.
}
```

The array that returns data in case of solution, contains a JSON with the following values:

```
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

```
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

A number of test cases that can be applied to evaluate the performance of the developed API are indicated.

### Positive Cases

#### 1. First Positive Case

In the first case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 2,
  "yCapacity": 10,
  "zTarget": 4
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "Solved.",
  "data": [
    {
      "jugXVolume": 2,
      "jugYVolume": 0,
      "action": "Fill X: (2, 0)"
    },
    {
      "jugXVolume": 0,
      "jugYVolume": 2,
      "action": "Transfer X to Y: (0, 2)"
    },
    {
      "jugXVolume": 2,
      "jugYVolume": 2,
      "action": "Fill X: (2, 2)"
    },
    {
      "jugXVolume": 0,
      "jugYVolume": 4,
      "action": "Transfer X to Y: (0, 4)"
    }
  ]
}
```

As can be seen, in the last step, being step 4, the indicated value of ```zTarget``` is reached, which is ```4```.

#### 2. Second Positive Case

In the second case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 25,
  "yCapacity": 10,
  "zTarget": 5
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "Solved.",
  "data": [
    {
      "jugXVolume": 25,
      "jugYVolume": 0,
      "action": "Fill X: (25, 0)"
    },
    {
      "jugXVolume": 15,
      "jugYVolume": 10,
      "action": "Transfer X to Y: (15, 10)"
    },
    {
      "jugXVolume": 15,
      "jugYVolume": 0,
      "action": "Empty Y: (15, 0)"
    },
    {
      "jugXVolume": 5,
      "jugYVolume": 10,
      "action": "Transfer X to Y: (5, 10)"
    }
  ]
}
```

As can be seen, in the last step, being step 4, the indicated value of ```zTarget``` is reached, which is ```5```.

#### 3. Third Positive Case 

In the third case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 2,
  "yCapacity": 100,
  "zTarget": 94
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "Solved.",
  "data": [
    {
      "jugXVolume": 0,
      "jugYVolume": 100,
      "action": "Fill Y: (0, 100)"
    },
    {
      "jugXVolume": 2,
      "jugYVolume": 98,
      "action": "Transfer X to Y: (2, 98)"
    },
    {
      "jugXVolume": 0,
      "jugYVolume": 98,
      "action": "Empty X: (0, 98)"
    },
    {
      "jugXVolume": 2,
      "jugYVolume": 96,
      "action": "Transfer X to Y: (2, 96)"
    },
    {
      "jugXVolume": 0,
      "jugYVolume": 96,
      "action": "Empty X: (0, 96)"
    },
    {
      "jugXVolume": 2,
      "jugYVolume": 94,
      "action": "Transfer X to Y: (2, 94)"
    }
  ]
}
```

As can be seen, in the last step, being step 4, the indicated value of ```zTarget``` is reached, which is ```94```.

### Negative Cases

#### 1. First Negative Case

In the first case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 2,
  "yCapacity": 6,
  "zTarget": 5
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "No solution.",
  "data": null
}
```

There is no solution for the indicated values. This is because although there is a greatest common divisor between the jugs, the remainder between the searched value Z and the greatest common divisor is non-zero. 

#### 2. Second Negative Case

In the second case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 35,
  "yCapacity": 45,
  "zTarget": 55
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "No solution.",
  "data": null
}
```

There is no solution for the indicated values. This is caused by the fact that the Z searched value is greater than the maximum capacity of both jugs.

#### 3. Third Negative Case

In the third case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": -5,
  "yCapacity": 10,
  "zTarget": 5
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "The values of the capabilities and the searched value must be greater than zero.",
  "data": null
}
```

The algorithm cannot be executed because all indicated values must be integers greater than zero.

#### 4. Fourth Negative Case

In the fourth case, the following values will be entered in the endpoint payload.

```json
{
  "xCapacity": 10,
  "yCapacity": null,
  "zTarget": 5
}
```

The result obtained by the endpoint would be:

```json
{
  "message": "The values of the capabilities and the searched value are required",
  "data": null
}
```

Returns an exception error, indicating that a value is missing in the payload sent, specifically yCapacity. This error occurs when a required value is not sent.

---
⌨️ made with ❤️ by [gustavoerivero](https://github.com/gustavoerivero)