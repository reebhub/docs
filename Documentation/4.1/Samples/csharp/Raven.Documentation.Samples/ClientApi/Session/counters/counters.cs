using System;
using Raven.Client.Documents;
using Raven.Client;
using System.Linq;
using System.IO;
using System.Collections.Generic;
//using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Operations.Counters;

namespace Rvn.Ch02
{
    class Program
    {
        static void Main(string[] args)
        {
            var docStore = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "db1"
            };
            docStore.Initialize();

            #region counters_region_CountersFor
            using (var session = docStore.OpenSession())
            {
                var document = session.Load<SupportCall>("SupportCalls/33-A");
                var documentCounters = session.CountersFor(document);
                documentCounters.Delete("Likes"); // Delete a counter named "Likes"
                documentCounters.Increment("Modified", 15); // Add 15 to the value of the counter "Modified"
                var counter = documentCounters.Get("DaysLeft"); // Retrieve the value of the counter "DaysLeft"
                session.SaveChanges();
            }
            #endregion

            // remove a counter from a document
            #region counters_region_Delete
            using (var session = docStore.OpenSession())
            {
                var document = session.Load<SupportCall>("SupportCalls/33-A");
                var documentCounters = session.CountersFor(document);

                documentCounters.Delete("counter");
                session.SaveChanges();
            }
            #endregion

            // Increment a counter's value
            #region counters_region_Increment
            using (var session = docStore.OpenSession())
            {
                var document = session.Load<SupportCall>("SupportCalls/33-A");
                var documentCounters = session.CountersFor(document);
                documentCounters.Increment("Likes"); // Increase "Likes" by 1
                documentCounters.Increment("Dislikes", 1); // Increase "Dislikes" by 1
                documentCounters.Increment("Views", 15); // Increase "Views" by 15
                documentCounters.Increment("DaysLeft", -10); // Decrease "DaysLeft" by 10
                session.SaveChanges();
            }
            #endregion

            // Increment a counter's value
            #region counters_region_Create
            using (var session = docStore.OpenSession())
            {
                var document = session.Load<SupportCall>("SupportCalls/33-A");
                var documentCounters = session.CountersFor(document);
                documentCounters.Increment("newCounter", 10); // create a new counter named "newCounter" for the document "SupportCalls/33-A", with a value of 10.
                documentCounters.Increment("anotherNewCounter", 0); // create "anotherNewCounter", with a value of 0. If this counter already exists, leave its value unchanged.
                session.SaveChanges();
            }
            #endregion

            // get a counter's value by the counter's name
            #region counters_region_Get
            using (var session = docStore.OpenSession())
            {
                var document = session.Load<SupportCall>("SupportCalls/33-A");
                var documentCounters = session.CountersFor(document);

                // retrieve and show the value of a counter named "c1"
                var documentCounter = documentCounters.Get("c1");
                Console.WriteLine("counter value: " + documentCounter);
                Console.ReadKey();
            }
            #endregion

            // GetAll
            #region counters_region_GetAll
            using (var session = docStore.OpenSession())
            {
                // load document
                var document = session.Load<SupportCall>("SupportCalls/33-A");

                // Use CountersFor and GetAll() to get the full sequence of counters' names (keys) and values attached to this document.
                var documentCounters = session.CountersFor(document);
                var countersSequence = documentCounters.GetAll();

                // list counters' names and values
                foreach (var counter in countersSequence)
                {
                    Console.WriteLine("counter name: " + counter.Key + ", counter value: " + counter.Value);
                }
            }
            #endregion

            // playing with GettAll
            /*
            using (var session = docStore.OpenSession())
            {

                // load all objects of a specific database
                Console.WriteLine("list all documents in a collection");
                var documentsList = session.Advanced.LoadStartingWith<SupportCall>("SupportCalls/");
                foreach (var someRecord in documentsList)
                {
                    var documentID = someRecord.Id;
                    //Console.WriteLine();
                    Console.WriteLine("\ndocument ID: " + documentID);

                    var document = session.Load<SupportCall>(documentID); // load a document
                    var documentCounters = session.CountersFor(document); // get the counters for this document

                    //list all counters currently attached to a certain object
                    Console.WriteLine("all counters for this document: ");
                    var documentCountersArray = documentCounters.GetAll().ToArray(); // gett all counters attached to this document
                    foreach (var counter in documentCountersArray)
                    {
                        Console.WriteLine("counter key: " + counter.Key + ", counter value: " + counter.Value);
                    }
                }

                Console.ReadKey();
            }
            */

            //playing with batch operations
            /*
            using (var session = docStore.OpenSession())
            {
                    //	GetCountersOperation
                    var getthem = new GetCountersOperation("SupportCalls/1-A", "c2");
                    var resgettem = docStore.Operations.Send(getthem);
                    Console.WriteLine("tararam2 results " + resgettem?.Counters[0]?.TotalValue);


                    // batch
                    var docsies =
                        new List<DocumentCountersOperation>()
                        {
                                new DocumentCountersOperation
                                {
                                    DocumentId = "SupportCalls/33-A",
                                    Operations = new List<CounterOperation>
                                        {
                                            new CounterOperation {CounterName = "c1", Type = CounterOperationType.Delete },
                                            new CounterOperation {CounterName = "c2", Type = CounterOperationType.Increment, Delta = -100 },
                                            new CounterOperation {CounterName = "c3", Type = CounterOperationType.Get },
                                        }
                                },
                                new DocumentCountersOperation
                                {
                                    DocumentId = "Users/3",
                                    Operations = new List<CounterOperation>
                                            {
                                                new CounterOperation {CounterName = "u1", Type = CounterOperationType.Delete },
                                                new CounterOperation {CounterName = "u2", Type = CounterOperationType.Increment, Delta = 0100 },
                                                new CounterOperation {CounterName = "u3", Type = CounterOperationType.Get },
                                            }
                                }
                        };
                  
                    var counterBatchObject = new CounterBatch();
                    counterBatchObject.Documents = docsies;
                    counterBatchObject.ReplyWithAllNodesValues = true;

                    var counterBatchOperationObject = new CounterBatchOperation(counterBatchObject);

                    var res = docStore.Operations.Send(counterBatchOperationObject);
                    foreach (var detail in res.Counters)
                    {
                        Console.WriteLine($"name = " +
                      $"{detail.CounterName}, value = {detail.TotalValue}");

                        Console.WriteLine("values per node: ");
                        foreach (var nodeValue in detail.CounterValues)
                        {
                            Console.WriteLine($"{nodeValue.Key[0]}:{nodeValue.Value}");
                        }
                        

                    }
                    

                    Console.ReadKey();
            }
              */

        }

        public class SupportCall
        {
            public string Id { get; set; }
            public string CustomerId { get; set; }
            public DateTime Started { get; set; }
            public DateTime? Ended { get; set; }
            public string Issue { get; set; }
            public int Votes { get; set; }
        }
    }
}
