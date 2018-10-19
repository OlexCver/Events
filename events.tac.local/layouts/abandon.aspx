<%@ Page language="c#" AutoEventWireup="false"  %>
<%@ OutputCache Location="None" VaryByParam="none" %>
<%
Sitecore.Analytics.Tracker.Current.CurrentPage.Cancel();
System.Web.HttpContext.Current.Session.Abandon();
%>
