﻿# Get Documents by ID

---

{NOTE: }  

* Use this endpoint with the **`GET`** method to retrieve documents from the database according to their document IDs:  
`<server URL>/databases/<database name>/docs?id=<document ID>`  

* Query parameters can be used to include [related documents](../../../client-api/how-to/handle-document-relationships#includes) and 
[counters](../../../document-extensions/counters/overview).  

* In this page:  
  * [Basic Example](../../../client-api/rest-api/document-commands/get-documents-by-id#basic-example)  
  * [Request Format](../../../client-api/rest-api/document-commands/get-documents-by-id#request-format)  
  * [Response Format](../../../client-api/rest-api/document-commands/get-documents-by-id#response-format)  
  * [More Examples](../../../client-api/rest-api/document-commands/get-documents-by-id#more-examples)  
      * [Get Multiple Documents](../../../client-api/rest-api/document-commands/get-documents-by-id#get-multiple-documents)  
      * [Get Related Documents](../../../client-api/rest-api/document-commands/get-documents-by-id#get-related-documents)  
      * [Get Document Metadata Only](../../../client-api/rest-api/document-commands/get-documents-by-id#get-document-metadata-only)  
      * [Get Document Counters](../../../client-api/rest-api/document-commands/get-documents-by-id#get-document-counters)  
{NOTE/}  

---

{PANEL: Basic Example}

This is a cURL request to retrieve one document named "products/48-A" from a database named "Example" on our 
[playground server](http://live-test.ravendb.net):  

{CODE-BLOCK: bash}
curl -X GET "http://live-test.ravendb.net/databases/Example/docs?id=products/48-A"
{CODE-BLOCK/}

Response:  

{CODE-BLOCK: http}
HTTP/1.1 200 OK
Server: nginx
Date: Tue, 10 Sep 2019 10:33:04 GMT
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
Connection: keep-alive
Content-Encoding: gzip
ETag: "A:285-k50KTOC5G0mfVXKjomTNFQ"
Vary: Accept-Encoding
Raven-Server-Version: 4.2.4.42

{
    "Results": [
        {
            "Name": "Chocolade",
            "Supplier": "suppliers/22-A",
            "Category": "categories/3-A",
            "QuantityPerUnit": "10 pkgs.",
            "PricePerUnit": 12.7500,
            "UnitsInStock": 22,
            "UnitsOnOrder": 15,
            "Discontinued": false,
            "ReorderLevel": 25,
            "@metadata": {
                "@collection": "Products",
                "@change-vector": "A:285-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "products/48-A",
                "@last-modified": "2018-07-27T12:11:53.0300420Z"
            }
        }
    ],
    "Includes": {}
}
{CODE-BLOCK/}

{PANEL/}

{PANEL: Request Format}

This is the general format of a cURL request that uses all parameters:  

{CODE-BLOCK: batch}
curl -X GET "<server URL>/databases/<database name>/docs?
            id=<document ID>
            &include=<path>
            &counter=<counter name>
            &metadataOnly=<boolean>"
--header "If-None-Match:<hash>"
{CODE-BLOCK/}
Linebreaks are added for clarity.  
<br/>
####Query String Parameters

| Parameter | Description | Required / # |
| - | - | - |
| **id** | ID of a document to retrieve.<br/>If no IDs are specified, all the documents in the database are retrieved in descending order of their [change vectors](../../../server/clustering/replication/change-vector). | Yes;<br/>Can be used more than once |
| **include** | Path to a field containing the ID of another, 'related' document. [See: How to Handle Document Relationships](../../../client-api/how-to/handle-document-relationships#includes). | No;<br/>Can be used more than once |
| **counter** | Name of a [counter](../../../document-extensions/counters/overview) to retrieve. Set this parameter to `@all_counters` to retrieve all counters of the specified documents. Counters of _included_ documents, however, will not be retrieved. | No;<br/>Can be used more than once |
| **metadataOnly** | Set this parameter to `true` to retrieve only the metadata of each document. This does not apply to included documents which are retrieved with their complete contents. | No;<br/>Used once |

####Headers

| Header | Description | Required |
| - | - | - |
| **If-None-Match** | This header takes a hash representing the previous results of an **identical** request. The hash is found in the response header `ETag`. If the results were not modified since the previous request, the server responds with http status code `304` and the requested documents are retrieved from a local cache rather than over the network. | No |

{PANEL/}

{PANEL: Response Format}

####Http Status Codes

| Code | Description |
| - | - |
| `200` | Results are successfully retrieved. If a requested document could not be found, the result returned is `null`. |
| `304` | In response to an `If-None-Match` check: none of the requested documents were modified since they were last loaded, so they were not retrieved from the server. |
| `404` | No document with the specified ID was found. This code is only sent when _one_ document was requested. Otherwise, see status code `200`. |

####Headers

| Header | Description |
| - | - |
| **Content-Type** | MIME media type and character encoding. This should always be: `application/json; charset=utf-8`. |
| **ETag** | Hash representing the state of these results. If another, **identical** request is made, this hash can be sent in the `If-None-Match` header to check whether the retrieved documents have been modified since the last response. If none were modified, they are not retrieved. |
| **Raven-Server-Version** | Version of RavenDB that the responding server is running. |

####Body

A retrieved document is identical in contents and format to the document stored on the server (unless the `metadataOnly` 
parameter is set to `true`).  

This is the general JSON format of the response body:  

{CODE-BLOCK: JavaScript}
{
    "Results": [ 
        { 
            <document contents>
        },
        { },
        ...
    ],
    "Includes":
        "<document ID>": {
            <document contents>
        },
        "<document ID>": { },
        ...
    }
    "CounterIncludes": {
        "<document ID>": [
            {
                "DocumentId": "<document ID>",
                "CounterName": "<counter name>",
                "TotalValue": <integer>
            },
            { },
            ...
        ],
        "<document ID>": [ ],
        ...
    }
}
{CODE-BLOCK/}
Linebreaks are added for clarity.  

{PANEL/}

{PANEL: More Examples}

[About Northwind](../../../start/about-examples), the database used in our examples.

In this section:  

* [Get Multiple Documents](../../../client-api/rest-api/document-commands/get-documents-by-id#get-multiple-documents)  
* [Get Related Documents](../../../client-api/rest-api/document-commands/get-documents-by-id#get-related-documents)  
* [Get Document Metadata Only](../../../client-api/rest-api/document-commands/get-documents-by-id#get-document-metadata-only)  
* [Get Document Counters](../../../client-api/rest-api/document-commands/get-documents-by-id#get-document-counters)  

---

### Get Multiple Documents

Example cURL request:  

{CODE-BLOCK: bash}
curl -X GET "http://live-test.ravendb.net/databases/Example/docs?
            id=shippers/1-A
            &id=shippers/2-A"
{CODE-BLOCK/}
Linebreaks are added for clarity.  

Response:  

{CODE-BLOCK: Http}
HTTP/1.1 200 OK
Server: nginx
Date: Thu, 12 Sep 2019 09:23:49 GMT
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
Connection: keep-alive
Content-Encoding: gzip
ETag: "Hash-auWLG9xq3imTfRdJvlKIL32LhEM0IwJ20eiibWse0X8="
Vary: Accept-Encoding
Raven-Server-Version: 4.2.4.42

{
    "Results": [
        {
            "Name": "Speedy Express",
            "Phone": "(503) 555-9831",
            "@metadata": {
                "@collection": "Shippers",
                "@change-vector": "A:349-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "shippers/1-A",
                "@last-modified": "2018-07-27T12:11:53.0317375Z"
            }
        },
        {
            "Name": "United Package",
            "Phone": "(503) 555-3199",
            "@metadata": {
                "@collection": "Shippers",
                "@change-vector": "A:351-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "shippers/2-A",
                "@last-modified": "2018-07-27T12:11:53.0317596Z"
            }
        }
    ],
    "Includes": {}
}
{CODE-BLOCK/}

---

### Get Related Documents

Example cURL request:  

{CODE-BLOCK: bash}
curl -X GET "http://live-test.ravendb.net/databases/Demo/docs?
            id=products/48-A
            &include=Supplier
            &include=Category"
{CODE-BLOCK/}
Linebreaks are added for clarity.  

Response:  

{CODE-BLOCK: http}
HTTP/1.1 200 OK
Server: nginx
Date: Tue, 10 Sep 2019 10:40:27 GMT
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
Connection: keep-alive
Content-Encoding: gzip
ETag: "Hash-9oK1ZcWmNa9SD9hP8m0vT355ztQuFnF/vKD5ILyI/KY="
Vary: Accept-Encoding
Raven-Server-Version: 4.2.4.42

{
    "Results": [
        {
            "Name": "Chocolade",
            "Supplier": "suppliers/22-A",
            "Category": "categories/3-A",
            "QuantityPerUnit": "10 pkgs.",
            "PricePerUnit": 12.7500,
            "UnitsInStock": 22,
            "UnitsOnOrder": 15,
            "Discontinued": false,
            "ReorderLevel": 25,
            "@metadata": {
                "@collection": "Products",
                "@change-vector": "A:285-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "products/48-A",
                "@last-modified": "2018-07-27T12:11:53.0300420Z"
            }
        }
    ],
    "Includes": {
        "suppliers/22-A": {
            "Contact": {
                "Name": "Dirk Luchte",
                "Title": "Accounting Manager"
            },
            "Name": "Zaanse Snoepfabriek",
            "Address": {
                "Line1": "Verkoop Rijnweg 22",
                "Line2": null,
                "City": "Zaandam",
                "Region": null,
                "PostalCode": "9999 ZZ",
                "Country": "Netherlands",
                "Location": null
            },
            "Phone": "(12345) 1212",
            "Fax": "(12345) 1210",
            "HomePage": null,
            "@metadata": {
                "@collection": "Suppliers",
                "@change-vector": "A:399-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "suppliers/22-A",
                "@last-modified": "2018-07-27T12:11:53.0335729Z"
            }
        },
        "categories/3-A": {
            "Name": "Confections",
            "Description": "Desserts, candies, and sweet breads",
            "@metadata": {
                "@attachments": [
                    {
                        "Name": "image.jpg",
                        "Hash": "1QxSMa3tBr+y8wQYNre7E9UJFFVTNWGjVoC+IC+gSSs=",
                        "ContentType": "image/jpeg",
                        "Size": 47955
                    }
                ],
                "@collection": "Categories",
                "@change-vector": "A:2092-k50KTOC5G0mfVXKjomTNFQ",
                "@flags": "HasAttachments",
                "@id": "categories/3-A",
                "@last-modified": "2018-07-27T12:16:44.1738714Z"
            }
        }
    }
}
{CODE-BLOCK/}

---

### Get Document Metadata Only

Example cURL request:

{CODE-BLOCK: bash}
curl -X GET "http://live-test.ravendb.net/databases/Example/docs?
            id=orders/19-A
            &metadataOnly=true"
{CODE-BLOCK/}
Linebreaks are added for clarity.  

Response:

{CODE-BLOCK: http}
HTTP/1.1 200 OK
Server: nginx
Date: Tue, 10 Sep 2019 10:52:28 GMT
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
Connection: keep-alive
Content-Encoding: gzip
ETag: "A:453-k50KTOC5G0mfVXKjomTNFQ"
Vary: Accept-Encoding
Raven-Server-Version: 4.2.4.42

{
    "Results": [
        {
            "@metadata": {
                "@collection": "Orders",
                "@change-vector": "A:453-k50KTOC5G0mfVXKjomTNFQ",
                "@flags": "HasRevisions",
                "@id": "orders/19-A",
                "@last-modified": "2018-07-27T12:11:53.0476121Z"
            }
        }
    ],
    "Includes": {}
}
{CODE-BLOCK/}

---

### Get Document Counters

Example cURL request:

{CODE-BLOCK: bash}
curl -X GET "http://live-test.ravendb.net/databases/Example/docs?
            id=products/48-A
            &counter=MoLtUaE"
{CODE-BLOCK/}
Linebreaks are added for clarity.  

Response:

{CODE-BLOCK: http}
HTTP/1.1 200 OK
Server: nginx
Date: Tue, 10 Sep 2019 12:26:04 GMT
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
Connection: keep-alive
Content-Encoding: gzip
ETag: "A:5957-k50KTOC5G0mfVXKjomTNFQ"
Vary: Accept-Encoding
Raven-Server-Version: 4.2.4.42

{
    "Results": [
        {
            "Name": "Chocolade",
            "Supplier": "suppliers/22-A",
            "Category": "categories/3-A",
            "QuantityPerUnit": "10 pkgs.",
            "PricePerUnit": 12.7500,
            "UnitsInStock": 22,
            "UnitsOnOrder": 15,
            "Discontinued": false,
            "ReorderLevel": 25,
            "@metadata": {
                "@collection": "Products",
                "@counters": [
                    "#OfCounters",
                    "MoLtUaE"
                ],
                "@change-vector": "A:285-k50KTOC5G0mfVXKjomTNFQ",
                "@id": "products/48-A",
                "@flags": "HasRevisions, HasCounters",
                "@last-modified": "2019-09-10T12:25:44.1759382Z"
            }
        }
    ],
    "Includes": {},
    "CounterIncludes": {
        "orders/19-A": [
            {
                "DocumentId": "orders/19-A",
                "CounterName": "MoLtUaE",
                "TotalValue": 42
            }
        ]
    }
}
{CODE-BLOCK/}

(Note that the standard [Northwind data](../../../start/about-examples) does not contain any [counters](../../../document-extensions/counters/overview) 
when it is [generated in the studio](../../../studio/database/document-extensions/counters) - counters were added to "products/48-A" for this example)  

{PANEL/}

## Related Articles  

### Getting Started  

- [About Examples](../../../start/about-examples)  
<br/>
### Client API  

##### Commands

- [Documents: Get](../../../client-api/commands/documents/get)

##### REST API

- [Get All Documents](../../../client-api/rest-api/document-commands/get-all-documents)  
- [Get Documents by Prefix](../../../client-api/rest-api/document-commands/get-documents-by-prefix)  
- [Put a Document](../../../client-api/rest-api/document-commands/put-documents)  
- [Delete a Document](../../../client-api/rest-api/document-commands/delete-document)  
- [Batch Commands](../../../client-api/rest-api/document-commands/batch-commands)  
- [Counters: Overview](../../../client-api/session/counters/overview)  
- [How to Handle Document Relationships](../../../client-api/how-to/handle-document-relationships#includes)  
<br/>
### Server  

- [Change Vector](../../../server/clustering/replication/change-vector)  
