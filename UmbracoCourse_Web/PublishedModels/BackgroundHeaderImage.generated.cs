//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "backgroundHeaderImage"
	/// <summary>Background Header Image</summary>
	public partial interface IBackgroundHeaderImage : IPublishedElement
	{
		/// <summary>Background Header Image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.MediaWithCrops BackgroundImage { get; }
	}

	/// <summary>Background Header Image</summary>
	[PublishedModel("backgroundHeaderImage")]
	public partial class BackgroundHeaderImage : PublishedElementModel, IBackgroundHeaderImage
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		public new const string ModelTypeAlias = "backgroundHeaderImage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<BackgroundHeaderImage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public BackgroundHeaderImage(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Background Header Image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("backgroundImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops BackgroundImage => GetBackgroundImage(this, _publishedValueFallback);

		/// <summary>Static getter for Background Header Image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.1+406576ddfa7f3577d959a32ec2296bd9f730786e")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.MediaWithCrops GetBackgroundImage(IBackgroundHeaderImage that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(publishedValueFallback, "backgroundImage");
	}
}
