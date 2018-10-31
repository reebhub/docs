# `GetAll`
## Get the full sequence of counter names (keys) and values, attached to a chosen document.

## Syntax
{CODE-BLOCK:json}
Dictionary<string, long?> GetAll()
{CODE-BLOCK/}

Return value: An array of counter names and values attached to a specified document.

##Usage:
the `GetAll` method is a member of the [CountersFor](../counters/CountersFor) session object.

To use it, open a session and load from the database a copy of the document whose counters you wish to observe.

Then, use `CountersFor.GetAll` to get a list of the document's counters' names and values.

##The procedure:
* **Open a session.**
* **Use your session to load a copy of a document from the database.**
* **use `CountersFor` to get a reference to the document's counters.**
* **Execute GetAll, to retrieve a list of this document's counters' names and values.**

##Sample:
{CODE counters_region_GetAll@ClientApi\Session\Counters\Counters.cs /}
