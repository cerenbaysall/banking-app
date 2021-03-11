# banking-app

# About The Project

This application is an example to use Mediator with .NET Core Web API and CQRS pattern with event sourcing by Kafka. The application contains two separated API as an AccountAPI and TransactionAPI. 
Basically, the client user can create a bank account with an existing customer by using AccountAPI and see the transaction of the process of creating the account by using TransactionAPI.
To create transactions, we do not call the TransactionAPI service directly, the main point we want to reach out to is to achieve creating transactions by using Kafka. TransactionAPI contains a background service to listen to Kafka's topic therefore it knows an operation is completed automatically.


# Built With

* [ASP.NET Core Web API](https://docs.microsoft.com/tr-tr/aspnet/core/web-api/?view=aspnetcore-5.0)
* [CQRS](https://martinfowler.com/bliki/CQRS.html#:~:text=CQRS%20stands%20for%20Command%20Query,you%20use%20to%20read%20information.)
* [MediatR](https://codeopinion.com/why-use-mediatr-3-reasons-why-and-1-reason-not/)
* [Docker](https://www.docker.com/)
* [Swagger](https://swagger.io/)
* [Unit Testing with Moq](https://softchris.github.io/pages/dotnet-moq.html#references)
* [Apache Kafka](https://kafka.apache.org/)
* [Confluent](https://www.confluent.io/)

# Getting Started

The application contains Docker-compose file to up Kafka and Zookeeper. After run docker-compose, you should run the APIs. 

```sh
   docker compose up
   BankingApp.AccountAPI % dotnet run
   BankingApp.TransactionAPI % dotnet run
```


# Installation

1. Clone the repo
   ```sh
   git clone https://github.com/cerenkeles/banking-app.git
   ```
2. Go to the project location
   ```sh
   cd banking-app
   ```
3. Run the docker compose command here
   ```sh
   docker compose up
   ```
4. Go to the AccountAPI location and run API
   ```sh
   cd BankingApp.AccountAPI
   dotnet run
   ```
5. Go to the TransactionAPI location and run API
   ```sh
   cd BankingApp.TransactionAPI
   dotnet run
   ``` 

# Usage

1. The application database context serves you 2 customers to test the create account behavior. You can use them with below information;

  ```sh
   {
      Name = "John",
      Surname = "Smith",
      CustomerNo = "Customer-7f7156354028"
   }
  ```
  ```sh
   {
      Name = "Ada",
      Surname = "Smith",
      CustomerNo = "Customer-41ee5d1ac2a8"
   }
  ```
  
2. After run the API projects, you should create an account by using CreateAccount method. You can get the Postman collection to test them by importing the exported collection with below URL ; 

  https://www.getpostman.com/collections/50745e950aea7dbb06f9

3. A background service listens the Kafka topic to create transactions, so after create an account, you can just call GetAccountTransactions method to see the transaction created automatically.


# Contact

https://github.com/cerenkeles
