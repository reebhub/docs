# `Get`
## Retrieve the value of a chosen counter.

## Syntax
{CODE-BLOCK:json}
long Get(string counterName)
{CODE-BLOCK/}

`counterName`: A string with the counter's name

Return value: The counter's current value

##Usage:

`Get` is a member of the [CountersFor](../counters/CountersFor) session object.

To use it, open a session and load from the database a copy of the document whose counter you wish to retrieve.

Then, use `CountersFor.Get` to get the counter's value, with its name as an argument.


##The procedure:
* **Open a session.**
* **Use your session to load a copy of a document from the database.**
* **use the session's `CountersFor` object, to get a reference to the document's counters.**
* **Execute `CountersFor.Get`, with the counter's name as an argument.**
* **The method returns the counter's value.**

##Sample:
{CODE counters_region_Get@ClientApi\Session\Counters\Counters.cs /}
