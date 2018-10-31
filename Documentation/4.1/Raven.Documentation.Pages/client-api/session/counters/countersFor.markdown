# `CountersFor`
## Session Counters Management single entry point

##Functionality:
CountersFor is your single entry-point to counters management within a session.

It is a Session object, that conveniently refers you to a document's counters you wish to manage, and bundles-up a set of simple API methods you can manage these counters with.

##Usage:
To use it, open a session and load from the database a copy of the document whose counters you wish to manage.

After loading the document, provide `CountersFor` with the value returned by `session.Load`.

This will refer `CountersFor` to the document's counters, and allow you to use its methods to manage them: Create a counter, Delete an existing one, or modify a counter's value.

##The procedure:
* **Open a session.**
* **Use your session to load a copy of a document from the database.**
* **Create an instance of CountersFor, and provide it with a reference to the loaded document.**

**Note: `CountersFor` methods that create, remove or modify a counter, require the execution of `session.SaveChanges` before changes actually take effect.**

##Sample:
{CODE counters_region_CountersFor@ClientApi\Session\Counters\Counters.cs /}
