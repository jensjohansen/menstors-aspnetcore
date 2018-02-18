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
	/// Review Model
	/// </summary>
	[DataContract]
	public partial class Review : IEquatable<Review>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public Review() { }

		/// <summary>
		/// Data Member Reviews.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member Reviews.Name (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The Name value cannot exceed 70 characters. ")]
		[DataMember(Name = "name")]
		[DisplayName("Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Name { get; set; }

		/// <summary>
		/// Data Member Reviews.Description (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Description value cannot exceed 255 characters. ")]
		[DataMember(Name = "description")]
		[DisplayName("Description")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Description { get; set; }

		/// <summary>
		/// Data Member Reviews.ContributorId (bigint)
		/// </summary>
		[ForeignKey("Contributor")]
		[DataMember(Name = "contributorId")]
		[DisplayName("Contributor Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  ContributorId { get; set; }

		/// <summary>
		/// Data Member Reviews.PaperId (bigint)
		/// </summary>
		[ForeignKey("Paper")]
		[DataMember(Name = "paperId")]
		[DisplayName("Paper Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  PaperId { get; set; }

		/// <summary>
		/// Data Member Reviews.PaperVersionId (bigint)
		/// </summary>
		[ForeignKey("PaperVersion")]
		[DataMember(Name = "paperVersionId")]
		[DisplayName("Paper Version Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  PaperVersionId { get; set; }

		/// <summary>
		/// Data Member Reviews.Comments (text)
		/// </summary>
		[DataMember(Name = "comments")]
		[DisplayName("Comments")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Comments { get; set; }

		/// <summary>
		/// Data Member Reviews.AuditEntered (datetime)
		/// </summary>
		[DataMember(Name = "auditEntered")]
		[DisplayName("Audit Entered")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditEntered { get; set; }

		/// <summary>
		/// Data Member Reviews.AuditEnteredBy (bigint)
		/// </summary>
		[ForeignKey("AuditEnteredBy")]
		[DataMember(Name = "auditEnteredBy")]
		[DisplayName("Audit Entered By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditEnteredBy { get; set; }

		/// <summary>
		/// Data Member Reviews.AuditUpdated (datetime)
		/// </summary>
		[DataMember(Name = "auditUpdated")]
		[DisplayName("Audit Updated")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditUpdated { get; set; }

		/// <summary>
		/// Data Member Reviews.AuditUpdatedBy (bigint)
		/// </summary>
		[ForeignKey("AuditUpdatedBy")]
		[DataMember(Name = "auditUpdatedBy")]
		[DisplayName("Audit Updated By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditUpdatedBy { get; set; }

		/// <summary>
		/// Virtual Parent Object: Contributor
		/// </summary>
		[ForeignKey("ContributorId")]
		[DataMember(Name = "contributor")]
		public virtual Contributor Contributor { get; private set; }

		/// <summary>
		/// Virtual Parent Object: Paper
		/// </summary>
		[ForeignKey("PaperId")]
		[DataMember(Name = "paper")]
		public virtual Paper Paper { get; private set; }

		/// <summary>
		/// Virtual Parent Object: PaperVersion
		/// </summary>
		[ForeignKey("PaperVersionId")]
		[DataMember(Name = "paperVersion")]
		public virtual PaperVersion PaperVersion { get; private set; }

		/// <summary>
		/// Full Constructor for Review Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="Name">Name (varchar)</param>
		/// <param name="Description">Description (varchar)</param>
		/// <param name="ContributorId">Contributor Id (bigint)</param>
		/// <param name="PaperId">Paper Id (bigint)</param>
		/// <param name="PaperVersionId">Paper Version Id (bigint)</param>
		/// <param name="Comments">Comments (text)</param>
		/// <param name="AuditEntered">Audit Entered (datetime)</param>
		/// <param name="AuditEnteredBy">Audit Entered By (bigint)</param>
		/// <param name="AuditUpdated">Audit Updated (datetime)</param>
		/// <param name="AuditUpdatedBy">Audit Updated By (bigint)</param>
		public Review(
			long? Id = default(long?),
			String Name = default(String),
			String Description = default(String),
			long? ContributorId = default(long?),
			long? PaperId = default(long?),
			long? PaperVersionId = default(long?),
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
			this.ContributorId = ContributorId;
			this.PaperId = PaperId;
			this.PaperVersionId = PaperVersionId;
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
			sb.Append("class Review {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  Name: ").Append(Name).Append("\n");
			sb.Append("  Description: ").Append(Description).Append("\n");
			sb.Append("  ContributorId: ").Append(ContributorId).Append("\n");
			sb.Append("  PaperId: ").Append(PaperId).Append("\n");
			sb.Append("  PaperVersionId: ").Append(PaperVersionId).Append("\n");
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
			return Equals((Review)obj);
		}

		/// <summary>
		/// Returns true if Review objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(Review other)
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
				this.ContributorId == other.ContributorId ||
				this.ContributorId != null &&
				this.ContributorId.Equals(other.ContributorId)
				) &&
				(
				this.PaperId == other.PaperId ||
				this.PaperId != null &&
				this.PaperId.Equals(other.PaperId)
				) &&
				(
				this.PaperVersionId == other.PaperVersionId ||
				this.PaperVersionId != null &&
				this.PaperVersionId.Equals(other.PaperVersionId)
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
				if (ContributorId!= null) hash = hash * 59 + ContributorId.GetHashCode();
				if (PaperId!= null) hash = hash * 59 + PaperId.GetHashCode();
				if (PaperVersionId!= null) hash = hash * 59 + PaperVersionId.GetHashCode();
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
		public static bool operator ==(Review left, Review right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(Review left, Review right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class Review
} // End of Namespace Academy.Mentors.Models
