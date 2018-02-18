/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:43 PM */


using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Academy.Mentors.Models
{
	/// <summary>
	/// Contributor Model
	/// </summary>
	[DataContract]
	public partial class Contributor : IEquatable<Contributor>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public Contributor() { }

		/// <summary>
		/// Data Member Contributors.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member Contributors.Name (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The Name value cannot exceed 70 characters. ")]
		[DataMember(Name = "name")]
		[DisplayName("Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Name { get; set; }

		/// <summary>
		/// Data Member Contributors.Description (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Description value cannot exceed 255 characters. ")]
		[DataMember(Name = "description")]
		[DisplayName("Description")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Description { get; set; }

		/// <summary>
		/// Data Member Contributors.Degree (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The Degree value cannot exceed 70 characters. ")]
		[DataMember(Name = "degree")]
		[DisplayName("Degree")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Degree { get; set; }

		/// <summary>
		/// Data Member Contributors.AlmaMater (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The AlmaMater value cannot exceed 70 characters. ")]
		[DataMember(Name = "almaMater")]
		[DisplayName("Alma Mater")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  AlmaMater { get; set; }

		/// <summary>
		/// Data Member Contributors.Email (varchar)
		/// </summary>
		[StringLength(127, ErrorMessage = "The Email value cannot exceed 127 characters. ")]
		[DataMember(Name = "email")]
		[DisplayName("Email")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Email { get; set; }

		/// <summary>
		/// Data Member Contributors.Evaluations (int)
		/// </summary>
		[DataMember(Name = "evaluations")]
		[DisplayName("Evaluations")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public int? Evaluations { get; set; }

		/// <summary>
		/// Data Member Contributors.Password (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Password value cannot exceed 255 characters. ")]
		[DataMember(Name = "password")]
		[DisplayName("Password")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Password { get; set; }

		/// <summary>
		/// Data Member Contributors.Comments (text)
		/// </summary>
		[DataMember(Name = "comments")]
		[DisplayName("Comments")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Comments { get; set; }

		/// <summary>
		/// Data Member Contributors.AuditEntered (datetime)
		/// </summary>
		[DataMember(Name = "auditEntered")]
		[DisplayName("Audit Entered")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditEntered { get; set; }

		/// <summary>
		/// Data Member Contributors.AuditEnteredBy (bigint)
		/// </summary>
		[ForeignKey("AuditEnteredBy")]
		[DataMember(Name = "auditEnteredBy")]
		[DisplayName("Audit Entered By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditEnteredBy { get; set; }

		/// <summary>
		/// Data Member Contributors.AuditUpdated (datetime)
		/// </summary>
		[DataMember(Name = "auditUpdated")]
		[DisplayName("Audit Updated")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditUpdated { get; set; }

		/// <summary>
		/// Data Member Contributors.AuditUpdatedBy (bigint)
		/// </summary>
		[ForeignKey("AuditUpdatedBy")]
		[DataMember(Name = "auditUpdatedBy")]
		[DisplayName("Audit Updated By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditUpdatedBy { get; set; }

		/// <summary>
		/// Virtual Child Collection: Papers
		/// </summary>
		[DataMember(Name = "papers")]
		public virtual ICollection<Paper> Papers { get; private set; }

		/// <summary>
		/// Virtual Child Collection: PaperVersions
		/// </summary>
		[DataMember(Name = "paperVersions")]
		public virtual ICollection<PaperVersion> PaperVersions { get; private set; }

		/// <summary>
		/// Virtual Child Collection: Reviews
		/// </summary>
		[DataMember(Name = "reviews")]
		public virtual ICollection<Review> Reviews { get; private set; }

		/// <summary>
		/// Full Constructor for Contributor Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="Name">Name (varchar)</param>
		/// <param name="Description">Description (varchar)</param>
		/// <param name="Degree">Degree (varchar)</param>
		/// <param name="AlmaMater">Alma Mater (varchar)</param>
		/// <param name="Email">Email (varchar)</param>
		/// <param name="Evaluations">Evaluations (int)</param>
		/// <param name="Password">Password (varchar)</param>
		/// <param name="Comments">Comments (text)</param>
		/// <param name="AuditEntered">Audit Entered (datetime)</param>
		/// <param name="AuditEnteredBy">Audit Entered By (bigint)</param>
		/// <param name="AuditUpdated">Audit Updated (datetime)</param>
		/// <param name="AuditUpdatedBy">Audit Updated By (bigint)</param>
		public Contributor(
			long? Id = default(long?),
			String Name = default(String),
			String Description = default(String),
			String Degree = default(String),
			String AlmaMater = default(String),
			String Email = default(String),
			int Evaluations = default(int),
			String Password = default(String),
			String Comments = default(String),
			DateTime? AuditEntered = default(DateTime?),
			long? AuditEnteredBy = default(long?),
			DateTime? AuditUpdated = default(DateTime?),
			long? AuditUpdatedBy = default(long?)
			)
		{
			this.Id = Id;
			this.Name = Name;
			this.Description = Description;
			this.Degree = Degree;
			this.AlmaMater = AlmaMater;
			this.Email = Email;
			this.Evaluations = Evaluations;
			this.Password = Password;
			this.Comments = Comments;
			this.AuditEntered = AuditEntered;
			this.AuditEnteredBy = AuditEnteredBy;
			this.AuditUpdated = AuditUpdated;
			this.AuditUpdatedBy = AuditUpdatedBy;
		}

		/// <summary>
		/// Returns a string representation of the Object
		/// </summary>
		public override String ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class Contributor {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  Name: ").Append(Name).Append("\n");
			sb.Append("  Description: ").Append(Description).Append("\n");
			sb.Append("  Degree: ").Append(Degree).Append("\n");
			sb.Append("  AlmaMater: ").Append(AlmaMater).Append("\n");
			sb.Append("  Email: ").Append(Email).Append("\n");
			sb.Append("  Evaluations: ").Append(Evaluations).Append("\n");
			sb.Append("  Password: ").Append(Password).Append("\n");
			sb.Append("  Comments: ").Append(Comments).Append("\n");
			sb.Append("  AuditEntered: ").Append(AuditEntered).Append("\n");
			sb.Append("  AuditEnteredBy: ").Append(AuditEnteredBy).Append("\n");
			sb.Append("  AuditUpdated: ").Append(AuditUpdated).Append("\n");
			sb.Append("  AuditUpdatedBy: ").Append(AuditUpdatedBy).Append("\n");
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
			return Equals((Contributor)obj);
		}

		/// <summary>
		/// Returns true if Contributor objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(Contributor other)
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
				this.Degree == other.Degree ||
				this.Degree != null &&
				this.Degree.Equals(other.Degree)
				) &&
				(
				this.AlmaMater == other.AlmaMater ||
				this.AlmaMater != null &&
				this.AlmaMater.Equals(other.AlmaMater)
				) &&
				(
				this.Email == other.Email ||
				this.Email != null &&
				this.Email.Equals(other.Email)
				) &&
				(
				this.Evaluations == other.Evaluations ||
				this.Evaluations != null &&
				this.Evaluations.Equals(other.Evaluations)
				) &&
				(
				this.Password == other.Password ||
				this.Password != null &&
				this.Password.Equals(other.Password)
				) &&
				(
				this.Comments == other.Comments ||
				this.Comments != null &&
				this.Comments.Equals(other.Comments)
				) &&
				(
				this.AuditEntered == other.AuditEntered ||
				this.AuditEntered != null &&
				this.AuditEntered.Equals(other.AuditEntered)
				) &&
				(
				this.AuditEnteredBy == other.AuditEnteredBy ||
				this.AuditEnteredBy != null &&
				this.AuditEnteredBy.Equals(other.AuditEnteredBy)
				) &&
				(
				this.AuditUpdated == other.AuditUpdated ||
				this.AuditUpdated != null &&
				this.AuditUpdated.Equals(other.AuditUpdated)
				) &&
				(
				this.AuditUpdatedBy == other.AuditUpdatedBy ||
				this.AuditUpdatedBy != null &&
				this.AuditUpdatedBy.Equals(other.AuditUpdatedBy)
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
				if (Degree!= null) hash = hash * 59 + Degree.GetHashCode();
				if (AlmaMater!= null) hash = hash * 59 + AlmaMater.GetHashCode();
				if (Email!= null) hash = hash * 59 + Email.GetHashCode();
				if (Evaluations!= null) hash = hash * 59 + Evaluations.GetHashCode();
				if (Password!= null) hash = hash * 59 + Password.GetHashCode();
				if (Comments!= null) hash = hash * 59 + Comments.GetHashCode();
				if (AuditEntered!= null) hash = hash * 59 + AuditEntered.GetHashCode();
				if (AuditEnteredBy!= null) hash = hash * 59 + AuditEnteredBy.GetHashCode();
				if (AuditUpdated!= null) hash = hash * 59 + AuditUpdated.GetHashCode();
				if (AuditUpdatedBy!= null) hash = hash * 59 + AuditUpdatedBy.GetHashCode();
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
		public static bool operator ==(Contributor left, Contributor right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(Contributor left, Contributor right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class Contributor
} // End of Namespace Academy.Mentors.Models
