#Cloud: Migration
---

{NOTE: }

* Data can be migrated between any two instances of RavenDB, whether they are a local instance or a cloud instance.
* In this page
  * [Import From Live RavenDB instance](cloud-migration#import-from-live-ravendb-instance)
  * [Import From File](cloud-migration#import-from-file)

{NOTE/}

---

{PANEL: Import From Live RavenDB Instance}

One good way to migrate your database is with the 
[import data from RavenDB](../studio/database/tasks/import-data/import-from-ravendb) operation:  

* Open the management studios for your source and destination servers. The studio is available at a RavenDB server's 
URL.  
* In each studio, click on `Manage certificates`:  
  
!["Manage Certificates"](images\migration-001-manage-certificates.png "Manage Certificates")  
  
* Export the `Cluster certificate` from the *destination* server:  
  
!["Cluster Certificate"](images\migration-002-cluster-certificate.png "Cluster Certificate")  
  
* Import that certificate as a `Client certificate` in the *source* server:  
  
!["Client Certificate"](images\migration-003-client-certificate.png "Client Certificate")  
  
* In the destination server, select an empty database in the destination server, go to `Settings`>`Import Data`, 
and click on the `From RavenDB` tab.  
* Enter the URL of the source server. Options will appear to fine-tune which data is migrated. When you're done, 
click `Migrate Database`:  
  
!["Import Options"](images\migration-004-options.png "Import Options")  

{PANEL/}

{PANEL: Import From File}
  
Another option is to [export a database](../studio/database/tasks/export-database) from the source server in the 
form of a `.ravenDBDump` file and upload it with the 
[import data from file](../studio/database/tasks/import-data/import-data-file) operation:

* In the source server, select a database to export and go to `Settings`>`Export Database`. After fine-tuning 
which data to export, click `Export Database`.  
* In the destination server, go to `Settings`>`Import Data`, and click on the `From file (.ravendbdump)` tab. 
Select the file and click `Import Database`.  

{PANEL/}

##Related Articles

###Cloud

- [Overview](cloud-overview)
- [Control Panel](cloud-control-panel)
- [Tiers and Instances](cloud-instances)
- [Pricing, Payment and Billing](cloud-pricing-payment-billing)
- [Backup](cloud-backup)
- [Migration](cloud-migration)
- [Scaling](cloud-scaling)
- [Security](cloud-security)

###Studio

- [Import Data From RavenDB](../studio/database/tasks/import-data/import-from-ravendb)
- [Export Database](../studio/database/tasks/export-database)
- [Import Data From File](../studio/database/tasks/import-data/import-data-file)
