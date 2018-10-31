# What are Counters

A Counter is a dedicated tracking variable, attached to a document. Counters are usually used in order to maintain chosen statistics related to the documents they belong to, e.g. count how many times a document has been downloaded or evaluated.

You may create, update and remove counters manually using RavenDB's Studio, or integrate dedicated API methods into your client in order to automate counters management by predefined criteria.

## Advantages: "Cheap" and concurrent modification
####
### Uncostly management
All data related to a counter is stored in its document's meta-data, allowing RavenDB to modify the counter while keeping the document that owns it undisturbed.

While creating or removing a counter does require full document modification, modifying the counter does not - keeping it a "low-cost" database activity during most of its lifespan.
####
### Concurrent updating
A server within a cluster is not required to coordinate a counter-modification operation with other servers, making it possible for multiple servers to modify the same counter at the same time, or even modify the document a counter belongs to while the counter is being updated by another client or server. Counter modifications are then distributed by each server during the regular database replication routine.
