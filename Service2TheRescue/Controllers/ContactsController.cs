using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Service2TheRescue.Models.Salesforce;
using Service2TheRescue.Salesforce;
using Salesforce.Force;

namespace Service2TheResucue.Controllers
{
    public class ContactsController : Controller
    {
        // Note: the SOQL Field list, and Binding Property list have subtle differences as custom properties may be mapped with the JsonProperty attribute to remove __c
        const string _ContactsPostBinding = "Id,FirstName,LastName,MailingStreet,MailingCity,MailingState,MailingPostalCode,MailingCountry,Phone,Email,Title,Description,AssistantName";
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT Id, FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }
        public async Task<ActionResult> UserView()
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }

        public async Task<ActionResult> ElectricianView()
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact WHERE Title LIKE '%Electrician%' ");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }
        public async Task<ActionResult> PlumberView()
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact WHERE Title LIKE '%Plumber%'");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }
        [HttpGet]
        public async Task<ActionResult> MyAction(string search)
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString(); var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            //do whatever you need with the parameter, 
            //like using it as parameter in Linq to Entities or Linq to Sql, etc. 
            //Suppose your search result will be put in variable "result".
            var result = await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact");
            ViewData.Model = result;
            return View();
        }


        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingStreet, MailingCity, MailingState, MailingPostalCode, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact Where Id = '" + id + "'");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Contacts Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT FirstName, LastName, MailingStreet, MailingCity, MailingState, MailingPostalCode, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact Where Id= '" + id + "'");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _ContactsPostBinding)] Contact contact)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Contact", contact.Id, contact);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Contact";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success.Success == "true")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(contact);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Contact
                    QueryResult<Contact> contacts =
                        await client.QueryAsync<Contact>(string.Format("SELECT FirstName, LastName, MailingCity, MailingState, MailingCountry, Phone, Email, Title, Description, AssistantName From Contact Where Id='{0}'", id));
                    return contacts.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedContacts.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedContacts.FirstOrDefault());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            bool success = false;
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.DeleteAsync("Contact", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = _ContactsPostBinding)] Contact contact)
        {
            String id = String.Empty;
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        return await client.CreateAsync("Contact", contact);
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Contact";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (this.ViewBag.ErrorMessage == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(contact);
            }
        }
    }
}