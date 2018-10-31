# `Increment`
## Modifying a counter's value.

## Syntax
{CODE-BLOCK:json}
void Increment(string counterName, long count = 1)
{CODE-BLOCK/}

`counterName`: A string with the counter's name

`count`: A number to add to the counter's value

##Usage:
Increment is a member of the [CountersFor](../counters/CountersFor) session object.

To use it, open a session and load from the database a copy of the document whose counter you wish to modify.

Then, use `CountersFor.Increment` to increase the value of a counter of this document by a value of your choice.

Changes will only take effect once you execute the session's SaveChanges() method.

##The procedure:
* **Open a session.**
* **Use your session to load a copy of a document from the database.**
* **use the session's `CountersFor` object to get a reference to the document's counters.**
* **use `CountersFor`'s `Increment` method, with the counter's name and value-increase as arguments.**
* **Execute session.SaveChanges() to implement the changes.**

##Sample:
{CODE counters_region_Increment@ClientApi\Session\Counters\Counters.cs /}

##Notes:
* `Increment` is also used to [create a new counter](../counters/increment#create-a-counter).
* Modifying counters values using `Increment` only takes effect when `session.SaveChanges()` is executed.
* To **decrease** a counter's value, provide 'Increment' with a negative number.
{CODE-BLOCK:json}
documentCounters.Increment("DaysLeft", -10);
{CODE-BLOCK/}

{PANEL: Creating a counter}

There is normally no need to explicitly create a counter, since executing [Increment](../counters/increment) either updates the value of an existing counter, or creates a new counter if a counter by that name didn't exist.

A newly-created counter is given the incrementation number as its initial value.

Let's consider a counter triggered by a "Like" click, for example.
{CODE-BLOCK:json}
documentCounters.Increment("LikesCounter", 1);
{CODE-BLOCK/}
`LikesCounter` is set properly either way:

If it existed when a "Like" click triggered `Increment`, it has now been correctly increased by 1.

If it didn't exist, it has now been created and given the initial value of 1, to count the first "Like" click.

Here are a few additional examples:
{CODE-BLOCK:json}
documentCounters.Increment("groupsCounter", 10);
documentCounters.Increment("newCounter", 0);
documentCounters.Increment("phaseCounter");
{CODE-BLOCK/}
`groupsCounter` is created with an initial value of 10, or 10 is added to its current value.

`newCounter` is created with an initial value of 0, or if it existed - its value remains uncahnged.

`phaseCounter` is created with an initial value of 1, or 1 is added to its current value - because 1 is `Increment`'s default incrementation.
{PANEL/}
