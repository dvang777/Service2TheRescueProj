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

namespace Service2TheRescue.Controllers

{
    public class UserInfosController : Controller
    {
        // Note: the SOQL Field list, and Binding Property list have subtle differences as custom properties may be mapped with the JsonProperty attribute to remove __c
        const string _UserInfosPostBinding = "Id,FirstName,LastName,CompanyName,Title,Street,City,State,PostalCode,Phone,IsActive,AboutMe";
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            IEnumerable<Models.Salesforce.UserInfo> selectedUserInfos = Enumerable.Empty<Models.Salesforce.UserInfo>();
            try
            {
                selectedUserInfos = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Models.Salesforce.UserInfo> userInfos =
                            await client.QueryAsync<Models.Salesforce.UserInfo>("SELECT Id,FirstName,LastName,CompanyName,Title,Street,City,State,PostalCode,Phone,IsActive,AboutMe From UserInfo");
                        return userInfos.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce UserInfos";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedUserInfos);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Models.Salesforce.UserInfo> selectedUserInfos = Enumerable.Empty<Models.Salesforce.UserInfo>();
            try
            {
                selectedUserInfos = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Models.Salesforce.UserInfo> userInfos =
                            await client.QueryAsync<Models.Salesforce.UserInfo>("SELECT Id,FirstName,LastName,CompanyName,Title,Street,City,State,PostalCode,Phone,IsActive,AboutMe From UserInfo Where Id = '" + id + "'");
                        return userInfos.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce UserInfos Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedUserInfos.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Models.Salesforce.UserInfo> selectedUserInfos = Enumerable.Empty<Models.Salesforce.UserInfo>();
            try
            {
                selectedUserInfos = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Models.Salesforce.UserInfo> userInfos =
                            await client.QueryAsync<Models.Salesforce.UserInfo>("SELECT Id,FirstName,LastName,CompanyName,Title,Street,City,State,PostalCode,Phone,IsActive,AboutMe From UserInfo Where Id= '" + id + "'");
                        return userInfos.Records;
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
            return View(selectedUserInfos.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _UserInfosPostBinding)] Models.Salesforce.UserInfo userInfo)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("UserInfo", userInfo.Id, userInfo);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce UserInfo";
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
                return View(userInfo);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Models.Salesforce.UserInfo> selectedUserInfos = Enumerable.Empty<Models.Salesforce.UserInfo>();
            try
            {
                selectedUserInfos = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Contact
                    QueryResult<Models.Salesforce.UserInfo> userinfos =
                        await client.QueryAsync<Models.Salesforce.UserInfo>(string.Format("SELECT Id,FirstName,LastName,CompanyName,Title,Street,City,State,PostalCode,Phone,IsActive,AboutMe From UserInfo Where Id='{0}'", id));
                    return userinfos.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce UserInfo";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedUserInfos.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedUserInfos.FirstOrDefault());
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
                        success = await client.DeleteAsync("UserInfo", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce UserInfo";
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
        public async Task<ActionResult> Create([Bind(Include = _UserInfosPostBinding)] Models.Salesforce.UserInfo userinfo)
        {
            String id = String.Empty;
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        return await client.CreateAsync("UserInfo", userinfo);
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce UserInfo";
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
                return View(userinfo);
            }
        }
    }
}