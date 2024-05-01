# Simple Banking Application

## About

This class defines all UseCases Logics, so, class is the business Logic Layer

All usecases are package by feature, not by layer. So there is not a `UseCases` directory with all usecases.

Each Package (`Feature`) have his own `UseCases` folder, this contains a more elastic definition for all features. Each feature can also have a diferente internal structure, for better a domain representation.

UseCases in same module (`Feature`) can reference others usecases or repositories (in same module), But, other features cannot.

To this problem, we have the `Commands` and `Handlers`. They are resposable for handling module-external dependencies, as long they can be easily replaced by some http call or something, they are a simple API to consume other modules resources. 

Here, a representation of this concept.

