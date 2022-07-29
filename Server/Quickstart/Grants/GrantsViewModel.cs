// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;

namespace IdentityServerHost.Quickstart.UI
{
    public class GrantsViewModel
    {
        public IEnumerable<GrantViewModel> Grants { get; set; } = default!;
    }

    public class GrantViewModel
    {
        public string ClientId { get; set; } = default!;
        public string ClientName { get; set; } = default!;
        public string ClientUrl { get; set; } = default!;
        public string ClientLogoUrl { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public IEnumerable<string> IdentityGrantNames { get; set; } = default!;
        public IEnumerable<string> ApiGrantNames { get; set; } = default!;
        }
}