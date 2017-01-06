﻿using System;
using System.Text.RegularExpressions;
using Group = TShockAPI.Group;

namespace SummonLimit
{
	// From EssentialsPlus

	public static class GroupExtensions
	{
		/// <summary>
		/// Returns the number in the end of the permission.
		/// </summary>
		/// <param name="group">A TShock group.</param>
		/// <param name="root">Root of permission (part before number)</param>
		/// <returns></returns>
		public static int GetDynamicPermission(this Group group, string root)
		{
			if (group.HasPermission(root + ".*") || group.HasPermission("*"))
				return short.MaxValue;

			int max = SummonLimit.MaxSummons;
			var regex = new Regex("^" + root.Replace(".", @"\.") + @"\.(\d+)$");

			foreach (string permission in group.TotalPermissions)
			{
				Match match = regex.Match(permission);
				if (match.Success && match.Value == permission)
					max = Math.Max(max, Convert.ToInt32(match.Groups[1].Value));
			}

			return max;
		}
	}
}