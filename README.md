Solyanka
===
Here will be build and release pipelines statuses
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
  - [Handling domain events]()
  - [Handling application events]()
  - [Handling integration events]()

### Packages:
  - Core
    * Solyanka.Cqs.Abstractions
    * Solyanka.Events.Abstractions
    * Solyanka.Domain.Abstractions
    * Solyanka.Utils
  - Projects
    - Dispatcher
      * Solyanka.Dispatcher
    - Events
      * Solyanka.Events.DomainEvents
      * Solyanka.Events.ApplicationEvents
      * Solyanka.Events.IntegrationEvents
    - ServiceBus
      * Solyanka.ServiceBus.Abstractions
      * Solyanka.ServiceBus
      

  -Projects.Microsoft
    - Dispatcher
      * Solyanka.Dispatcher.Microsoft.DependencyInjection
    - Events
      * Solyanka.Events.DomainEvents.Microsoft.EntityFrameworkCore
    - ServiceBus
      * Solyanka.ServiceBus.Microsoft.DependencyInjection
      * Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection

### Samples
 will be someday