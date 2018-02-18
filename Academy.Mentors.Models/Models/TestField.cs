/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:44 PM */


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
	/// TestField Model
	/// </summary>
	[DataContract]
	public partial class TestField : IEquatable<TestField>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public TestField() { }

		/// <summary>
		/// Data Member TestFields.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member TestFields.Name (varchar)
		/// </summary>
		[StringLength(70, ErrorMessage = "The Name value cannot exceed 70 characters. ")]
		[DataMember(Name = "name")]
		[DisplayName("Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Name { get; set; }

		/// <summary>
		/// Data Member TestFields.Description (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Description value cannot exceed 255 characters. ")]
		[DataMember(Name = "description")]
		[DisplayName("Description")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Description { get; set; }

		/// <summary>
		/// Data Member TestFields.MyBoolean (bit)
		/// </summary>
		[DataMember(Name = "myBoolean")]
		[DisplayName("My Boolean")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		[UIHint("TrueFalse")]
		//[UIHint("YesNo")]
		//[UIHint("OnOff")]
		public bool?  MyBoolean { get; set; }

		/// <summary>
		/// Data Member TestFields.MyCreditCard (varchar)
		/// </summary>
		[StringLength(25, ErrorMessage = "The MyCreditCard value cannot exceed 25 characters. ")]
		[DataMember(Name = "myCreditCard")]
		[DisplayName("My Credit Card")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyCreditCard { get; set; }

		/// <summary>
		/// Data Member TestFields.MyCurrency (decimal)
		/// </summary>
		[DataMember(Name = "myCurrency")]
		[DisplayName("My Currency")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		[DataType(DataType.Currency)]
		public Decimal?  MyCurrency { get; set; }

		/// <summary>
		/// Data Member TestFields.MyDateTime (datetime)
		/// </summary>
		[DataMember(Name = "myDateTime")]
		[DisplayName("My Date Time")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  MyDateTime { get; set; }

		/// <summary>
		/// Data Member TestFields.MyDouble (double)
		/// </summary>
		[DataMember(Name = "myDouble")]
		[DisplayName("My Double")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public double?  MyDouble { get; set; }

		/// <summary>
		/// Data Member TestFields.MyEmail (varchar)
		/// </summary>
		[StringLength(127, ErrorMessage = "The MyEmail value cannot exceed 127 characters. ")]
		[DataMember(Name = "myEmail")]
		[DisplayName("My Email")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyEmail { get; set; }

		/// <summary>
		/// Data Member TestFields.MyFloat (float)
		/// </summary>
		[DataMember(Name = "myFloat")]
		[DisplayName("My Float")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public double?  MyFloat { get; set; }

		/// <summary>
		/// Data Member TestFields.MyImageUrl (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The MyImageUrl value cannot exceed 255 characters. ")]
		[DataMember(Name = "myImageUrl")]
		[DisplayName("My Image Url")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyImageUrl { get; set; }

		/// <summary>
		/// Data Member TestFields.MyInteger (int)
		/// </summary>
		[DataMember(Name = "myInteger")]
		[DisplayName("My Integer")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public int? MyInteger { get; set; }

		/// <summary>
		/// Data Member TestFields.MyLong (bigint)
		/// </summary>
		[DataMember(Name = "myLong")]
		[DisplayName("My Long")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  MyLong { get; set; }

		/// <summary>
		/// Data Member TestFields.MyPhone (varchar)
		/// </summary>
		[StringLength(25, ErrorMessage = "The MyPhone value cannot exceed 25 characters. ")]
		[DataMember(Name = "myPhone")]
		[DisplayName("My Phone")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyPhone { get; set; }

		/// <summary>
		/// Data Member TestFields.MyPostalCode (varchar)
		/// </summary>
		[StringLength(15, ErrorMessage = "The MyPostalCode value cannot exceed 15 characters. ")]
		[DataMember(Name = "myPostalCode")]
		[DisplayName("My Postal Code")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyPostalCode { get; set; }

		/// <summary>
		/// Data Member TestFields.MyString (varchar)
		/// </summary>
		[StringLength(35, ErrorMessage = "The MyString value cannot exceed 35 characters. ")]
		[DataMember(Name = "myString")]
		[DisplayName("My String")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyString { get; set; }

		/// <summary>
		/// Data Member TestFields.MyTextArea (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The MyTextArea value cannot exceed 255 characters. ")]
		[DataMember(Name = "myTextArea")]
		[DisplayName("My Text Area")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyTextArea { get; set; }

		/// <summary>
		/// Data Member TestFields.MyTicks (bigint)
		/// </summary>
		[DataMember(Name = "myTicks")]
		[DisplayName("My Ticks")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  MyTicks { get; set; }

		/// <summary>
		/// Data Member TestFields.MyUrl (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The MyUrl value cannot exceed 255 characters. ")]
		[DataMember(Name = "myUrl")]
		[DisplayName("My Url")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  MyUrl { get; set; }

		/// <summary>
		/// Data Member TestFields.Comments (text)
		/// </summary>
		[DataMember(Name = "comments")]
		[DisplayName("Comments")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Comments { get; set; }

		/// <summary>
		/// Data Member TestFields.AuditEntered (datetime)
		/// </summary>
		[DataMember(Name = "auditEntered")]
		[DisplayName("Audit Entered")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditEntered { get; set; }

		/// <summary>
		/// Data Member TestFields.AuditEnteredBy (bigint)
		/// </summary>
		[ForeignKey("AuditEnteredBy")]
		[DataMember(Name = "auditEnteredBy")]
		[DisplayName("Audit Entered By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditEnteredBy { get; set; }

		/// <summary>
		/// Data Member TestFields.AuditUpdated (datetime)
		/// </summary>
		[DataMember(Name = "auditUpdated")]
		[DisplayName("Audit Updated")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --",DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}" )]
		[DataType(DataType.DateTime)]
		public DateTime?  AuditUpdated { get; set; }

		/// <summary>
		/// Data Member TestFields.AuditUpdatedBy (bigint)
		/// </summary>
		[ForeignKey("AuditUpdatedBy")]
		[DataMember(Name = "auditUpdatedBy")]
		[DisplayName("Audit Updated By")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  AuditUpdatedBy { get; set; }

		/// <summary>
		/// Full Constructor for TestField Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="Name">Name (varchar)</param>
		/// <param name="Description">Description (varchar)</param>
		/// <param name="MyBoolean">My Boolean (bit)</param>
		/// <param name="MyCreditCard">My Credit Card (varchar)</param>
		/// <param name="MyCurrency">My Currency (decimal)</param>
		/// <param name="MyDateTime">My Date Time (datetime)</param>
		/// <param name="MyDouble">My Double (double)</param>
		/// <param name="MyEmail">My Email (varchar)</param>
		/// <param name="MyFloat">My Float (float)</param>
		/// <param name="MyImageUrl">My Image Url (varchar)</param>
		/// <param name="MyInteger">My Integer (int)</param>
		/// <param name="MyLong">My Long (bigint)</param>
		/// <param name="MyPhone">My Phone (varchar)</param>
		/// <param name="MyPostalCode">My Postal Code (varchar)</param>
		/// <param name="MyString">My String (varchar)</param>
		/// <param name="MyTextArea">My Text Area (varchar)</param>
		/// <param name="MyTicks">My Ticks (bigint)</param>
		/// <param name="MyUrl">My Url (varchar)</param>
		/// <param name="Comments">Comments (text)</param>
		/// <param name="AuditEntered">Audit Entered (datetime)</param>
		/// <param name="AuditEnteredBy">Audit Entered By (bigint)</param>
		/// <param name="AuditUpdated">Audit Updated (datetime)</param>
		/// <param name="AuditUpdatedBy">Audit Updated By (bigint)</param>
		public TestField(
			long? Id = default(long?),
			String Name = default(String),
			String Description = default(String),
			bool MyBoolean = default(bool),
			String MyCreditCard = default(String),
			Decimal? MyCurrency = default(Decimal?),
			DateTime? MyDateTime = default(DateTime?),
			double? MyDouble = default(double?),
			String MyEmail = default(String),
			float? MyFloat = default(float?),
			String MyImageUrl = default(String),
			int MyInteger = default(int),
			long? MyLong = default(long?),
			String MyPhone = default(String),
			String MyPostalCode = default(String),
			String MyString = default(String),
			String MyTextArea = default(String),
			long? MyTicks = default(long?),
			String MyUrl = default(String),
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
			this.MyBoolean = MyBoolean;
			this.MyCreditCard = MyCreditCard;
			this.MyCurrency = MyCurrency;
			this.MyDateTime = MyDateTime;
			this.MyDouble = MyDouble;
			this.MyEmail = MyEmail;
			this.MyFloat = MyFloat;
			this.MyImageUrl = MyImageUrl;
			this.MyInteger = MyInteger;
			this.MyLong = MyLong;
			this.MyPhone = MyPhone;
			this.MyPostalCode = MyPostalCode;
			this.MyString = MyString;
			this.MyTextArea = MyTextArea;
			this.MyTicks = MyTicks;
			this.MyUrl = MyUrl;
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
			sb.Append("class TestField {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  Name: ").Append(Name).Append("\n");
			sb.Append("  Description: ").Append(Description).Append("\n");
			sb.Append("  MyBoolean: ").Append(MyBoolean).Append("\n");
			sb.Append("  MyCreditCard: ").Append(MyCreditCard).Append("\n");
			sb.Append("  MyCurrency: ").Append(MyCurrency).Append("\n");
			sb.Append("  MyDateTime: ").Append(MyDateTime).Append("\n");
			sb.Append("  MyDouble: ").Append(MyDouble).Append("\n");
			sb.Append("  MyEmail: ").Append(MyEmail).Append("\n");
			sb.Append("  MyFloat: ").Append(MyFloat).Append("\n");
			sb.Append("  MyImageUrl: ").Append(MyImageUrl).Append("\n");
			sb.Append("  MyInteger: ").Append(MyInteger).Append("\n");
			sb.Append("  MyLong: ").Append(MyLong).Append("\n");
			sb.Append("  MyPhone: ").Append(MyPhone).Append("\n");
			sb.Append("  MyPostalCode: ").Append(MyPostalCode).Append("\n");
			sb.Append("  MyString: ").Append(MyString).Append("\n");
			sb.Append("  MyTextArea: ").Append(MyTextArea).Append("\n");
			sb.Append("  MyTicks: ").Append(MyTicks).Append("\n");
			sb.Append("  MyUrl: ").Append(MyUrl).Append("\n");
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
			return Equals((TestField)obj);
		}

		/// <summary>
		/// Returns true if TestField objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(TestField other)
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
				this.MyBoolean == other.MyBoolean ||
				this.MyBoolean != null &&
				this.MyBoolean.Equals(other.MyBoolean)
				) &&
				(
				this.MyCreditCard == other.MyCreditCard ||
				this.MyCreditCard != null &&
				this.MyCreditCard.Equals(other.MyCreditCard)
				) &&
				(
				this.MyCurrency == other.MyCurrency ||
				this.MyCurrency != null &&
				this.MyCurrency.Equals(other.MyCurrency)
				) &&
				(
				this.MyDateTime == other.MyDateTime ||
				this.MyDateTime != null &&
				this.MyDateTime.Equals(other.MyDateTime)
				) &&
				(
				this.MyDouble == other.MyDouble ||
				this.MyDouble != null &&
				this.MyDouble.Equals(other.MyDouble)
				) &&
				(
				this.MyEmail == other.MyEmail ||
				this.MyEmail != null &&
				this.MyEmail.Equals(other.MyEmail)
				) &&
				(
				this.MyFloat == other.MyFloat ||
				this.MyFloat != null &&
				this.MyFloat.Equals(other.MyFloat)
				) &&
				(
				this.MyImageUrl == other.MyImageUrl ||
				this.MyImageUrl != null &&
				this.MyImageUrl.Equals(other.MyImageUrl)
				) &&
				(
				this.MyInteger == other.MyInteger ||
				this.MyInteger != null &&
				this.MyInteger.Equals(other.MyInteger)
				) &&
				(
				this.MyLong == other.MyLong ||
				this.MyLong != null &&
				this.MyLong.Equals(other.MyLong)
				) &&
				(
				this.MyPhone == other.MyPhone ||
				this.MyPhone != null &&
				this.MyPhone.Equals(other.MyPhone)
				) &&
				(
				this.MyPostalCode == other.MyPostalCode ||
				this.MyPostalCode != null &&
				this.MyPostalCode.Equals(other.MyPostalCode)
				) &&
				(
				this.MyString == other.MyString ||
				this.MyString != null &&
				this.MyString.Equals(other.MyString)
				) &&
				(
				this.MyTextArea == other.MyTextArea ||
				this.MyTextArea != null &&
				this.MyTextArea.Equals(other.MyTextArea)
				) &&
				(
				this.MyTicks == other.MyTicks ||
				this.MyTicks != null &&
				this.MyTicks.Equals(other.MyTicks)
				) &&
				(
				this.MyUrl == other.MyUrl ||
				this.MyUrl != null &&
				this.MyUrl.Equals(other.MyUrl)
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
				if (MyBoolean!= null) hash = hash * 59 + MyBoolean.GetHashCode();
				if (MyCreditCard!= null) hash = hash * 59 + MyCreditCard.GetHashCode();
				if (MyCurrency!= null) hash = hash * 59 + MyCurrency.GetHashCode();
				if (MyDateTime!= null) hash = hash * 59 + MyDateTime.GetHashCode();
				if (MyDouble!= null) hash = hash * 59 + MyDouble.GetHashCode();
				if (MyEmail!= null) hash = hash * 59 + MyEmail.GetHashCode();
				if (MyFloat!= null) hash = hash * 59 + MyFloat.GetHashCode();
				if (MyImageUrl!= null) hash = hash * 59 + MyImageUrl.GetHashCode();
				if (MyInteger!= null) hash = hash * 59 + MyInteger.GetHashCode();
				if (MyLong!= null) hash = hash * 59 + MyLong.GetHashCode();
				if (MyPhone!= null) hash = hash * 59 + MyPhone.GetHashCode();
				if (MyPostalCode!= null) hash = hash * 59 + MyPostalCode.GetHashCode();
				if (MyString!= null) hash = hash * 59 + MyString.GetHashCode();
				if (MyTextArea!= null) hash = hash * 59 + MyTextArea.GetHashCode();
				if (MyTicks!= null) hash = hash * 59 + MyTicks.GetHashCode();
				if (MyUrl!= null) hash = hash * 59 + MyUrl.GetHashCode();
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
		public static bool operator ==(TestField left, TestField right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(TestField left, TestField right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class TestField
} // End of Namespace Academy.Mentors.Models
