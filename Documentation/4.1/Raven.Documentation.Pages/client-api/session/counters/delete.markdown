#`Delete`
## Remove a counter currently attached to a document.

## Syntax
{CODE-BLOCK:json}
void Delete(string counterName)
{CODE-BLOCK/}

`counterName`: A string with the counter's name

##Usage:
Use Delete() to completely remove a counter from a document's meta-data.

Delete is a member of the [CountersFor](../counters/CountersFor) session object.

To use it, open a session and load from the database a copy of the document whose counter you wish to remove.

Then, use `CountersFor.Delete` to remove the counter's data from the document's meta-data.

**Note: Changes will only take effect once you execute the session's SaveChanges() method.**

**Note: Deleting a document, deletes its counters as well.**

##The procedure:

* **Use your session to load a copy of a document from the database.**
* **use the session's `CountersFor` object to get a reference to the document's counters.**
* **use `CountersFor`'s `Delete` method, with the counter's name as an argument.**
* **Execute session.SaveChanges() to implement the changes.**

##Sample:
{CODE counters_region_Delete@ClientApi\Session\Counters\Counters.cs /}
