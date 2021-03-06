package net.ravendb.ClientApi.Session.Querying;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.documents.IDocumentStore;
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.queries.Query;
import net.ravendb.client.documents.queries.QueryData;
import net.ravendb.client.documents.session.IDocumentSession;

import java.util.List;

public class HowToProjectQueryResults {

    private static class Company {

    }

    private static class Order {

    }

    private static class Employee {

    }
    private static class Total {

    }

    private static class NameCityAndCountry {
    }

    private static class ShipToAndProducts {

    }

    private static class Product {

    }

    private static class OrderProjection {

    }

    private static class EmployeeProjection {

    }

    private static class FullName {
        private String fullName;

        public String getFullName() {
            return fullName;
        }

        public void setFullName(String fullName) {
            this.fullName = fullName;
        }
    }

    public void examples() {
        try (IDocumentStore store = new DocumentStore()) {
            try (IDocumentSession session = store.openSession()) {
                //region projections_1
                // request name, city and country for all entities from 'Companies' collection
                QueryData queryData = new QueryData(
                    new String[] { "name", "address.city", "address.country"},
                    new String[] { "name", "city", "country"});
                List<NameCityAndCountry> results = session
                    .query(Company.class)
                    .selectFields(NameCityAndCountry.class, queryData)
                    .toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_2
                QueryData queryData = new QueryData(new String[]{ "shipTo", "lines[].productName" },
                    new String[]{"shipTo", "products"});

                List<ShipToAndProducts> results = session.query(Order.class)
                    .selectFields(ShipToAndProducts.class, queryData)
                    .toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_3
                List<FullName> results = session.advanced().rawQuery(FullName.class, "from Employees as e " +
                    "select {" +
                    "    fullName : e.firstName + \" \" + e.lastName " +
                    "}").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_4
                List<Total> results = session.advanced().rawQuery(Total.class, "from Orders as o " +
                    "select { " +
                    "    total : o.lines.reduce( " +
                    "        (acc , l) => acc += l.pricePerUnit * l.quantity, 0) " +
                    "}").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_5
                List<OrderProjection> results = session.advanced().rawQuery(OrderProjection.class, "from Orders as o " +
                    "load o.company as c " +
                    "select { " +
                    "    companyName: c.name," +
                    "    shippedAt: o.shippedAt" +
                    "}").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_6
                List<EmployeeProjection> results = session.advanced().rawQuery(EmployeeProjection.class, "from Employees as e " +
                    "select { " +
                    "    dayOfBirth : new Date(Date.parse(e.birthday)).getDate(), " +
                    "    monthOfBirth : new Date(Date.parse(e.birthday)).getMonth() + 1, " +
                    "    age : new Date().getFullYear() - new Date(Date.parse(e.birthday)).getFullYear() " +
                    "}").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_7
                List<EmployeeProjection> results = session.advanced().rawQuery(EmployeeProjection.class, "from Employees as e " +
                    "select { " +
                    "    date : new Date(Date.parse(e.birthday)), " +
                    "    name : e.firstName.substr(0,3) " +
                    "}").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_8
                List<ContactDetails> results = session.query(Company.class, Companies_ByContact.class)
                    .selectFields(ContactDetails.class)
                    .toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_10
                // query index 'Products_BySupplierName'
                // return documents from collection 'Products' that have a supplier 'Norske Meierier'
                // project them to 'Products'
                List<Product> results = session.query(Products_BySupplierName.Result.class, Products_BySupplierName.class)
                    .whereEquals("name", "Norske Meierier")
                    .ofType(Product.class)
                    .toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_12
                List<Employee> results = session.advanced().rawQuery(Employee.class, "declare function output(e) { " +
                    "    var format = function(p){ return p.FirstName + \" \" + p.LastName; }; " +
                    "    return { FullName : format(e) }; " +
                    "} " +
                    "from Employees as e select output(e)").toList();
                //endregion
            }

            try (IDocumentSession session = store.openSession()) {
                //region projections_13
                List<Employee> results = session.advanced().rawQuery(Employee.class, "from Employees as e " +
                    "select {" +
                    "     Name : e.FirstName, " +
                    "     Metadata : getMetadata(e)" +
                    "}").toList();
                //endregion
            }
        }
    }

    //region projections_9_0
    private class Companies_ByContact extends AbstractIndexCreationTask {
        public Companies_ByContact() {

            map = "from c in docs.Companies select new  { name = c.contact.name, phone = c.phone } ";

            storeAllFields(FieldStorage.YES); // name and phone fields can be retrieved directly from index
        }
    }
    //endregion

    //region projections_9_1
    public static class ContactDetails {
        private String name;
        private String phone;

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getPhone() {
            return phone;
        }

        public void setPhone(String phone) {
            this.phone = phone;
        }
    }
    //endregion

    //region projections_11
    public static class Products_BySupplierName extends AbstractIndexCreationTask {
        public static class Result {
        }

    }
    //endregion
}
