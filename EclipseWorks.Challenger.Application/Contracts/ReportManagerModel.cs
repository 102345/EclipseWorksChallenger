﻿using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.Application.Contracts
{
    [ExcludeFromCodeCoverage]
    public class ReportManagerModel
    {
        public string NameProject { get; set; }

        public int CountTask { get; set; }

        public string Task { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }
    }
}
