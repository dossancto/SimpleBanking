# Simple Banking Infra

## About

This layer contains all Non-Business Logics implementations. Such as Repositories or email providers

Keep this layer without any kind of business logics, the main responsibility of this layer is implementing interfaces required by usecases

Migrations are also stored here, because are specific of a determined database.

## Envs

This layer also handle Environment Variables, the only layers that requires Env variables are `Infra`, `DependencyInjection` and `Integration.Tests`

A Class is used to hold all environment variables, the `Anv` class have some helper methods to handling more easily environment variables, and is a single point of change for all project envs

