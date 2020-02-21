Solyanka
===

This project was started owing to (big thanks to):
  - @[jbogard](https://github.com/jbogard) and his ©[MediatR](https://github.com/jbogard/MediatR)
  - @[marshinov](https://habr.com/ru/users/marshinov/) and his [report](https://habr.com/ru/company/jugru/blog/447308/)

### Description:
Solyanka is a simple CQS/CQRS framework, supporting:
  - commands, queries, handlers and pipelines;
  - events (domain, application, integration) handling;
  - domain structures (DDD);
  - sending integration events over service bus

### Documentation:
  - [Handling queries, commands and events]()
  - [Building queries and commands pipelines]()
  - [Using IEventContainer to handle events]()
  - [Using IServiceBus to push events to another app]()

### Packages:
  - Core
    * Solyanka.Cqs.Abstractions
    * Solyanka.Events.Abstractions
    * Solyanka.Utils
  - Projects
    - Dispatcher
      * Solyanka.Dispatcher
      * Solyanka.Dispatcher.EventsPushing
    - ServiceBus
      * Solyanka.ServiceBus.Abstractions
      * Solyanka.ServiceBus
      
  Microsoft DI and other technologies:
  -Projects.Microsoft
    - Dispatcher
      * Solyanka.Dispatcher.Microsoft.DependencyInjection
    - ServiceBus
      * Solyanka.ServiceBus.Microsoft.DependencyInjection
      * Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection

### Samples
 will be someday

### In mind
- Validation
- Logging
- Errors handling