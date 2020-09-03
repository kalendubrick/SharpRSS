// <copyright file="GlobalSuppressions.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Reviewed")]
[assembly: SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Using validators to assure Uri correctness")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Reference to logger is clear", Scope = "member", Target = "~M:SharpRSS.FeedParser.FeedParser.Parse(System.String)~SharpRSS.FeedParser.Models.Feed")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Reference to logger is clear", Scope = "member", Target = "~M:SharpRSS.FeedParser.FeedParser.ParseItem(System.Xml.XmlElement,System.String)~SharpRSS.FeedParser.Models.FeedItem")]
