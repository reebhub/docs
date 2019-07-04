#Cloud: Scaling
---

{NOTE: }

* RavenDB instances in the **development** and **production** tiers can be **upscaled** or **downscaled** into an 
instance of the *same tier.*  
* An instance can't be converted to a different tier, but databases can be [migrated](cloud-migration) between any 
two RavenDB instances.  
{NOTE/}

---

{PANEL: }

To scale a RavenDB cloud instance, go to the [Products tab](../cloud/cloud-control-panel#the-products-tab) 
in the cloud Control Panel. Click on the `Manage` button for the product you want to scale. In the `General` 
tab you will see buttons to `change instance type` and `change storage`:  

!["Scaling Buttons"](images/scaling-001-buttons.png "Scaling Buttons")  
  
Clicking on the `change instance type` button will open a menu with options for `CPU Priority` and `Cluster Size`:  

!["Scaling Instance Type"](images/scaling-002-instance.png "Scaling Instance Type")  
  
Clicking on the `change storage` button will open a menu with options for the instance's storage capacity:  

!["Scaling Storage"](images/scaling-003-storage.png "Scaling Storage")  
  
There are two types of storage: standard and premium. Selecting premium storage gives you the additional option 
of selecting the amount of IOPS (in/out operations per second) that the instance can handle.  

{NOTE: }

When you scale a development tier instance, it will go down temporarily while the configuration is updated. 
Production tier instances are scaled in a **rolling update**, one cluster node at a time, so your cluster will 
not experience any downtime.  

{NOTE/}

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
