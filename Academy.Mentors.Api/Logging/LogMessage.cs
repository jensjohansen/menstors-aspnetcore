/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Academy.Mentors.Api.Logging 
{
	/// <summary>
	/// LogMessage Model
	/// </summary>
	[DataContract]
	public partial class LogMessage : IEquatable<LogMessage>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public LogMessage() { }

		/// <summary>
		/// Data Member LogMessages.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member LogMessages.LogMessageTypeId (bigint)
		/// </summary>
		[ForeignKey("LogMessageType")]
		[DataMember(Name = "logMessageTypeId")]
		[DisplayName("Log Message Type Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  LogMessageTypeId { get; set; }

		/// <summary>
		/// Data Member LogMessages.ApplicationName (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The ApplicationName value cannot exceed 70 characters. ")]
		[DataMember(Name = "applicationName")]
		[DisplayName("Application Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  ApplicationName { get; set; }

		/// <summary>
		/// Data Member LogMessages.ApplicationMethod (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The ApplicationMethod value cannot exceed 70 characters. ")]
		[DataMember(Name = "applicationMethod")]
		[DisplayName("Application Method")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  ApplicationMethod { get; set; }

		/// <summary>
		/// Data Member LogMessages.IpAddress (varchar)
		/// </summary>
		[StringLength(40, ErrorMessage = "The IpAddress value cannot exceed 40 characters. ")]
		[DataMember(Name = "ipAddress")]
		[DisplayName("Ip Address")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  IpAddress { get; set; }

		/// <summary>
		/// Data Member LogMessages.LoginToken (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The LoginToken value cannot exceed 255 characters. ")]
		[DataMember(Name = "loginToken")]
		[DisplayName("Login Token")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  LoginToken { get; set; }

		/// <summary>
		/// Data Member LogMessages.ShortMessage (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The ShortMessage value cannot exceed 255 characters. ")]
		[DataMember(Name = "shortMessage")]
		[DisplayName("Short Message")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  ShortMessage { get; set; }

		/// <summary>
		/// Data Member LogMessages.RequestHttpMethod (varchar)
		/// </summary>
		[StringLength(15, ErrorMessage = "The RequestHttpMethod value cannot exceed 15 characters. ")]
		[DataMember(Name = "requestHttpMethod")]
		[DisplayName("Request Http Method")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  RequestHttpMethod { get; set; }

		/// <summary>
		/// Data Member LogMessages.RequestUri (text)
		/// </summary>
		[DataMember(Name = "requestUri")]
		[DisplayName("Request Uri")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  RequestUri { get; set; }

		/// <summary>
		/// Data Member LogMessages.RequestParams (text)
		/// </summary>
		[DataMember(Name = "requestParams")]
		[DisplayName("Request Params")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  RequestParams { get; set; }

		/// <summary>
		/// Data Member LogMessages.RequestBody (text)
		/// </summary>
		[DataMember(Name = "requestBody")]
		[DisplayName("Request Body")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  RequestBody { get; set; }

		/// <summary>
		/// Data Member LogMessages.StatusCode (int)
		/// </summary>
		[DataMember(Name = "statusCode")]
		[DisplayName("Status Code")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public int? StatusCode { get; set; }

		/// <summary>
		/// Data Member LogMessages.ResponseContent (text)
		/// </summary>
		[DataMember(Name = "responseContent")]
		[DisplayName("Response Content")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  ResponseContent { get; set; }

		/// <summary>
		/// Data Member LogMessages.FullMessage (text)
		/// </summary>
		[DataMember(Name = "fullMessage")]
		[DisplayName("Full Message")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  FullMessage { get; set; }

		/// <summary>
		/// Data Member LogMessages.Exception (text)
		/// </summary>
		[DataMember(Name = "exception")]
		[DisplayName("Exception")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Exception { get; set; }

		/// <summary>
		/// Data Member LogMessages.Trace (text)
		/// </summary>
		[DataMember(Name = "trace")]
		[DisplayName("Trace")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Trace { get; set; }

		/// <summary>
		/// Data Member LogMessages.Logged (datetime)
		/// </summary>
		[DataMember(Name = "logged")]
		[DisplayName("Logged")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  Logged { get; set; }

		/// <summary>
		/// Virtual Parent Object: LogMessageType
		/// </summary>
		[ForeignKey("LogMessageTypeId")]
		[DataMember(Name = "logMessageType")]
		public virtual LogMessageType LogMessageType { get; private set; }

		/// <summary>
		/// Full Constructor for LogMessage Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="LogMessageTypeId">Log Message Type Id (bigint)</param>
		/// <param name="ApplicationName">Application Name (varchar)</param>
		/// <param name="ApplicationMethod">Application Method (varchar)</param>
		/// <param name="IpAddress">Ip Address (varchar)</param>
		/// <param name="LoginToken">Login Token (varchar)</param>
		/// <param name="ShortMessage">Short Message (varchar)</param>
		/// <param name="RequestHttpMethod">Request Http Method (varchar)</param>
		/// <param name="RequestUri">Request Uri (text)</param>
		/// <param name="RequestParams">Request Params (text)</param>
		/// <param name="RequestBody">Request Body (text)</param>
		/// <param name="StatusCode">Status Code (int)</param>
		/// <param name="ResponseContent">Response Content (text)</param>
		/// <param name="FullMessage">Full Message (text)</param>
		/// <param name="Exception">Exception (text)</param>
		/// <param name="Trace">Trace (text)</param>
		/// <param name="Logged">Logged (datetime)</param>
		public LogMessage(
			long? Id = default(long?),
			long? LogMessageTypeId = default(long?),
			String ApplicationName = default(String),
			String ApplicationMethod = default(String),
			String IpAddress = default(String),
			String LoginToken = default(String),
			String ShortMessage = default(String),
			String RequestHttpMethod = default(String),
			String RequestUri = default(String),
			String RequestParams = default(String),
			String RequestBody = default(String),
			int StatusCode = default(int),
			String ResponseContent = default(String),
			String FullMessage = default(String),
			String Exception = default(String),
			String Trace = default(String),
			DateTime? Logged = default(DateTime?)
			)
		{
			this.Id = Id;
			this.LogMessageTypeId = LogMessageTypeId;
			this.ApplicationName = ApplicationName;
			this.ApplicationMethod = ApplicationMethod;
			this.IpAddress = IpAddress;
			this.LoginToken = LoginToken;
			this.ShortMessage = ShortMessage;
			this.RequestHttpMethod = RequestHttpMethod;
			this.RequestUri = RequestUri;
			this.RequestParams = RequestParams;
			this.RequestBody = RequestBody;
			this.StatusCode = StatusCode;
			this.ResponseContent = ResponseContent;
			this.FullMessage = FullMessage;
			this.Exception = Exception;
			this.Trace = Trace;
			this.Logged = Logged;
		}

		/// <summary>
		/// Returns a string representation of the Object
		/// </summary>
		public override String ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class LogMessage {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  LogMessageTypeId: ").Append(LogMessageTypeId).Append("\n");
			sb.Append("  ApplicationName: ").Append(ApplicationName).Append("\n");
			sb.Append("  ApplicationMethod: ").Append(ApplicationMethod).Append("\n");
			sb.Append("  IpAddress: ").Append(IpAddress).Append("\n");
			sb.Append("  LoginToken: ").Append(LoginToken).Append("\n");
			sb.Append("  ShortMessage: ").Append(ShortMessage).Append("\n");
			sb.Append("  RequestHttpMethod: ").Append(RequestHttpMethod).Append("\n");
			sb.Append("  RequestUri: ").Append(RequestUri).Append("\n");
			sb.Append("  RequestParams: ").Append(RequestParams).Append("\n");
			sb.Append("  RequestBody: ").Append(RequestBody).Append("\n");
			sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
			sb.Append("  ResponseContent: ").Append(ResponseContent).Append("\n");
			sb.Append("  FullMessage: ").Append(FullMessage).Append("\n");
			sb.Append("  Exception: ").Append(Exception).Append("\n");
			sb.Append("  Trace: ").Append(Trace).Append("\n");
			sb.Append("  Logged: ").Append(Logged).Append("\n");
			sb.Append("}\n");
			return sb.ToString();
		}

		/// <summary>
		/// Returns a JSON representation of the Object
		/// </summary>
		public String ToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		/// <summary>
		/// Returns true if objects are Equal
		/// </summary>
		/// <param Name="obj">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null,obj)) return false;
			if (ReferenceEquals(this,obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((LogMessage)obj);
		}

		/// <summary>
		/// Returns true if LogMessage objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(LogMessage other)
		{
			if (ReferenceEquals(null,other)) return false;
			if (ReferenceEquals(this,other)) return true;
			return
				(
				this.Id == other.Id ||
				this.Id != null &&
				this.Id.Equals(other.Id)
				) &&
				(
				this.LogMessageTypeId == other.LogMessageTypeId ||
				this.LogMessageTypeId != null &&
				this.LogMessageTypeId.Equals(other.LogMessageTypeId)
				) &&
				(
				this.ApplicationName == other.ApplicationName ||
				this.ApplicationName != null &&
				this.ApplicationName.Equals(other.ApplicationName)
				) &&
				(
				this.ApplicationMethod == other.ApplicationMethod ||
				this.ApplicationMethod != null &&
				this.ApplicationMethod.Equals(other.ApplicationMethod)
				) &&
				(
				this.IpAddress == other.IpAddress ||
				this.IpAddress != null &&
				this.IpAddress.Equals(other.IpAddress)
				) &&
				(
				this.LoginToken == other.LoginToken ||
				this.LoginToken != null &&
				this.LoginToken.Equals(other.LoginToken)
				) &&
				(
				this.ShortMessage == other.ShortMessage ||
				this.ShortMessage != null &&
				this.ShortMessage.Equals(other.ShortMessage)
				) &&
				(
				this.RequestHttpMethod == other.RequestHttpMethod ||
				this.RequestHttpMethod != null &&
				this.RequestHttpMethod.Equals(other.RequestHttpMethod)
				) &&
				(
				this.RequestUri == other.RequestUri ||
				this.RequestUri != null &&
				this.RequestUri.Equals(other.RequestUri)
				) &&
				(
				this.RequestParams == other.RequestParams ||
				this.RequestParams != null &&
				this.RequestParams.Equals(other.RequestParams)
				) &&
				(
				this.RequestBody == other.RequestBody ||
				this.RequestBody != null &&
				this.RequestBody.Equals(other.RequestBody)
				) &&
				(
				this.StatusCode == other.StatusCode ||
				this.StatusCode != null &&
				this.StatusCode.Equals(other.StatusCode)
				) &&
				(
				this.ResponseContent == other.ResponseContent ||
				this.ResponseContent != null &&
				this.ResponseContent.Equals(other.ResponseContent)
				) &&
				(
				this.FullMessage == other.FullMessage ||
				this.FullMessage != null &&
				this.FullMessage.Equals(other.FullMessage)
				) &&
				(
				this.Exception == other.Exception ||
				this.Exception != null &&
				this.Exception.Equals(other.Exception)
				) &&
				(
				this.Trace == other.Trace ||
				this.Trace != null &&
				this.Trace.Equals(other.Trace)
				) &&
				(
				this.Logged == other.Logged ||
				this.Logged != null &&
				this.Logged.Equals(other.Logged)
				) ;
		}

		/// <summary>
		/// Returns the hashcode of this Object
		/// </summary>
		/// <returns>Hash code (int)</returns>
		public override int GetHashCode()
		{
			// Credit: http://stackoverflow.com/a/263416/677735
			unchecked // Overflow is fine, just wrap
			{
				int hash = 41;
				// Suitable nullity checks etc, of course :)
				if (Id!= null) hash = hash * 59 + Id.GetHashCode();
				if (LogMessageTypeId!= null) hash = hash * 59 + LogMessageTypeId.GetHashCode();
				if (ApplicationName!= null) hash = hash * 59 + ApplicationName.GetHashCode();
				if (ApplicationMethod!= null) hash = hash * 59 + ApplicationMethod.GetHashCode();
				if (IpAddress!= null) hash = hash * 59 + IpAddress.GetHashCode();
				if (LoginToken!= null) hash = hash * 59 + LoginToken.GetHashCode();
				if (ShortMessage!= null) hash = hash * 59 + ShortMessage.GetHashCode();
				if (RequestHttpMethod!= null) hash = hash * 59 + RequestHttpMethod.GetHashCode();
				if (RequestUri!= null) hash = hash * 59 + RequestUri.GetHashCode();
				if (RequestParams!= null) hash = hash * 59 + RequestParams.GetHashCode();
				if (RequestBody!= null) hash = hash * 59 + RequestBody.GetHashCode();
				if (StatusCode!= null) hash = hash * 59 + StatusCode.GetHashCode();
				if (ResponseContent!= null) hash = hash * 59 + ResponseContent.GetHashCode();
				if (FullMessage!= null) hash = hash * 59 + FullMessage.GetHashCode();
				if (Exception!= null) hash = hash * 59 + Exception.GetHashCode();
				if (Trace!= null) hash = hash * 59 + Trace.GetHashCode();
				if (Logged!= null) hash = hash * 59 + Logged.GetHashCode();
				return hash;
			}
		}

		#region Operators

		/// <summary>
		/// Equals Operator (==)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator ==(LogMessage left, LogMessage right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(LogMessage left, LogMessage right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class LogMessage
} // End of Namespace Academy.Mentors.Api.Logging 
