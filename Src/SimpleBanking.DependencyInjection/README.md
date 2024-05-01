# Simple Banking Dependency Injection

## About

This layer contains handle all dependency injection and instantiation rules. Configure the services start.

All Injection rules (that aren't defined by `UI` layers) must be instantiated here.

> This layer `CAN'T` have any Logic, it must only define models.
    This layer also `CAN'T` have `UI` specific dependencies, as long this will be reused for different `UIs`

