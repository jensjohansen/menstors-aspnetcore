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
	/// LogMessageType Model
	/// </summary>
	[DataContract]
	public partial class LogMessageType : IEquatable<LogMessageType>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public LogMessageType() { }

		/// <summary>
		/// Data Member LogMessageTypes.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member LogMessageTypes.Name (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The Name value cannot exceed 70 characters. ")]
		[DataMember(Name = "name")]
		[DisplayName("Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Name { get; set; }

		/// <summary>
		/// Data Member LogMessageTypes.Description (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Description value cannot exceed 255 characters. ")]
		[DataMember(Name = "description")]
		[DisplayName("Description")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Description { get; set; }

		/// <summary>
		/// Data Member LogMessageTypes.RetentionDays (int)
		/// </summary>
		[DataMember(Name = "retentionDays")]
		[DisplayName("Retention Days")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public int? RetentionDays { get; set; }

		/// <summary>
		/// Virtual Child Collection: LogMessages
		/// </summary>
		[DataMember(Name = "logMessages")]
		public virtual ICollection<LogMessage> LogMessages { get; private set; }

		/// <summary>
		/// Full Constructor for LogMessageType Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="Name">Name (varchar)</param>
		/// <param name="Description">Description (varchar)</param>
		/// <param name="RetentionDays">Retention Days (int)</param>
		public LogMessageType(
			long? Id = default(long?),
			String Name = default(String),
			String Description = default(String),
			int RetentionDays = default(int)
			)
		{
			this.Id = Id;
			this.Name = Name;
			this.Description = Description;
			this.RetentionDays = RetentionDays;
		}

		/// <summary>
		/// Returns a string representation of the Object
		/// </summary>
		public override String ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class LogMessageType {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  Name: ").Append(Name).Append("\n");
			sb.Append("  Description: ").Append(Description).Append("\n");
			sb.Append("  RetentionDays: ").Append(RetentionDays).Append("\n");
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
			return Equals((LogMessageType)obj);
		}

		/// <summary>
		/// Returns true if LogMessageType objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(LogMessageType other)
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
				this.Name == other.Name ||
				this.Name != null &&
				this.Name.Equals(other.Name)
				) &&
				(
				this.Description == other.Description ||
				this.Description != null &&
				this.Description.Equals(other.Description)
				) &&
				(
				this.RetentionDays == other.RetentionDays ||
				this.RetentionDays != null &&
				this.RetentionDays.Equals(other.RetentionDays)
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
				if (Name!= null) hash = hash * 59 + Name.GetHashCode();
				if (Description!= null) hash = hash * 59 + Description.GetHashCode();
				if (RetentionDays!= null) hash = hash * 59 + RetentionDays.GetHashCode();
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
		public static bool operator ==(LogMessageType left, LogMessageType right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(LogMessageType left, LogMessageType right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class LogMessageType
} // End of Namespace Academy.Mentors.Api.Logging 
