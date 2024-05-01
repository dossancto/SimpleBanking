# Simple Banking API

## About

This is a UI layer, this layer must only format input and outputs.

> **DONT APPLY ANY BUSINESS LOGIC IN THIS LAYER, THIS LAYER MUST BE EASILY REPLACEBLE**

This layer can have Middlewares, Handle external world requests and response them. 

This layer also have Message Broker Consumers.

This layer must only consume usecases or adapters, cannot use directly some infra. All services must be intanciated by `Dependency Injection`

This layer also builds and uses Dependency Injection layer
