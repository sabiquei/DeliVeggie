# DeliVeggie Evaluation project


## 1.  Introduction
This solution is built primarly using .NET Core, Angular and MongoDB. The application is used to list products and display details of the project. 

The solution has been pushed to a public repository given below. 
> https://github.com/sabiquei/DeliVeggie

## 2. Projects

### 2.1 DeliVeggie.Gateway

It's a web api service that exposes apis to get all products and get a product by it's Id. 
It's done using .NET Core 3.1. This api utilizes rabbitmq to communicate with Deliveggie.MicroService.

### 2.2 DeliVeggie.MicroService

This is a console application built using .NET Core 3.1. The application continusly listens to rabbitmq and also facilitates fetching data from Mongodb and returning it through DeliVeggie.Persistance.MongoDb. 
Business logic to calculate the price reduction is also part of this project. 
A unit test project DeliVeggie.MicroService.Tests has also been included. 

## 2.3 DeliVeggie.Persistance.MongoDb
This is .NET library project responsible for communicating with MongoDB and fetching product details.

MongoDb currently only has once collection named products. Details for PriceReductions has been moved to products iteslf as part of de-normalization. 

## 2.4 DeliVeggie.Client

This is an angular application for displaying products and product details. The application calls DeliVeggie.Gateway to get the product details. 

## 2.5 DeliVeggie.Shared

It's a class library used for creating models and classes which are to be shared by multiple projects.

## 3. Docker and Kubernetes

The project also includes files to deploy and test locally using docker and kubernetes. 

A docker-compose.yml file has been included in the root of the repository. 

Dockerfile are created for DeliVeggie.Gateway,  DeliVeggie.MicroService and DeliVeggie.Client projects. 

DeliVeggie.Client uses nginx for serving the angular applicaton.

The docker-compose.yml contains services for MongoDb and RabbitMq instances. 
Used mongo-express for creating entries manually in mongodb (This is optional). 

Sample data used

``` json
{
    _id: ObjectId('643aed2cdd210d3bf1a21f78'),
    Name: 'Uncle Ben\'s Rice',
    EntryDate: '2023-04-15T18:30:04.508Z',
    Price: 28.35,
    PriceReductions: [
        {
            DayOfWeek: 1,
            Reduction: 0
        },
        {
            DayOfWeek: 2,
            Reduction: 0
        },
        {
            DayOfWeek: 3,
            Reduction: 0
        },
        {
            DayOfWeek: 4,
            Reduction: 0
        },
        {
            DayOfWeek: 5,
            Reduction: 0
        },
        {
            DayOfWeek: 6,
            Reduction: 0.2
        },
        {
            DayOfWeek: 7,
            Reduction: 0.5
        }
    ]
}
```

Docker commands used.
```
    docker compose build
    docker compose up
```

The images built were pushed to a public docker repository in docker hub.

> docker pull sabiquei/deliveggie-client:v1

> docker pull sabiquei/deliveggie-gateway:v1

> docker pull sabiquei/deliveggie-microservice:v1

The Deployment folder contains three files
1. deliveggie-deployment.yml
2. mongodb-deployment.yml
3. rabitmq-deployment.yml

The deliveggie-deployment uses the images mentioned above to create the services and deployments. 

Commands to execute

```
kubectl apply -f rabbitmq-deployment.yml,mongodb-deployment.yml,deliveggie-deployment.yml  
```

After this is done, execute the follwing to get a list of pods
```
kubectl get pods
```

Then setup port forwarding for gateway and angular application using the follwing command.
Replace <CLIENT_POD_NAME> and <GATEWAY_POD_NAME> with appropriate pod names. 

```
kubectl port-forward <CLIENT_POD_NAME> 8005:8005

kubectl port-forward <GATEWAY_POD_NAME> 44350:80 
```






