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

```JSON
{
	"message": string, // Indicates if the challenge could be solved, has no solution or, some validation message.
	"data": null | JSON // If there is no solution, it returns a null value. Otherwise, it returns an array with the steps to solve the challenge with the passed values and reach the desired amount.
}
```

The array that returns data in case of solution, contains a JSON with the following values:

```JSON
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

```JSON
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

### Installation 🔧

## Deployment 📦 

## Test Cases

---
⌨️ made with ❤️ by [gustavoerivero](https://github.com/gustavoerivero)