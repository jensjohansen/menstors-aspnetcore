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
	/// JwtUser Model
	/// </summary>
	[DataContract]
	public partial class JwtUser : IEquatable<JwtUser>
	{
		/// <summary>
		/// Default Constructor for Serialization
		/// </summary>
		public JwtUser() { }

		/// <summary>
		/// Data Member JwtUsers.Id (bigint)
		/// </summary>
		[Key]
		[DataMember(Name = "id")]
		[DisplayName("Id")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public long?  Id { get; set; }

		/// <summary>
		/// Data Member JwtUsers.UserName (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The UserName value cannot exceed 255 characters. ")]
		[DataMember(Name = "userName")]
		[DisplayName("User Name")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  UserName { get; set; }

		/// <summary>
		/// Data Member JwtUsers.Password (varchar)
		/// </summary>
		[StringLength(255, ErrorMessage = "The Password value cannot exceed 255 characters. ")]
		[DataMember(Name = "password")]
		[DisplayName("Password")]
		[DisplayFormat(NullDisplayText = "-- Unspecified --")]
		public String  Password { get; set; }

		/// <summary>
		/// Full Constructor for JwtUser Object
		/// </summary>
		/// <param name="Id">Id (bigint)</param>
		/// <param name="UserName">User Name (varchar)</param>
		/// <param name="Password">Password (varchar)</param>
		public JwtUser(
			long? Id = default(long?),
			String UserName = default(String),
			String Password = default(String)
			)
		{
			this.Id = Id;
			this.UserName = UserName;
			this.Password = Password;
		}

		/// <summary>
		/// Returns a string representation of the Object
		/// </summary>
		public override String ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class JwtUser {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  UserName: ").Append(UserName).Append("\n");
			sb.Append("  Password: ").Append(Password).Append("\n");
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
			return Equals((JwtUser)obj);
		}

		/// <summary>
		/// Returns true if JwtUser objects are Equal
		/// </summary>
		/// <param Name="other">Object to be compared to this object</param>
		/// <returns>Boolean</returns>
		public bool Equals(JwtUser other)
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
				this.UserName == other.UserName ||
				this.UserName != null &&
				this.UserName.Equals(other.UserName)
				) &&
				(
				this.Password == other.Password ||
				this.Password != null &&
				this.Password.Equals(other.Password)
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
				if (UserName!= null) hash = hash * 59 + UserName.GetHashCode();
				if (Password!= null) hash = hash * 59 + Password.GetHashCode();
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
		public static bool operator ==(JwtUser left, JwtUser right)
		{
			return Equals(left,right);
		}

		/// <summary>
		/// Not Equals Operator (!=)
		/// </summary>
		/// <param Name="left">Left side operand</param>
		/// <param Name="right">Right side operand</param>
		/// <returns>Boolean</returns>
		public static bool operator !=(JwtUser left, JwtUser right)
		{
			return !Equals(left,right);
		}


		#endregion Operators
	} // End of Partial Class JwtUser
} // End of Namespace Academy.Mentors.Models
